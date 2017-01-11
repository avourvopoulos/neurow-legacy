using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
	public GameObject panel;

	public GameObject gui;
	public GameObject waypoint;
	public GameObject arrow;
	
	public GameObject leap, hands, HTCVive;
	
	public static bool leapOn = false;
	public static bool isTraining = false;

	public static bool noAPE;

	public static GameObject MainCamera, OculusCamera;

//General Settings -------------------------
//	static float boatSpeed;
//	static float turnSpeed;
//	static float cutOffAngle;
	static bool audioOn;
	public static bool reverseHands;
//------------------------------------------

//Network Settings -------------------------
	static string ip;
	static string port;
	static bool useNetwork;
//------------------------------------------

//VR Settings -------------------------
	public static bool oculusRift = false;
	public static bool htcvive = false;
//	static bool leapMotion;
//------------------------------------------

//Haptics Settings -------------------------
	public static string comPort;
	public static float m1Power;
	public static float m2Power;
	public static float m3Power = 27; // For study only
	public static float m4Power;
	public static float m5Power;
	public static float m6Power = 45; // For study only
//------------------------------------------

	public static string duration;
	public static bool Timeout = false;

	public static string logDir = string.Empty;
	public InputField dir;

	public static AudioSource waveSound;

	void Start(){
		
		waveSound = GetComponent<AudioSource>();

		getLogDirectory ();

		duration = "480";
		Scoring.updateDuration(duration);

		MainCamera = GameObject.Find("Main Camera");
		OculusCamera = GameObject.Find("OVRCameraController");

//		OVRDevice.ResetOrientation(0);

		OculusCamera.SetActive (false);
//		isTraining = true;

		if(Application.loadedLevelName == "Game") // BCI MODE
		{
			isTraining = false;
			leapOn = false;
			noAPE = true;
			reverseHands = true;

			// Remove this after trial
//			oculusRift = true;
//			switchCamera(oculusRift);

//			gamePause();
		}
		else if(Application.loadedLevelName == "Game_With_APE") // BCI MODE
		{
			isTraining = false;
			leapOn = false;
			noAPE = false;
			reverseHands = true;

			// Remove this after trial
//			oculusRift = true;
//			switchCamera(oculusRift);

			//			gamePause();
		}
		else if(Application.loadedLevelName == "Leap") // HAND TRACKING MODE
		{
//			isTraining = true;
			leapOn = true;
			reverseHands = false;
		}
//		else if (Application.loadedLevelName == "Training")
//		{
//			isTraining = true;
//			leapOn = false;
//		}



		gamePause ();
	}


	public void gamePause(){
		gui.SetActive (false);
		panel.SetActive (true);
		Time.timeScale = 0f;
//		if(audioOn)
//		audio.Pause();
	}

	public void gameResume(){
		gui.SetActive (true);
		panel.SetActive (false);
		Time.timeScale = 1f;
	//	audio.Play();
	}

	void getLogDirectory(){
		logDir = Application.persistentDataPath;
		dir.text = logDir;
	}

	// Update is called once per frame
	void Update () {

		// temp
//		if (Timeout && !isTraining)
//			gamePause();


//		Debug.Log ("leap: " + DisconnectionNotice.IsConnected());

//		if(Application.loadedLevel == 2 || Application.loadedLevel == 3)
//		{
			//enable/disable settings panel
			if (Input.GetKey (KeyCode.P)) {
				gamePause();
			}
			if (Input.GetKey (KeyCode.L)) {
				gameResume();
			}
			
			if(Input.GetKey(KeyCode.Escape))
			{
				Application.LoadLevel("MainMenu");
			}

			if(Input.GetKey(KeyCode.LeftControl))
			{
				print("Reset Oculus Orientation!\n");
				OVRDevice.ResetOrientation(0);
			}
			
//			if (Input.GetKey (KeyCode.T))
//			{
//				Application.LoadLevel("Training");
////				isTraining = true;
////				leapOn = true;
//			}
//			if (Input.GetKey (KeyCode.F1))
//			{
//				Application.LoadLevel("Game_No_APE");
//			}
//			if (Input.GetKey (KeyCode.F2))
//			{
//			Application.LoadLevel("Game_With_APE");
//			}
//			if (Input.GetKey (KeyCode.G)) {
//				isTraining = false;
//				leapOn = false;
//			}
			
			
			//enable/disable BCI training mode
			if (isTraining) {
				//			gui.SetActive (false);
				waypoint.SetActive (false);
				arrow.SetActive (false);
			}
			
			if (!isTraining) {
				//			gui.SetActive (true);
				waypoint.SetActive (true);
				arrow.SetActive (true);
			}
			
			//enable/disable leapmotion
			if (leapOn) {
				leap.SetActive (true);
			waypoint.SetActive (true);
			arrow.SetActive (true);
				//	hands.SetActive (false);
			}
			if (!leapOn) {
				leap.SetActive (false);
				hands.SetActive (true);
			}

			if (htcvive){
				HTCVive.SetActive(true);
				MainCamera.SetActive(false);}
			else{
				HTCVive.SetActive(false);
				MainCamera.SetActive(true);}
			//}
	}


	public static void updateVariables(string name, string value)
	{
		switch(name)
		{
		case "SendIPAddress":
//			print(name+": "+value);
			ip = value;
			break;
		case "SendPort":
//			print(name+": "+value);
			port = value;
			break;
		case "LocalIPAddress":
//			print(name+": "+value);
			ip = value;
			break;
		case "LocalPort":
//			print(name+": "+value);
			UDPReceive.portField = value;
			break;
		case "ComPort":
//			print(name+": "+value);
			comPort = value;
			break;
		case "Duration":
			duration = value;
			Scoring.updateDuration(duration);
			Timeout=false;
			break;
		}
	}

	public static void updateVariables(string name, bool value)
	{
		switch(name)
		{
		case "Audio":
//			print(name+": "+value);
			audioOn = value;
			if(audioOn)
				waveSound.Play();
			else
				waveSound.Pause();
			break;
		case "ReverseHands":
			print(name+": "+value);
			reverseHands = value;
			break;
		case "Use":
//			print(name+": "+value);
			useNetwork = value;
			break;
		case "OculusRift":
//			print(name+": "+value);
			oculusRift = value;
			switchCamera(oculusRift);
			break;
		case "LeapMotion":
//			print(name+": "+value);
			Settings.leapOn = value;
			if(value)
				Application.LoadLevel("Leap");
			else{
				Application.LoadLevel("Game");
			}
			break;
		case "HTCVive":
			//print(name+": "+value);
			htcvive = value;
			break;
		case "Training":
			isTraining = value;
			break;
		}
	}

	public static void switchCamera(bool oculus){

		if (oculus) {
			OculusCamera.SetActive(true);
			MainCamera.SetActive(false);
		}
		else{
			OculusCamera.SetActive(false);
			MainCamera.SetActive(true);
		}
	}


	public static void updateVariables(string name, float value)
	{
//		print(name+": "+value);

		switch(name)
		{
		case "BoatSpeed":
//			print(name+": "+value);
			MoveBoat.boatspeed = value;
			break;
		case "TurnSpeed":
//			print(name+": "+value);
			MoveBoat.turnspeed = value;
			break;
		case "CutOffAngle":
//			print(name+": "+value);
			MoveBoat.stoppingAngle = value;
			break;
		case "M1":
//			print(name+": "+value);
			m1Power = value;
			break;
		case "M2":
//			print(name+": "+value);
			m2Power = value;
			break;
		case "M3":
//			print(name+": "+value);
			m3Power = value;
			break;
		case "M4":
//			print(name+": "+value);
			m4Power = value;
			break;
		case "M5":
//			print(name+": "+value);
			m5Power = value;
			break;
		case "M6":
//			print(name+": "+value);
			m6Power = value;
			break;
		}
	}

}
