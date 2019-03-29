using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLight : MonoBehaviour {
	[SerializeField]
	GameObject followPlayer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.transform.position = new Vector3(followPlayer.transform.position.x, 
			followPlayer.transform.position.y + 0.5f, 
			followPlayer.transform.position.z - 15);
	}
}
