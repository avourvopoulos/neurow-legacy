using UnityEngine;
using System.Collections;

public class MoveHands : MonoBehaviour {

	public GameObject leftRow;
	public GameObject rightRow;

	// Use this for initialization
	void Start()
	{
		if (leftRow == null) {
			leftRow = GameObject.FindGameObjectWithTag ("LeftTarget");
		//	leftRow.transform.position = new Vector3(leftRow.transform.position.x, leftRow.transform.position.y, leftRow.transform.position.z);
		}
	
		if (rightRow == null) {
			rightRow = GameObject.FindGameObjectWithTag ("RightTarget");
		//	rightRow.transform.position = new Vector3(rightRow.transform.position.x, rightRow.transform.position.y, rightRow.transform.position.z);
		}

//		UpdatePosition();
	}
	
	// Update is called once per frame
	void Update()
	{
		UpdatePosition();
	}

	void UpdatePosition()
	{
		if (this.gameObject.name.Contains("Left") ){// && MoveBoat.left) {
			if(Settings.leapOn)
				this.gameObject.transform.position = new Vector3(leftRow.transform.position.x, leftRow.transform.position.y-2.8f, leftRow.transform.position.z);
			else
				this.gameObject.transform.position = new Vector3(leftRow.transform.position.x, leftRow.transform.position.y-2.8f, leftRow.transform.position.z);
			//	GameObject.FindGameObjectWithTag("LeftHand").transform.position = new Vector3(GameObject.FindGameObjectWithTag("LeftTarget").transform.position.x+0f, GameObject.FindGameObjectWithTag("LeftTarget").transform.position.y-3.0f, GameObject.FindGameObjectWithTag("LeftTarget").transform.position.z-0f);
		}

		if (this.gameObject.name.Contains("Right") ){// && MoveBoat.right) {
			this.gameObject.transform.position = new Vector3(rightRow.transform.position.x, rightRow.transform.position.y-0.0f, rightRow.transform.position.z);
			//	GameObject.FindGameObjectWithTag("RightHand").transform.position = new Vector3(GameObject.FindGameObjectWithTag("RightTarget").transform.position.x, GameObject.FindGameObjectWithTag("RightTarget").transform.position.y-1.0f, GameObject.FindGameObjectWithTag("RightTarget").transform.position.z);
		}
	}


}
