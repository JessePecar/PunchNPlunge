using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class optionsScript : MonoBehaviour {

	// Use this for initialization
	int nextSkin;
	public Image skin1, skin2, skin3, skin4;
	public int SelectedSkin;
	void Start(){
		nextSkin = 0;
		changeSkin();
	}

	public void moveRight(){
		if(nextSkin < 3){
			nextSkin++;
		}
		else{
			nextSkin = 0;
		}
        changeSkin();
	}
	public void moveLeft(){
		if(nextSkin > 0){
			nextSkin--;
		}
		else{
			nextSkin = 3;
		}
        changeSkin();
	}
	void changeSkin(){
		switch(nextSkin){
            //if nextSkin = 0
            case 0:
            	skin1.enabled = true;
                skin2.enabled = false;
                skin4.enabled = false;
                skin3.enabled = false;
                SelectedSkin = 1;
                break;
			//if nextSkin = 1
			case 1:
                skin1.enabled = false;
				skin2.enabled = true;
                skin4.enabled = false;
                skin3.enabled = false;
                SelectedSkin = 2;
				break;
            //if nextSkin = 2
            case 2:
                skin1.enabled = false;
            	skin3.enabled = true;
                skin2.enabled = false;
                skin4.enabled = false;
                SelectedSkin = 3;
                break;
            //if nextSkin = 3
            case 3:
                skin1.enabled = false;
            	skin4.enabled = true;
                skin2.enabled = false;
                skin3.enabled = false;
                SelectedSkin = 4;
                break;
            //if nextSkin = anything it isn't supposed to be
            default:
            	skin1.enabled = true;
                skin2.enabled = false;
                skin4.enabled = false;
                skin3.enabled = false;
                SelectedSkin = 1;
			break;
			
		}
	}
}
