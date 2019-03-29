using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 
using UnityEngine.Events;

public class GamePanelScript : MonoBehaviour {

    //We will put here reference to prefab in Editor
    public GameObject CellPrefab;
    public PNPNetworkDiscovery discoveryComp;
    public bool updating = true;
    // Use this for initialization
    void Start () {
      StartCoroutine(UpdateGames());
      PNPNetworkDiscovery[] components = GameObject.FindObjectsOfType<PNPNetworkDiscovery>();
      if(components.Length>0){
        discoveryComp = components[0];
      }
        
    }
    
    
    public IEnumerator UpdateGames(){
      while(true){
        yield return new WaitForSeconds(1f);
        if(updating){
          foreach (Transform child in transform) {
              GameObject.Destroy(child.gameObject);
          }
          foreach(KeyValuePair<string, GameDiscovery> gamePair in discoveryComp.discoveredGames)
          {
            //gamePair.Key|Value
            //Debug.Log(gamePair.Value); 
            GameObject newCell = Instantiate (CellPrefab) as GameObject;
            GamePanelInfoScript cellScript = newCell.GetComponent<GamePanelInfoScript>();
            //cellScript.value2 = "trololo";
            cellScript.GameName = gamePair.Value.name;
            cellScript.ip = gamePair.Value.ipAddress;
            cellScript.port = gamePair.Value.port;
            cellScript.status = gamePair.Value.status;
            cellScript.level = gamePair.Value.levelName;
            cellScript.players = gamePair.Value.players;
            
            cellScript.UpdateInfo();
            newCell.transform.SetParent(  this.gameObject.transform,  false);
          }
        }
      }
    }
  
    // Update is called once per frame
    void Update () {
    
    }
}