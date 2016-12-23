using UnityEngine;
using System.Collections;

public class MoveBoat : MonoBehaviour {

	public static float boatspeed = 5f;//default = 5
	public static float turnspeed = 10f;
	public static float stoppingAngle = 45f;

	public Transform compass;

	public static bool left, right = false;

	// Use this for initialization
	void Start () {
		boatspeed = 5f;
	}
	
	// Update is called once per frame
	void Update () {

		//if(Input.GetKey(KeyCode.UpArrow))
		//transform.Translate(Vector3.forward * boatspeed * Time.deltaTime);
		if((compass.localEulerAngles.y >= (360 - stoppingAngle) && compass.localEulerAngles.y <= 360) || (compass.localEulerAngles.y >= 0 && compass.localEulerAngles.y <= (0 + stoppingAngle)))
			transform.Translate(Vector3.forward * boatspeed * Time.deltaTime);

	
		if (Input.GetKey (KeyCode.LeftArrow) || left) {
			left = true;
//			right = false;
			if(!Settings.reverseHands)
				transform.Rotate (Vector3.up * turnspeed * Time.deltaTime, Space.World);
			else
				transform.Rotate (Vector3.down * turnspeed * Time.deltaTime, Space.World);
		//	transform.Translate(Vector3.forward * boatspeed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.RightArrow) || right) {
//			left = false;
			right = true;
			if(!Settings.reverseHands)
				transform.Rotate (Vector3.down * turnspeed * Time.deltaTime, Space.World);
			else
				transform.Rotate (Vector3.up * turnspeed * Time.deltaTime, Space.World);
		//	transform.Translate(Vector3.forward * boatspeed * Time.deltaTime);
		}

		if(Input.GetKeyUp(KeyCode.LeftArrow))
			left = false;

		if(Input.GetKeyUp(KeyCode.RightArrow))
			right = false;

	}
}
