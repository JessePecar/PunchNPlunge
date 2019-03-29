using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmAdjust : MonoBehaviour {

	public NetworkedPlayerMovement nPlayer;
	
	// Use this for initialization
	void Start () {
		//nPlayer = this.gameObject.GetComponent<NetworkedPlayer>();
		if(this.nPlayer == null){
			nPlayer = this.transform.parent.gameObject.GetComponent<NetworkedPlayerMovement>();
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void AdjustAngle(Vector3 mousePosition){
		
		float AngleRad = Mathf.Atan2 (this.transform.position.y - this.nPlayer.owner.mousePosition.y , this.transform.position.x - this.nPlayer.owner.mousePosition.x );
		float angle = (180 / Mathf.PI) * AngleRad;
	 
		this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);;
	}
	
	void LateUpdate(){
		
		//this.rotation 
	}
}
