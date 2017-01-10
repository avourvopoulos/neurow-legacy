using UnityEngine;
using System.Collections;

public class HapticFeedback : MonoBehaviour
{
	public int hand = 0;
	public int handPart = 0;
//	float speed = 0;

	private bool left = true;
	private bool right = true;
	private bool stop = true;

	private string thisHand;

	private float speed;

	public string data = "";

	public Transform leftHand;
	public Transform rightHand;

	float x, y, q1, q2, q3, q4;

	float x_limit_left = -3.2298f;
	float y_limit_left = -3.7159f;

	float x_limit_right = 1.8761f;
	float y_limit_right = 0.6870f;

	void Start()
	{
		thisHand = transform.parent.parent.name;

//		leftHand = GameObject.FindGameObjectWithTag ("LeftHand").transform;
//		rightHand = GameObject.FindGameObjectWithTag ("RightHand").transform;

		q1 = 1.7f;
		q2 = 1.5f;
		q3 = 1.15f;
		q4 = 1.30f;
	}

	void Update()
	{
//		if(this.gameObject.transform.rigidbody.velocity.magnitude > 0)
//		{
//			speed = this.gameObject.transform.rigidbody.velocity.magnitude * 25;
//		}

		switch(hand)
		{
		case 0:
			switch(handPart)
			{
			case 0:
				speed = Settings.m1Power;
				break;

			case 1:
				speed = Settings.m2Power;
				break;

			case 2:
				speed = Settings.m3Power;	//study only
//				speed = 27;
				break;

			case 3:
				speed = Settings.m4Power;
				break;

			case 4:
				speed = Settings.m5Power;
				break;

			case 5:
				speed = Settings.m6Power;	//study only
//				speed = 45;
				break;
			}
			break;
		
		case 1:
			switch(handPart)
			{
			case 0:
				break;
				
			case 1:
				break;
				
			case 2:
				break;
				
			case 3:
				speed = Settings.m4Power;//using only one hand
				break;
				
			case 4:
				break;
				
			case 5:
				break;
			}
			break;
		}

		if (thisHand.Equals ("LeftHand"))
		{
			if(RotateRow.left)
			{
	//			print("left");
				stop = true;
				left = false;
				right = true;

				x = leftHand.localPosition.x;
				y = leftHand.localPosition.y;
				double d = 0.7;

	//			data += x.ToString() + "," + y.ToString() + "," + "\n";

				if (x >= x_limit_left && y >= y_limit_left) {//quadrant 1
					Arduino.Send ((speed * (q1)) + ";" + hand + ";" + handPart + ";");
				} else if (x <= x_limit_left && y >= y_limit_left) {//quadrant 2
					Arduino.Send ((speed * (q2)) + ";" + hand + ";" + handPart + ";");
				} else if (x <= x_limit_left && y <= y_limit_left) {//quadrant 3
					Arduino.Send ((speed * (q3 * d)) + ";" + hand + ";" + handPart + ";");
				} else if (x >= x_limit_left && y <= y_limit_left) {//quadrant 4
					Arduino.Send ((speed * (q4 * d)) + ";" + hand + ";" + handPart + ";");
				}
			}
			else
			{
				Arduino.Send ("0;" + hand + ";" + handPart + ";");
			}

//			print(GameObject.FindGameObjectWithTag ("LeftHand").transform.localPosition.y.ToString());
//			print(GameObject.FindGameObjectWithTag ("LeftHand").transform.localPosition.x.ToString());
//			print(GameObject.FindGameObjectWithTag ("LeftHand").transform.localPosition.z.ToString());



//			if(AngleTest.up)
//			{
////				print("up");
////				Arduino.Send((speed*1*AngleTest.speed)+";"+hand+";"+handPart+";");
//				Arduino.Send(speed+";"+hand+";"+handPart+";");
//			}
//			else
//			{
////				print("down");
////				Arduino.Send((speed*0.6*AngleTest.speed)+";"+hand+";"+handPart+";");
//				Arduino.Send(speed+";"+hand+";"+handPart+";");
//			}
		}

		if(thisHand.Equals("RightHand"))
		{
			if(RotateRow.right)
			{
	//			print("right");
				stop = true;
				left = true;
				right = false;
				
				x = rightHand.localPosition.x;
				y = rightHand.localPosition.y;
				
				if(x >= x_limit_right && y >= y_limit_right)//quadrant 1
				{
					Arduino.Send((speed*q1)+";"+hand+";"+handPart+";");
				}
				else if(x <= x_limit_right && y >= y_limit_right)//quadrant 2
				{
					Arduino.Send((speed*q2)+";"+hand+";"+handPart+";");
				}
				else if(x <= x_limit_right && y <= y_limit_right)//quadrant 3
				{
					Arduino.Send((speed*q3)+";"+hand+";"+handPart+";");
				}
				else if(x >= x_limit_right && y <= y_limit_right)//quadrant 4
				{
					Arduino.Send((speed*q4)+";"+hand+";"+handPart+";");
				}
			}
			else
			{
				Arduino.Send ("0;" + hand + ";" + handPart + ";");
			}
		}

		if(!RotateRow.left && !RotateRow.right && stop)
		{
			stop = false;
			left = true;
			right = true;
			Arduino.Send("0;"+hand+";"+handPart+";");
		}

	}

//	void OnDisable()
//	{
//		System.IO.File.AppendAllText("E:\\test.txt", data);
//	}

//	void OnCollisionEnter(Collision collision)
//	{
//		Arduino.Send(speed+";"+hand+";"+handPart+";");
//	}
//
//	void OnCollisionExit(Collision collisionInfo)
//	{
//		Arduino.Send("0;"+hand+";"+handPart+";");
//	}

}
