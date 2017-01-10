using UnityEngine;
using System.Collections;

public class HandCollision : MonoBehaviour {

	public static bool grabbed = false;
	public static string collidedHand;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(GrabbingHand.closedLeft && grabbed)
		{
			//Debug.Log("Turn Left!");
		}
		if(GrabbingHand.closedRight && grabbed)
		{
			//Debug.Log("Turn Right!");
		}
	
	//	Debug.Log (collidedHand);
	}


	void OnCollisionStay (Collision col)
	{

		if(col.gameObject.name.Contains("bone") || col.gameObject.name.Contains("palm"))
		{
			grabbed = true;

		//	Debug.Log (gameObject.name + " hand collision");

				collidedHand = gameObject.name;
		}
		else
		{
			grabbed = false;
			collidedHand = string.Empty;
		}
	}

	void OnCollisionExit (Collision col)
	{

		if(col.gameObject.name.Contains("bone") || col.gameObject.name.Contains("palm"))
		{
			grabbed = false;
			collidedHand = string.Empty;
		}
	}

}
