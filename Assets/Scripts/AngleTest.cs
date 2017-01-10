using UnityEngine;
using System.Collections;

public class AngleTest : MonoBehaviour
{
	public Transform target;
	public Transform this_2;

	Vector3 baseline;
	Vector3 angleVector;

	double x1, y1, x2, y2 = 1000;

	bool first = true;

	public static bool up = false;
	public static float speed = 0;

	// Use this for initialization
	void Start()
	{
//		baseline = transform.position - this_2.position;
//		angleVector = target.position - transform.position;
	}

	void FixedUpdate()
	{
//		print(target.position.y.ToString("F2"));
		if(first)
		{
			if(y2 != 1000)
			{
				x1 = x2;
				y1 = y2;

				y2 = Mathf.Abs(target.position.y - this_2.position.y);
				x2 = Mathf.Abs(target.position.z - this_2.position.z);
			}
			else
			{
				x1 = x2;
				y1 = y2;
			}
		}
		else
		{
			
			y2 = Mathf.Abs(target.position.y - this_2.position.y);
			x2 = Mathf.Abs(target.position.z - this_2.position.z);
		}
		first = !first;

		speed = Mathf.Abs((float)(y1-y2))*12;
//		print(speed*5);
//		print(Mathf.Abs((float)(y1-y2)).ToString("F2"));

//		print("antes: "+y1+" | depois: "+y2);

//		print("1:"+x1.ToString("F2")+";"+y1.ToString("F2"));
//		print("2:"+x2.ToString("F2")+";"+y2.ToString("F2"));
//		print(this_2.rigidbody.angularVelocity.ToString("F5"));
//		print(target.rigidbody.velocity.ToString("F1"));
		
		if(y2 != 1000)
		{
			if(x1 < x2)
			{
//				print ("forward");
//				print((x1-x2)*-100);
			}
			
			if(x1 > x2)
			{
//				print ("backward");
//				print((x1-x2)*100);
			}
			
			if(y1 < y2)
			{
				up = true;
//				print ("upward");
//				print((y1-y2)*-100);
			}
			
			if(y1 > y2)
			{
				up = false;
//				print ("downward");
//				print((y1-y2)*100);
			}
		}
	}
	
	// Update is called once per frame
	void Update()
	{
//		angleVector = target.position - transform.position;
//		baseline = (transform.position - target.position);

//		Debug.DrawRay(target.position, target.up*10, Color.green);
//		Debug.DrawRay(transform.position, transform.right*10, Color.green);
//		Debug.DrawRay(target.position, baseline*10, Color.red);

//		print (this_2.InverseTransformDirection(this_2.rigidbody.angularVelocity).ToString("F5"));


//		if(first)
//		{
//			y1 = target.position.y;
//			x1 = target.position.z;
//		}
//		else
//		{
//
//			y2 = target.position.y;
//			x2 = target.position.z;
//		}
//		first = !first;
//
//		if(y2 != 1000)
//		{
//			if(x1 > x2)
//			{
//				print ("forward");
//			}
//
//			if(x1 < x2)
//			{
//				print ("backward");
//			}
//
//			if(y1 > y2)
//			{
//				print ("upward");
//			}
//			
//			if(y1 < y2)
//			{
//				print ("downward");
//			}
//		}

//		print(target.rigidbody.velocity.ToString());



//		print("ang: "+Vector3.Angle(transform.right, baseline));
//		print(Vector3.Distance(target.position, transform.position));
//		y1 = (target.position.y - (transform.position.y + 2.5));
//		x1 = (target.position.z - transform.position.z);
//		print(target.position.y - (transform.position.y + 2.5));
//		print(target.position.z - transform.position.z);
//		print(x+","+y);
//
//		if (y < -1.25f)
//		{
//			//bottom
//
//		}
//		else if (x >= 0)
//		{
//			//top right
//
//		}
//		else
//		{
//			//top left
//
//		}
	}
}
