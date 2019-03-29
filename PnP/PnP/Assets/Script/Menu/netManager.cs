using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//public class netManager : NetworkManager {
public class netManager : NetworkLobbyManager {

  public PNPNetworkDiscovery discoveryComp;
  
  public Scene[] levels;
  int levelIndex = 0;
  int serverPort = 7777;

  void Start(){
    string[] args = System.Environment.GetCommandLineArgs ();
    string input = "";
    bool startingAsServer = false;
    this.serverPort = Random.Range(7778, 8887);
    for (int i = 0; i < args.Length; i++) {
       //Debug.Log ("ARG " + i + ": " + args [i]);
       if (args[i] == "--server") {
           startingAsServer = true;
       }
       if(args[i] == "-level"){
         levelIndex = int.Parse(args[i+1]);
       }
    this.networkPort = this.serverPort;
    }
    if(startingAsServer){
      this.StartDiscoveryServer();
      StartServer();
    }
  }
  
  public void StartDiscoveryClient(){
    discoveryComp.StartClient();
  }
  
  public void StartServing(){
    StartDiscoveryServer();
    StartServer();
    //StartClient();
  }
  public void StartHosting(){
    StartDiscoveryServer();
    StartHost();
  }
  
  public void StartDiscoveryServer(){
    
    const string glyphs= "abcdefghijklmnopqrstuvwxyz0123456789";
    int charAmount = 16; //set those to the minimum and maximum length of your string
    string myString = "";
    for(int i=0; i<charAmount; i++)
    {
        myString += glyphs[Random.Range(0, glyphs.Length)];
        //Debug.Log(glyphs[Random.Range(0, glyphs.Length)]);
        //Debug.Log(myString);
    }
    discoveryComp.broadcastData = myString+":PurpleLevel:"+serverPort+":Waiting:";
    discoveryComp.StartServer();
    
  }
	public void StartupHost () {
		SetPort();
		NetworkManager.singleton.StartHost();
	}
	void SetPort(){
		NetworkManager.singleton.networkPort = 7777;
	}
	public void JoinGame(){
		SetIPAddress();
		//SetPort();
		NetworkManager.singleton.StartClient();
	}
	void SetIPAddress(){
		string ipAddress = GameObject.Find("InputFieldIPAddress").GetComponentInChildren<Text>().text;
		NetworkManager.singleton.networkAddress = ipAddress;
	}
  
  
  /*
  void Start(){
    MMStart();
    MMListMatches();
  }
    
	void MMStart(){
		this.StartMatchMaker();
	}
	void MMListMatches(){
		this.matchMaker.ListMatches(0, 20, "", true, 0, 0, OnMatchList);
	}
	public override void OnMatchList(bool success, string extendedInfo, List<UnityEngine.Networking.Match.MatchInfoSnapshot> matchList){
		base.OnMatchList(success, extendedInfo, matchList);

		if(!success){
			//Success failed, there were no lobbys
				Debug.Log("No Lobbies Open");
		}
		else{
			//if there isn't an open lobby, create one, if so, dont create and join
			if(matchList.Count > 0){
				//Debug.Log(matchList[0]);
				//MMjoinMatch(matchList[0]);
			}
			else{
				MMcreateMatch();
			}
		}

	}
	void MMcreateMatch(){
		this.matchMaker.CreateMatch("MM", 15, true, "","","",0,0, OnMatchCreate);

	}
	public override void OnMatchCreate(bool success, string extendedInfo, UnityEngine.Networking.Match.MatchInfo matchInfo){
		base.OnMatchCreate(success, extendedInfo, matchInfo);
		if(!success){
			//Failed to created the match
		}
		else{
			//Successfully created a lobby
		}
	}
	void MMjoinMatch(UnityEngine.Networking.Match.MatchInfoSnapshot firstMatch){
        this.matchMaker.JoinMatch(firstMatch.networkId, "", "", "", 0, 0, OnMatchJoined);
	}*/ 

}
