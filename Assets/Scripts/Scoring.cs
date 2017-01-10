using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
	public static Scoring SCORE;
	
	private static int waypoints = 0;
	
	private static float time;
	
	public Text score;
	public Text gameTime;

	public static string curr_score = "";
	public static float upTime = 0f;

	public static float duration;
	
	void Awake()
	{

//		if(SCORE != null)
//			GameObject.Destroy(SCORE);
//		else
//			SCORE = this;
//		
//		DontDestroyOnLoad(this);
	}

	public static void updateDuration(string dur) {

		duration = float.Parse (dur);

	//	Debug.Log("time "+time+" : "+"upTime "+upTime);
	}

	void Update()
	{


		if(score != null)
		{

			duration -= Time.deltaTime;
			if(duration < 0)
			{

				Settings.Timeout = true;
			//	Debug.Log("durationIN "+duration);
			}

			//Debug.Log("duration "+duration);


			gameTime.text = duration.ToString("F0") + " s";

			upTime += Time.deltaTime;
//			print(upTime.ToString("F0")+"\n");

			curr_score = ""+waypoints;
			score.text = curr_score;
		}
	}
	
	public static void Add()
	{
		waypoints++;
		//	print(waypoints);
	}
}