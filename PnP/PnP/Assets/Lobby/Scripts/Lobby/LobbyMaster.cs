using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Prototype.NetworkLobby
{
    //Player entry in the lobby. Handle selecting color/setting name & getting ready for the game
    //Any LobbyHook can then grab it and pass those value to the game player prefab (see the Pong Example in the Samples Scenes)
    public class LobbyMaster : NetworkLobbyPlayer
    {
        //static Color[] Colors = new Color[] { Color.magenta, Color.red, Color.cyan, Color.blue, Color.green, Color.yellow };
        //These are sprites representing the Player Characters
        public Sprite[] Sprites;
        //used on server to avoid assigning the same color to two player
        static List<int> _colorInUse = new List<int>();
        public int[] SceneIndices;

        public Button colorButton;
        public InputField nameInput;
        public Button readyButton;
        public Button waitingPlayerButton;
        public Button removePlayerButton;

        public GameObject localIcone;
        public GameObject remoteIcone;

        //OnMyName function will be invoked on clients when server change the value of playerName
        
        //This is the Index of the sprite of the player to be used.
        [SyncVar(hook = "OnMyColor")]
        public int playerColor;

        public Color OddRowColor = new Color(10.0f / 255.0f, 10.0f / 255.0f, 10.0f / 255.0f, 1f);
        public Color EvenRowColor = new Color(45.0f / 255.0f, 45.0f / 255.0f, 45.0f / 255.0f, 1f);

        static Color JoinColor = new Color(255.0f/255.0f, 0.0f, 101.0f/255.0f,1.0f);
        static Color NotReadyColor = new Color(34.0f / 255.0f, 44 / 255.0f, 55.0f / 255.0f, 1.0f);
        static Color ReadyColor = new Color(0.0f, 204.0f / 255.0f, 204.0f / 255.0f, 1.0f);
        static Color TransparentColor = new Color(0, 0, 0, 0);

        //static Color OddRowColor = new Color(250.0f / 255.0f, 250.0f / 255.0f, 250.0f / 255.0f, 1.0f);
        //static Color EvenRowColor = new Color(180.0f / 255.0f, 180.0f / 255.0f, 180.0f / 255.0f, 1.0f);


        public override void OnClientEnterLobby()
        {
            //base.OnClientEnterLobby();

            //setup the player data on UI. The value are SyncVar so the player
            //will be created with the right value currently on server
            
            if(isServer)
            {
                SetupLocalPlayer();
            }
            else
            {
                SetupOtherPlayer();
            }
            OnMyColor(0);
            colorButton.interactable = (false);
            readyButton.enabled = (false);
            waitingPlayerButton.enabled = (false);
            removePlayerButton.enabled = (false);
        }
        
        void SetupOtherPlayer()
        {
            
        }

        void SetupLocalPlayer()
        {
            colorButton.interactable = (true);
            colorButton.onClick.RemoveAllListeners();
            colorButton.onClick.AddListener(OnColorClicked);
        }
        

        public override void OnStartAuthority()
        {
            base.OnStartAuthority();

            //if we return from a game, color of text can still be the one for "Ready"
           

           
        }

        void ChangeReadyButtonColor(Color c)
        {
            ColorBlock b = readyButton.colors;
            b.normalColor = c;
            b.pressedColor = c;
            b.highlightedColor = c;
            b.disabledColor = c;
            readyButton.colors = b;
        }

        public void OnMyColor(int newColor)
        {
            playerColor = newColor;
            colorButton.GetComponent<Image>().sprite = Sprites[playerColor];
        }

        //===== UI Handler

        //Note that those handler use Command function, as we need to change the value on the server not locally
        //so that all client get the new value throught syncvar
        public void OnColorClicked()
        {
            CmdColorChange();
        }

       
        //====== Server Command

        //This is basically changing the sprite and playerColor of the LobbyPlayer
        [Command]
        public void CmdColorChange()
        {
            int idx = System.Array.IndexOf(Sprites, playerColor);

            int inUseIdx = _colorInUse.IndexOf(idx);

            if (idx < 0) idx = 0;

            idx = (idx + 1) % Sprites.Length;

            bool alreadyInUse = false;

            //do
            {
                alreadyInUse = false;
                for (int i = 0; i < _colorInUse.Count; ++i)
                {
                    if (_colorInUse[i] == idx)
                    {//that color is already in use
                        alreadyInUse = true;
                        idx = (idx + 1) % Sprites.Length;
                        Debug.Log(idx);
                    }
                }
            }
            //while (alreadyInUse);

            if (inUseIdx >= 0)
            {//if we already add an entry in the colorTabs, we change it
                _colorInUse[inUseIdx] = idx;
            }
            else
            {//else we add it
                _colorInUse.Add(idx);
            }

            playerColor = idx;
        }
    }
}
