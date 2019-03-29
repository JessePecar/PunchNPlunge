using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkedPlayerMovement : NetworkBehaviour {
	GameObject player;
	public GameObject selectedPlayer1Prefab;
    public GameObject selectedPlayer2Prefab;
    public GameObject selectedPlayer3Prefab;
    public GameObject selectedPlayer4Prefab;
	public bool facingLeft;
	Rigidbody2D rb;
	float moveForce, maxSpeed;
    Vector3 mousePos;
	public bool canJump, canPunch, canMove, facing;
	public bool isJumping;
	public float axisH;
	private bool showArms;
	int animationChanger;
	public Vector3 mousePosition;
	public NetworkedPlayer owner;
	
	[SyncVar]
	public GameObject ownerObject;
	
	public ArmAdjust armPosition;
	// Use this for initialization
	void Start () {
		animationChanger = 0;
        player = this.gameObject;
		rb = player.GetComponent<Rigidbody2D>();
		moveForce = 0.08f;
		maxSpeed = 1f;
        canJump = true;
		canPunch = true;
		facing = true;
		this.canMove = true;
		this.armPosition = this.gameObject.GetComponentInChildren<ArmAdjust>();
       	this.owner.playerObject = this.gameObject;
	}
 
	[Command]
    public void Cmd_UpdatePosition(float axis, bool jumping, Vector3 mousePosition){
		if(this.canMove){
			if(axis != 0 && Mathf.Abs(rb.velocity.x)<=5){
				rb.velocity = new Vector3(axis * 5, rb.velocity.y, 0);
			}
			else if(axis == 0 && Mathf.Abs(rb.velocity.x)<=5){
				rb.velocity = new Vector3(0, rb.velocity.y, 0);
			}
			if(jumping && owner.canJump){
				//Debug.Log("Jump!");
				rb.AddForce(Vector2.up * 300);
				owner.canJump = false;
			}

			if(rb.velocity.y == 0){
				owner.canJump = true;
			}
		}
	}
	// Update is called once per frame
	void Update () {
		if(owner != null){
			Cmd_UpdatePosition(owner.axisH, owner.isJumping, owner.mousePosition);
		}
	}
	void LateUpdate(){
		if(isServer){
			if(this.owner == null && this.ownerObject != null){
				this.owner = this.ownerObject.GetComponent<NetworkedPlayer>();
			}
			if(this.owner == null || this.owner.playerObject == null || this.owner.playerObject != this.gameObject){
        		NetworkServer.Destroy(this.gameObject);
			}
			this.armPosition.AdjustAngle(this.owner.mousePosition);
		}
	}
}
