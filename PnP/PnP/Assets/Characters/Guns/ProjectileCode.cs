using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ProjectileCode : NetworkBehaviour {

	public string type;//Plunge,Punch
	[SyncVar]
	public Vector3 angle;
	[SyncVar]
	public float speed = 0;
	[SyncVar]
	public GameObject owner;
	
	// Use this for initialization
	void Start () {
		StartCoroutine(StartMoving());
	}
	
	IEnumerator StartMoving(){
		while(speed == 0){
			yield return new WaitForSeconds(0.01f);
		}
		
		this.transform.rotation = Quaternion.Euler(this.angle);
		this.GetComponent<Rigidbody2D>().velocity = this.angle*this.speed; //may need to increase 5
		
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	[Command]
	public void Cmd_StartMoving(Vector3 angle, float speed ){
		this.angle = angle;
		this.speed = speed;
		
	}
	void OnCollisionEnter2D(Collision2D collision)
    {
		//Debug.Log(collision.gameObject);
		if(isServer){
			
			if(collision.gameObject.tag == "Environment" && this.type=="Plunge"){
				NetworkedPlayer ownerPlayer = owner.gameObject.GetComponent<NetworkedPlayer>();
				ownerPlayer.StartRappelC(collision.contacts[0].point , 1.0f);
			}
			if(collision.gameObject.tag == "Player"){
				NetworkedPlayer otherPlayer = collision.gameObject.GetComponent<NetworkedPlayerMovement>().owner;
				if(this.type=="Plunge"){
					//Debug.Log("Plunged Someone");
					//Debug.Log(-this.GetComponent<Rigidbody2D>().velocity);
					
					otherPlayer.StartPlungeC(-angle , 1.0f);
					//This little interesting piece of code would take away points when the other player was plunged, which actually hilarious
					/* TODO: Add a stun funcitonality to the player. */
					//otherPlayer.points--;
				}
				else if(this.type=="Punch"){
					//Debug.Log("Punched Someone");
					collision.gameObject.GetComponent<NetworkedPlayerMovement>().canMove = true;
					collision.gameObject.GetComponent<Rigidbody2D>().AddForce(this.GetComponent<Rigidbody2D>().velocity);
					this.owner.GetComponent<NetworkedPlayer>().points++;
					otherPlayer.Cmd_TakeDamage(1);
				}
			}
			NetworkServer.Destroy(this.gameObject);
		}
    }
	
}
