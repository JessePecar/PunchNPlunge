using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {
	GameObject player;
	[SerializeField]
	
	GameObject glove;
	[SerializeField]
	public Plane m_Plane;
	Rigidbody rb;
	float moveForce, maxSpeed;
	bool canJump, canPunch, facing;
	// Use this for initialization
	void Start () {
		player = this.gameObject;
		rb = player.GetComponent<Rigidbody>();
		moveForce = 0.08f;
		maxSpeed = 1f;
        canJump = true;
		canPunch = true;
		facing = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		float axisH = Input.GetAxis("Horizontal");
		player.transform.Translate(Vector2.right * axisH * moveForce);
		if(facing & canPunch){
			glove.transform.position = new Vector3(this.transform.position.x + 0.5f, this.transform.position.y,this.transform.position.z);
		}
		if(Input.GetAxis("Jump") > 0 && canJump){
			Debug.Log("Jump!");
			rb.AddForce(Vector2.up * 400);
			canJump = false;
		}

		if(rb.velocity.y == 0){
			canJump = true;
		}
		
		if(Input.GetAxis("Fire2") > 0 && canPunch){
			Ray mousePos;
            //RaycastHit hit;
			mousePos = new Ray();
			mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
			float hit;
			Vector3 punchDirection = Vector3.zero;
			if(m_Plane.Raycast(mousePos, out hit)){
				punchDirection = mousePos.GetPoint(hit);
				Debug.Log(hit);
			}
			glove.transform.LookAt(punchDirection);
			canPunch = false;
			StartCoroutine(punchOut());
		}
		
	}
	public IEnumerator punchOut(){
        //This will be the basis of where I want it to be
		//glove.transform.Translate(Vector3.forward * 3);
		glove.GetComponent<Rigidbody>().AddForce(glove.transform.forward * 60);
        yield return new WaitForSeconds(0.75f);
        glove.GetComponent<Rigidbody>().velocity = Vector3.zero;
        glove.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        
        glove.transform.LookAt(this.transform);
		StartCoroutine(punchIn());
	}
	public IEnumerator punchIn(){
        
        //glove.transform.Translate(Vector3.forward * 3);
        glove.GetComponent<Rigidbody>().AddForce(glove.transform.forward * 60);
        yield return new WaitForSeconds(0.75f);
		glove.GetComponent<Rigidbody>().velocity = Vector3.zero;
        glove.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		canPunch = true;
	}
}
