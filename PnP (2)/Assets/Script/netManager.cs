using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class netManager : NetworkLobbyManager {

	void Start () {
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
		}
		else{
			//if there isn't an open lobby, create one, if so, dont create and join
			if(matchList.Count > 0){
				Debug.Log(matchList[0]);
				MMjoinMatch(matchList[0]);
			}
			else{
				MMcreateMatch();
			}
		}

	}
	void MMcreateMatch(){
		
	}
	void MMjoinMatch(UnityEngine.Networking.Match.MatchInfoSnapshot firstMatch){
        this.matchMaker.JoinMatch(firstMatch.networkId, "", "", "", 0, 0, OnMatchJoined);
	}
}
