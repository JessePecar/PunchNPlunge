using UnityEngine;
using Prototype.NetworkLobby;
using System.Collections;
using UnityEngine.Networking;

public class NetworkLobbyHook : LobbyHook 
{
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        NetworkedPlayer nPlayer = gamePlayer.GetComponent<NetworkedPlayer>();
        //This will initialize the NetworkedPlayer Attributes
        nPlayer.respawning = true;
        nPlayer.playerName = lobby.nameInput.text;
        nPlayer.characterIndex = lobby.playerColor;
        nPlayer.points = 0;
        nPlayer.health = 3;
    }
}
