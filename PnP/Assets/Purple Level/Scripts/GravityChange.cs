using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChange : MonoBehaviour {
	[SerializeField]
	private float gravityNum;
	// Use this for initialization
	void Start () {
        Physics.gravity = new Vector2(0, gravityNum);
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
