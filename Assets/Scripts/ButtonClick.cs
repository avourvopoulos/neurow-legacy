using UnityEngine;
using System.Collections;

public class ButtonClick : MonoBehaviour {

	public GameObject chooseModePanel;

	void Awake()
	{
//		chooseModePanel = GameObject.Find("ChooseMode");
//		chooseModePanel.SetActive (false);

		//unpause game if it was paused
		if(Time.timeScale == 0f)
		{
			Time.timeScale = 1f;
			audio.Play();
		}
	}

	public void mainMenu(){
		Application.LoadLevel ("MainMenu");
	}

	public void showCredits(){
		Application.LoadLevel ("Credits");
	}

	//load main game scene
	public void playGame(){
//		if(DisconnectionNotice.IsConnected())
			Application.LoadLevel("Game");
//		else
//		Application.LoadLevel(2);
	}

	//choose mode
	public void ChooseMode()
	{
		GameObject.Find("Panel").SetActive(false);
		chooseModePanel.SetActive(true);
	}

	//BCI Mode
	public void BCIMode()
	{
		chooseModePanel.SetActive(true);
		Application.LoadLevel(2);
	}

	//Hand Tracking Mode
	public void HandTrackingMode()
	{
		chooseModePanel.SetActive(true);
		Application.LoadLevel(3);
	}

	public void exitGame(){
	//	if (Application.loadedLevel != 1)
			Application.Quit ();
	}

	public void playSound()
	{
		audio.Play ();
	}

}
