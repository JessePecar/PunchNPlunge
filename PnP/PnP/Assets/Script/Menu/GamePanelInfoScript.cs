using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 
using UnityEngine.Networking; 
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GamePanelInfoScript : MonoBehaviour, IPointerClickHandler {

    //We will put here reference to prefab in Editor
    public string GameName = "Gibberish";
    public string ip = "127.0.0.1";
    public int port = 7777;
    public string status = "Waiting";
    public string level = "PurpleLevel";
    public int players = 0;
    
    public Text GameNameText;
    public Text Players;
    //public Text IPPort;
    public Color WaitingColor;
    public Color RunningColor;
    public Color AtCapacityColor;
    public Image LevelImage;
    public Button StartButton;
    NetworkManager netManager;
    //public Image discoveryComp;
    // Use this for initialization
    void Start () {
      UpdateInfo();
      NetworkManager[] components = GameObject.FindObjectsOfType<NetworkManager>();
      if(components.Length>0){
        netManager = components[0];
      }
      
    }
    public void UpdateInfo(){
      GameNameText.text = ip+":"+port+" ("+status+")";
      Players.text = players + " player(s) online";
      if(status=="Running"){
        LevelImage.color = RunningColor;
      }else if(status=="AtCapacity"){
        LevelImage.color = AtCapacityColor;
      }else{
        LevelImage.color = WaitingColor;
      }
      Sprite t = Resources.Load<Sprite>("Images/"+level);
      if(t != null){
        LevelImage.sprite = t;
      }
    }
    public void OnPointerClick(PointerEventData eventData) // 3
     {
       ConnectToGame();
     }
    public void ConnectToGame(){
      if(status == "Waiting"){
        Debug.Log("Connecting to " + level);
        netManager.networkAddress = ip;
        netManager.networkPort = port;
        netManager.StartClient();
      }else{
        Debug.Log("Not Connecting to " + level + ": " + status);
      }
    }
  
    // Update is called once per frame
    void Update () {
    
    }
}