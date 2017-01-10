using UnityEngine;
using System.Collections;

public class WayPoints : MonoBehaviour
{
	public Transform player;
	Transform waypoint;

//	string trajectory = "";

	int[] directions = new int[] {1, 1, 1, -1, -1, -1};
	int direction = 0;
	int i = 0;
	int rand;

	Vector3 pos1 = new Vector3(-55, 1, 200);
	Vector3 pos2 = new Vector3(0, 1, 400);
	Vector3 pos3 = new Vector3(55, 1, 600);
	Vector3 pos4 = new Vector3(0, 1, 800);
	Vector3 pos5 = new Vector3(55, 1, 1000);
	Vector3 pos6 = new Vector3(0, 1, 1200);

	Vector3[] targetPositions1;
	Vector3[] targetPositions2;

	// Use this for initialization
	void Start ()
	{
		targetPositions1 = new Vector3[]{pos1, pos2, pos3, pos4, pos5, pos6};

		pos1 = new Vector3(55, 1, 200);
		pos2 = new Vector3(0, 1, 400);
		pos3 = new Vector3(-55, 1, 600);
		pos4 = new Vector3(0, 1, 800);
		pos5 = new Vector3(-55, 1, 1000);
		pos6 = new Vector3(0, 1, 1200);

		targetPositions2 = new Vector3[]{pos1, pos2, pos3, pos4, pos5, pos6};


		waypoint = gameObject.transform;
//		NewPosition();
		newPositionFixed();
	}
	
	// Update is called once per frame
//	void FixedUpdate()
//	{
//		trajectory += System.DateTime.Now.Ticks + "," + Scoring.upTime.ToString("F0") + "," + Scoring.curr_score + "," + player.position.x + "," + player.position.z + "," + transform.position.x + "," + transform.position.z + "\n";
//	}

	void OnTriggerEnter(Collider other)
	{
		if(other.collider.tag == "Player")
		{
			Scoring.Add();
//			NewPosition();
			newPositionFixed();
			audio.Play(); 
		}
	}

	void newPositionFixed()
	{
		if (i <= 5)
		{
			if(Settings.noAPE)
				waypoint.position = targetPositions1[i];
			else
				waypoint.position = targetPositions2[i];

			i++;
		}
		else
		{
			//	end game session
			//			Destroy(gameObject);
		}
	}

	void NewPosition()
	{
		direction = 0;
		while(direction == 0 && i <= 5)
		{
			rand = Random.Range(0, 6);
			print (i + " | " + rand+"\n");
			direction = directions[rand];
			directions[rand] = 0;
			if(direction != 0)
				i++;
		}

		if (direction != 0)
		{	//only spawn 6 targets
			Vector3 playerPos = player.position;
			Vector3 playerDirection = player.forward;
			//		float spawnDistance = Random.Range(180, 250);
			float spawnDistance = 200;
			waypoint.rotation = player.rotation;
			waypoint.position = playerPos + playerDirection * spawnDistance;
			//		waypoint.Translate(Vector3.right * Random.Range(30, 65) * dir, Camera.main.transform);
			spawnDistance = 55;
			waypoint.Translate (Vector3.right * spawnDistance * direction, Camera.main.transform);
			waypoint.position = new Vector3 (waypoint.position.x, 0.8f,waypoint.position.z);
		}
		else
		{
			//	end game session
//			Destroy(gameObject);
		}
	}

//	void OnDisable()
//	{
//		string fileName = "trajectory_" + System.DateTime.Now.Year + "-" + System.DateTime.Now.Month + "-" + System.DateTime.Now.Day + "_" + System.DateTime.Now.Hour + "-" + System.DateTime.Now.Minute + "-" + System.DateTime.Now.Second;
////		print(Application.persistentDataPath);
//		System.IO.File.AppendAllText(Settings.logDir+"\\" + fileName + ".txt", trajectory);
//	}

}
