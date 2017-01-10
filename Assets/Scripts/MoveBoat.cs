using UnityEngine;
using System.Collections;

public class MoveBoat : MonoBehaviour {

	[SerializeField]
	public static float boatspeed = 0f;//default = 5
	public static float turnspeed = 25f;
	public static float stoppingAngle = 45f;

	public Transform compass;
	public Transform target;

	public static bool left, right, front = false;

	string saveStr = "";

	string trajectory = "";

	float dampening = 0.05f;
	float translationStep = 0f;
	float rotationStep = 0f;

	Vector3 turn;

	System.DateTime a;
	double freq = 0.0;


	public GameObject NewPosition;

	// Use this for initialization
	void Start()
	{
		boatspeed = 0f;
	}

	void updateFrequency()
	{
		if(freq != 0)
		{
			freq = (0.95 * freq) + (0.05 * (System.DateTime.Now.Subtract(a).TotalSeconds));
			a = System.DateTime.Now;
		}
		else
		{
			freq = 1.0 / 16.0;
			a = System.DateTime.Now;
		}
		
//		print(freq);
	}

	void FixedUpdate () {

		if(Input.GetKey (KeyCode.UpArrow) || front)
		{
			updateFrequency();

			front = false;
			RotateRow.left = false;
			RotateRow.right = false;



//			print("dist: " + Vector3.Distance(transform.position, NewPosition.transform.position));

			if(!Settings.isTraining)
			{
				transform.position = NewPosition.transform.position;
				transform.rotation = NewPosition.transform.rotation;
				
				if((compass.localEulerAngles.y >= (360 - stoppingAngle) && compass.localEulerAngles.y <= 360) || (compass.localEulerAngles.y >= 0 && compass.localEulerAngles.y <= (0 + stoppingAngle)))
					NewPosition.transform.Translate(Vector3.forward * boatspeed * dampening);
				
				logTrajectory();
			}
		}

//		if((compass.localEulerAngles.y >= (360 - stoppingAngle) && compass.localEulerAngles.y <= 360) || (compass.localEulerAngles.y >= 0 && compass.localEulerAngles.y <= (0 + stoppingAngle)))
//			transform.Translate(Vector3.forward * boatspeed * Time.deltaTime);

		if (Input.GetKey (KeyCode.LeftArrow) || left)
		{
			updateFrequency();

			left = false;
//			right = false;
			RotateRow.left = true;
			RotateRow.right = false;

			if(!Settings.isTraining)
			{
				transform.position = NewPosition.transform.position;
				transform.rotation = NewPosition.transform.rotation;

				if(!Settings.reverseHands)
				{
					turn = Vector3.up * turnspeed * dampening;
					NewPosition.transform.Rotate (turn, Space.World);
	//				NewPosition.transform.Translate(Vector3.right * boatspeed * dampening);
				}
				else
				{
					turn = Vector3.down * turnspeed * dampening;
					NewPosition.transform.Rotate (turn, Space.World);
	//				NewPosition.transform.Translate(Vector3.left * boatspeed * dampening);
					saveStr += System.DateTime.Now.Ticks + "," + turn.y + "\n";
				}

				if((compass.localEulerAngles.y >= (360 - stoppingAngle) && compass.localEulerAngles.y <= 360) || (compass.localEulerAngles.y >= 0 && compass.localEulerAngles.y <= (0 + stoppingAngle)))
					NewPosition.transform.Translate(Vector3.forward * boatspeed * dampening);

				logTrajectory();
			}
		}
		if (Input.GetKey (KeyCode.RightArrow) || right)
		{
			updateFrequency();

//			left = false;
			right = false;
			RotateRow.left = false;
			RotateRow.right = true;

			if(!Settings.isTraining)
			{
				transform.position = NewPosition.transform.position;
				transform.rotation = NewPosition.transform.rotation;

				if(!Settings.reverseHands)
				{
					turn = Vector3.down * turnspeed * dampening;
					NewPosition.transform.Rotate (turn, Space.World);
	//				NewPosition.transform.Translate(Vector3.left * boatspeed * dampening);
				}
				else
				{
					turn = Vector3.up * turnspeed * dampening;
					NewPosition.transform.Rotate (turn, Space.World);
	//				NewPosition.transform.Translate(Vector3.right * boatspeed * dampening);
					saveStr += System.DateTime.Now.Ticks + "," + turn.y + "\n";
				}

				if((compass.localEulerAngles.y >= (360 - stoppingAngle) && compass.localEulerAngles.y <= 360) || (compass.localEulerAngles.y >= 0 && compass.localEulerAngles.y <= (0 + stoppingAngle)))
					NewPosition.transform.Translate(Vector3.forward * boatspeed * dampening);

				logTrajectory();
			}
		}

		if(Input.GetKeyUp(KeyCode.LeftArrow))
			left = false;

		if(Input.GetKeyUp(KeyCode.RightArrow))
			right = false;

		if(Input.GetKeyUp(KeyCode.UpArrow))
			front = false;
	}

	void Update()
	{
		if (Settings.isTraining)
		{
			transform.Translate(Vector3.forward * boatspeed * Time.deltaTime, Space.Self);

			if(RotateRow.right)
			{
				transform.Rotate(Vector3.up * boatspeed * Time.deltaTime, Space.Self);
			}
			else
			if(RotateRow.left)
			{
				transform.Rotate(Vector3.down * boatspeed * Time.deltaTime, Space.Self);
			}
		}
		else
		{
			translationStep = (Vector3.forward * boatspeed * dampening).z * Time.deltaTime / (float)freq;
			transform.position = Vector3.MoveTowards(transform.position, NewPosition.transform.position, translationStep);
			
			rotationStep = Quaternion.Angle (Quaternion.Euler (new Vector3 (0f, 0f, 0f)), Quaternion.Euler (turn)) * Time.deltaTime / (float)freq;
			transform.rotation = Quaternion.RotateTowards(transform.rotation, NewPosition.transform.rotation, rotationStep);
		}
	}

	void logTrajectory()
	{
		trajectory += System.DateTime.Now.Ticks + "," + Scoring.upTime.ToString("F0") + "," + Scoring.curr_score + "," + transform.position.x + "," + transform.position.z + "," + target.position.x + "," + target.position.z + "\n";
	}

	void OnDisable()
	{
		string fileName = "trajectory_" + System.DateTime.Now.Year + "-" + System.DateTime.Now.Month + "-" + System.DateTime.Now.Day + "_" + System.DateTime.Now.Hour + "-" + System.DateTime.Now.Minute + "-" + System.DateTime.Now.Second;
		System.IO.File.AppendAllText(Settings.logDir+"\\" + fileName + ".txt", trajectory);
	}
}
