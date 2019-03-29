using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.NetworkLobby;

public class FindBackButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
  public void TryToBack(){
    
      LobbyManager[] managers = GameObject.FindObjectsOfType<LobbyManager>();
      if(managers.Length>0 && managers[0] != null){
        managers[0].GoBackButton();
      }
  }
}
