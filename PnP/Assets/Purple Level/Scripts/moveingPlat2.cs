using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveingPlat2 : MonoBehaviour {
    bool moveUp;
	// Use this for initialization
	void Start () {
		moveUp = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (moveUp)
        {
            this.gameObject.transform.Translate(0, 0.01f, 0);
        }
        else
        {
            this.gameObject.transform.Translate(0, -0.01f, 0);
        }

        //This will check it's positioning to see if the block is out of bounds
        if (this.gameObject.transform.position.y < 0.5f)
        {
            moveUp = true;
        }
        else if (this.gameObject.transform.position.y > 4.0f)
        {
            moveUp = false;
        }
	}
}
