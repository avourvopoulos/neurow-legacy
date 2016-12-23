using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
	public static Scoring SCORE;
	
	private static int waypoints = 0;
	
	private static float time;
	
	public Text score;
	public Text finalscore;
	public Text gameTime;

	public static string curr_score = "";
	public static float upTime = 0f;

	public static float duration;
	public static float initDuration;

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
		initDuration = duration;
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
				duration = initDuration;
			//	Debug.Log("durationIN "+duration);
			}
			else{
				Settings.Timeout = false;
			}

			//Debug.Log("duration "+duration);


			gameTime.text = duration.ToString("F0") + " s";

			curr_score = ""+waypoints;
			score.text = curr_score;
			finalscore.text = curr_score;
		}
	}
	
	public static void Add()
	{
		waypoints++;
		//	print(waypoints);
	}
}