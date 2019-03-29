using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MapTimers : NetworkBehaviour {
	public NetworkedPlayer[] playerManager;
	private NetworkedPlayer winningPlayer;
	private int highScore = -1;
	public int gameLength;
	private int numPlayers;
	Text Timer;
	void Start () {
		gameLength = 120;	
		playerManager = GameObject.FindObjectsOfType<NetworkedPlayer>();
		numPlayers = playerManager.Length;
		for(int i = 0; i < numPlayers; i++){
			playerManager[i].Rpc_timerSet(gameLength);
		}
		StartCoroutine(gameTimer(gameLength));
	}
	
	public IEnumerator gameTimer(int gameTime){
		while (gameTime > 0){
			yield return new WaitForSeconds(1.0f);
			gameTime--;
			/* This will display the timer at the top of the screen for the gamerz to see */
			for(int i = 0; i < numPlayers; i++){
				playerManager[i].Rpc_timerSet(gameTime);
			}	

		}
		/* This will make all the players stop spawning and kill them all. */
		for(int i = 0; i < numPlayers; i++){
			playerManager[i].Cmd_stopSpawning();
			/* This checks to see who has the highest score at the end of the game. */
			if(playerManager[i].points > highScore){
				highScore = playerManager[i].points;
				winningPlayer = playerManager[i];
			}
		}
		if(highScore != winningPlayer.points){
			Debug.Log("Draw!");
		}
		else{
			//This sends all the players the winner of the game, will test if this works.
			for(int i = 0; i < numPlayers; i++){
				playerManager[i].Rpc_DeclareWinner(winningPlayer.playerName, winningPlayer.points, winningPlayer.characterIndex);
			}
		}
	}
	void Update(){
		playerManager = GameObject.FindObjectsOfType<NetworkedPlayer>();
		numPlayers = playerManager.Length;
				
	}

}
