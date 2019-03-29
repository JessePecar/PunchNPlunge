using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuWalkthrough : MonoBehaviour {
	public GameObject mainMenu, options, play, credits, quit, landing;
	public void Start(){
        landing.SetActive(true);
        mainMenu.SetActive(false);
        play.SetActive(false);
        options.SetActive(false);
        credits.SetActive(false);
        quit.SetActive(false);
	}
	public void goToMenu(){
        landing.SetActive(false);
        mainMenu.SetActive(true);
        play.SetActive(false);
        options.SetActive(false);
        credits.SetActive(false);
        quit.SetActive(false);
	}
	public void goToPlay(){
		landing.SetActive(false);
        mainMenu.SetActive(true);
        play.SetActive(true);
        options.SetActive(false);
        credits.SetActive(false);
        quit.SetActive(false);
	}
	public void goToOptions(){
        landing.SetActive(false);
        mainMenu.SetActive(true);
        play.SetActive(false);
        options.SetActive(true);
        credits.SetActive(false);
        quit.SetActive(false);
	}
	public void goToCredits(){
        landing.SetActive(false);
        mainMenu.SetActive(true);
        play.SetActive(false);
        options.SetActive(false);
        credits.SetActive(true);
        quit.SetActive(false);
	}
	public void goToQuit(){
        landing.SetActive(false);
        mainMenu.SetActive(true);
        play.SetActive(false);
        options.SetActive(false);
        credits.SetActive(false);
        quit.SetActive(true);
	}
	public void quitThisBitch(){
		Application.Quit();
	}
}
