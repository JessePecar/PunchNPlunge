using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
//using Literally.Everything;

public class PNPNetworkDiscovery : NetworkDiscovery
{
    public Dictionary<string, GameDiscovery> discoveredGames = new Dictionary<string, GameDiscovery>();
    void Start()
    {
       
    }

    public void StartClient()
    {
        Initialize();
        StartAsClient();
    }
    public void StartServer()
    {
        Initialize();
        StartAsServer();
    }

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
      //Name,Level,Port,Status
      string[] parsedData = data.Split(':');
      GameDiscovery temp;
      if(discoveredGames.TryGetValue(parsedData[0], out temp))
      {
          //success!
      }
      else
      {
        discoveredGames.Add(parsedData[0],
          //Checking for Memory Leak Later
          new GameDiscovery(
            parsedData[0],
            0,
            parsedData[1],
            fromAddress,
            int.Parse(parsedData[2]),
            parsedData[3]          
          )
        );
      }
      
      //Debug.Log(fromAddress);
      //Debug.Log(data);
        /*NetworkManager.singleton.networkAddress = fromAddress;
        NetworkManager.singleton.StartClient();*/
    }
}