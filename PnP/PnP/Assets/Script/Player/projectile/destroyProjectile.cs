using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class destroyProjectile : NetworkBehaviour
{
    // Use this for initialization
    void OnCollisionEnter(Collision collision)
    {
        GameObject.Destroy(this.gameObject);
        if (collision.gameObject.tag != "Wall" && isServer)
        {
            NetworkServer.Destroy(collision.gameObject);
        }
    }
}
