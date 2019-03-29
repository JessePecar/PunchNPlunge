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
    Vector3 mousePos;
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

        if(canPunch){
            Vector3 mousePos = new Vector3(0, 0, 0);
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Debug.Log(mousePos);

            glove.transform.LookAt(mousePos);
            glove.transform.position = new Vector3((this.transform.position.x + mousePos.x), 
			(this.transform.position.y + mousePos.y), 
			this.transform.position.z);
		}

		float axisH = Input.GetAxis("Horizontal");
		if(axisH != 0){
			rb.velocity = new Vector3(axisH * 5, rb.velocity.y, 0);
		}
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
			
			
			
			canPunch = false;
			StartCoroutine(punchOut());
		}
		
	}
	public IEnumerator punchOut(){
        //This will be the basis of where I want it to be
		
		
        yield return new WaitForSeconds(0.75f);
        canPunch = true;
	}
}
