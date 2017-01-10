using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		Invoke ("loadMainMenu", 1);
	}
	
	// Update is called once per frame
	void Update () {


		//exit on escape or go to main menu
		if (Input.GetKey (KeyCode.Escape))
			Application.LoadLevel (0);


	
	}

	void loadMainMenu(){

		if (Application.loadedLevel == 0)
			Application.LoadLevel (1);
	}


}
