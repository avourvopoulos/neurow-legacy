using UnityEngine;
using System.Collections;

public class RotateRow : MonoBehaviour {

	public static  float speed = 300f;

	public float angle, angle1;
	public float xDiff, yDiff;

	float angleOffset = 0f;
	Quaternion initRotationL, initRotationR;

	public Transform targetHand;

	// Use this for initialization
	void Start () {
	
		angleOffset = 90.0f;
		initRotationL = GameObject.FindGameObjectWithTag ("LeftRow").transform.localRotation;
		initRotationR = GameObject.FindGameObjectWithTag ("RightRow").transform.localRotation;

	}
	
	// Update is called once per frame
	void Update () {
	
		if (GameObject.Find ("rigidHand") != null)
			targetHand = GameObject.Find ("rigidHand").transform;
		else
			targetHand = targetHand;

//		if (this.gameObject.transform.rotation.y > 60 && this.gameObject.transform.rotation.y < 80) {
//			audio.Play ();
//		}

		//transform.Rotate(Vector3.right * speed * Time.deltaTime);

//		Debug.Log(Settings.leapOn+" :: "+GrabbingHand.closedLeft+" :: "+HandCollision.collidedHand);

//		if (Settings.leapOn) {
//
////			if ((GrabbingHand.closedLeft && HandCollision.collidedHand == "Lpivot")) {
//			if ((GrabbingHand.closedLeft)) {
//
//				xDiff = (GameObject.FindGameObjectWithTag ("rigidHand").transform.position.z - GameObject.FindGameObjectWithTag ("Lpoint").transform.position.z);
//				yDiff = (GameObject.FindGameObjectWithTag ("rigidHand").transform.position.y - GameObject.FindGameObjectWithTag ("Lpoint").transform.position.y);	
//				angle = (float)Mathf.Atan2(yDiff, xDiff) * 180.0f / (float)Mathf.PI;
//				angle1 = Mathf.Abs (angle);
//
//				GameObject.FindGameObjectWithTag ("LeftRow").transform.rotation = Quaternion.Euler(-angle + angleOffset, initRotationL.eulerAngles.y, initRotationL.eulerAngles.z);
//
//				MoveBoat.left = true;
//				MoveBoat.right = false;
//			}
//
////			else if ((GrabbingHand.closedRight && HandCollision.collidedHand.Contains ("Rpivot"))) {
//			else if ((GrabbingHand.closedRight)) {
//
//				xDiff = (GameObject.FindGameObjectWithTag ("rigidHand").transform.position.z - GameObject.FindGameObjectWithTag ("Rpoint").transform.position.z);
//				yDiff = (GameObject.FindGameObjectWithTag ("rigidHand").transform.position.y - GameObject.FindGameObjectWithTag ("Rpoint").transform.position.y);	
//				angle = (float)Mathf.Atan2(yDiff, xDiff) * 180.0f / (float)Mathf.PI;
//				angle1 = Mathf.Abs (angle);
//				
//				GameObject.FindGameObjectWithTag ("RightRow").transform.rotation = Quaternion.Euler(-angle + angleOffset, initRotationR.eulerAngles.y, initRotationR.eulerAngles.z);
//
//				MoveBoat.right = true;
//				MoveBoat.left = false;
//			}
//			else{
//
//				GameObject.FindGameObjectWithTag ("LeftRow").transform.rotation = Quaternion.Euler(initRotationL.eulerAngles.x, initRotationL.eulerAngles.y, initRotationL.eulerAngles.z);
//				GameObject.FindGameObjectWithTag ("RightRow").transform.rotation = Quaternion.Euler(initRotationR.eulerAngles.x, initRotationR.eulerAngles.y, initRotationR.eulerAngles.z);
//				MoveBoat.right = false;
//				MoveBoat.left = false;
//			}
//
//		} 

		if (Settings.leapOn) {
			
//			if ((GrabbingHand.closedLeft && HandCollision.collidedHand == "Lpivot")) {
			if ((GrabbingHand.closedLeft)) {

				if(Settings.oculusRift)
				{
					GameObject.Find("CameraLeft").camera.cullingMask = (0 << LayerMask.NameToLayer("RowHandRight") | 1 << LayerMask.NameToLayer("RowHandLeft") | 0 << LayerMask.NameToLayer("LeapMotion") | 1 << LayerMask.NameToLayer("Water") | 1 << LayerMask.NameToLayer("UI") | 1 << LayerMask.NameToLayer("TransparentFX") | 1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Ignore Raycast"));
					GameObject.Find("CameraRight").camera.cullingMask = (0 << LayerMask.NameToLayer("RowHandRight") | 1 << LayerMask.NameToLayer("RowHandLeft") | 0 << LayerMask.NameToLayer("LeapMotion") | 1 << LayerMask.NameToLayer("Water") | 1 << LayerMask.NameToLayer("UI") | 1 << LayerMask.NameToLayer("TransparentFX") | 1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Ignore Raycast"));
					
				}
				else
				{
					Camera.main.cullingMask = (1 << LayerMask.NameToLayer("RowHandLeft") | 0 << LayerMask.NameToLayer("RowHandRight") | 0 << LayerMask.NameToLayer("LeapMotion") | 1 << LayerMask.NameToLayer("Water") | 1 << LayerMask.NameToLayer("UI") | 1 << LayerMask.NameToLayer("TransparentFX") | 1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Ignore Raycast"));
				}

//				xDiff = (GameObject.FindGameObjectWithTag ("rigidHand").transform.position.z - GameObject.FindGameObjectWithTag ("Lpoint").transform.position.z);
//				yDiff = (GameObject.FindGameObjectWithTag ("rigidHand").transform.position.y - GameObject.FindGameObjectWithTag ("Lpoint").transform.position.y);	
//				angle = (float)Mathf.Atan2(yDiff, xDiff) * 180.0f / (float)Mathf.PI;
//				angle1 = Mathf.Abs (angle);
				
//				GameObject.FindGameObjectWithTag ("LeftRow").transform.rotation = Quaternion.Euler(-angle + angleOffset, initRotationL.eulerAngles.y, initRotationL.eulerAngles.z);
//				GameObject.FindGameObjectWithTag ("LeftRow").transform.rotation = Quaternion.Euler(-angle + angleOffset, GameObject.FindGameObjectWithTag ("LeftHand").transform.rotation.eulerAngles.y, GameObject.FindGameObjectWithTag ("LeftHand").transform.rotation.eulerAngles.z);
				GameObject.FindGameObjectWithTag ("LeftRow").transform.LookAt(GameObject.FindGameObjectWithTag ("rigidHand").transform);
//				if(angle > 120f)
//				{
				if(!Settings.reverseHands){//reverse hands
					MoveBoat.right = false;
					MoveBoat.left = true;
				}
				else{
					MoveBoat.right = true;
					MoveBoat.left = false;
				}
//				}
			}
			
//			else if ((GrabbingHand.closedRight && HandCollision.collidedHand.Contains ("Rpivot"))) {
			else if ((GrabbingHand.closedRight)) {

				if(Settings.oculusRift)
				{
					GameObject.Find("CameraLeft").camera.cullingMask = (1 << LayerMask.NameToLayer("RowHandRight") | 0 << LayerMask.NameToLayer("RowHandLeft") | 0 << LayerMask.NameToLayer("LeapMotion") | 1 << LayerMask.NameToLayer("Water") | 1 << LayerMask.NameToLayer("UI") | 1 << LayerMask.NameToLayer("TransparentFX") | 1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Ignore Raycast"));
					GameObject.Find("CameraRight").camera.cullingMask = (1 << LayerMask.NameToLayer("RowHandRight") | 0 << LayerMask.NameToLayer("RowHandLeft") | 0 << LayerMask.NameToLayer("LeapMotion") | 1 << LayerMask.NameToLayer("Water") | 1 << LayerMask.NameToLayer("UI") | 1 << LayerMask.NameToLayer("TransparentFX") | 1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Ignore Raycast"));

				}
				else
				{
					Camera.main.cullingMask = (1 << LayerMask.NameToLayer("RowHandRight") | 0 << LayerMask.NameToLayer("RowHandLeft") | 0 << LayerMask.NameToLayer("LeapMotion") | 1 << LayerMask.NameToLayer("Water") | 1 << LayerMask.NameToLayer("UI") | 1 << LayerMask.NameToLayer("TransparentFX") | 1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Ignore Raycast"));
				}
				
//				xDiff = (GameObject.FindGameObjectWithTag ("rigidHand").transform.position.z - GameObject.FindGameObjectWithTag ("Rpoint").transform.position.z);
//				yDiff = (GameObject.FindGameObjectWithTag ("rigidHand").transform.position.y - GameObject.FindGameObjectWithTag ("Rpoint").transform.position.y);	
//				angle = (float)Mathf.Atan2(yDiff, xDiff) * 180.0f / (float)Mathf.PI;
//				angle1 = Mathf.Abs (angle);
				
//				GameObject.FindGameObjectWithTag ("RightRow").transform.rotation = Quaternion.Euler(-angle + angleOffset, initRotationR.eulerAngles.y, initRotationR.eulerAngles.z);
//				GameObject.FindGameObjectWithTag ("RightRow").transform.rotation = Quaternion.Euler(-angle + angleOffset, GameObject.FindGameObjectWithTag ("RightRow").transform.rotation.eulerAngles.y, GameObject.FindGameObjectWithTag ("RightRow").transform.rotation.eulerAngles.z);
				GameObject.FindGameObjectWithTag ("RightRow").transform.LookAt(GameObject.FindGameObjectWithTag ("rigidHand").transform);
//				if(angle > 120f)
//				{
				if(!Settings.reverseHands){//reverse hands
					MoveBoat.right = true;
					MoveBoat.left = false;
				}
				else{
					MoveBoat.right = false;
					MoveBoat.left = true;
				}
//				}
			}
			else{

				if(Settings.oculusRift)
				{
					GameObject.Find("CameraLeft").camera.cullingMask = (0 << LayerMask.NameToLayer("RowHandLeft") | 0 << LayerMask.NameToLayer("RowHandRight") | 1 << LayerMask.NameToLayer("LeapMotion") | 1 << LayerMask.NameToLayer("Water") | 1 << LayerMask.NameToLayer("UI") | 1 << LayerMask.NameToLayer("TransparentFX") | 1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Ignore Raycast"));
					GameObject.Find("CameraRight").camera.cullingMask  = (0 << LayerMask.NameToLayer("RowHandLeft") | 0 << LayerMask.NameToLayer("RowHandRight") | 1 << LayerMask.NameToLayer("LeapMotion") | 1 << LayerMask.NameToLayer("Water") | 1 << LayerMask.NameToLayer("UI") | 1 << LayerMask.NameToLayer("TransparentFX") | 1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Ignore Raycast"));
				}
				else
					{

					Camera.main.cullingMask = (0 << LayerMask.NameToLayer("RowHandLeft") | 0 << LayerMask.NameToLayer("RowHandRight") | 1 << LayerMask.NameToLayer("LeapMotion") | 1 << LayerMask.NameToLayer("Water") | 1 << LayerMask.NameToLayer("UI") | 1 << LayerMask.NameToLayer("TransparentFX") | 1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Ignore Raycast"));
					}

//				GameObject.FindGameObjectWithTag ("LeftRow").transform.rotation = initRotationL;
//				GameObject.FindGameObjectWithTag ("RightRow").transform.rotation = initRotationR;
				GameObject.FindGameObjectWithTag ("LeftRow").transform.localRotation = initRotationL;
				GameObject.FindGameObjectWithTag ("RightRow").transform.localRotation = initRotationR;
				MoveBoat.right = false;
				MoveBoat.left = false;
			}
			
		} 


			// Row Left 
			if (MoveBoat.left && this.gameObject.name == "Lpivot") {
				GameObject.FindGameObjectWithTag ("LeftRow").transform.Rotate (Vector3.right * speed * Time.deltaTime);
			}
			
			// Row Right 
			if (MoveBoat.right && this.gameObject.name == "Rpivot") {
			GameObject.FindGameObjectWithTag ("RightRow").transform.Rotate (Vector3.right * speed * Time.deltaTime);

			}




	}
}
