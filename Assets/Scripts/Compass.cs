using UnityEngine;
using System.Collections;

public class Compass : MonoBehaviour
{
	public Transform waypoint;
	public Transform arrow;

	private Transform compassParent;

	// Use this for initialization
	void Start()
	{
		compassParent = arrow.parent;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 lookAt = waypoint.position;
		lookAt.y = transform.position.y;
		transform.LookAt(lookAt);
//		print(Vector3.Distance(transform.position, waypoint.position));
//		float dist = Vector3.Distance(transform.position, waypoint.position);
//		gameObject.renderer.material.color = HSVToRGB(DistToHue(dist), 1, 1);
//		GameObject.FindGameObjectWithTag ("arrow").renderer.material.color = HSVToRGB(DegToHue(compassParent.localEulerAngles.y), 1, 1);
		arrow.renderer.material.color = HSVToRGB(DegToHue(compassParent.localEulerAngles.y), 1, 1);
	}

	float DegToHue(float d)
	{
//		print(d.ToString());

		if(d > 45 && d < 315)
		{
			d = 45;
		}

		if(d >= 315)
		{
			d = 360 - d;
		}

		if(d < 0)
		{
			d = 0;
		}

		return (float)((d * (-0.28 / 45)) + 0.28);
	}

	public static Color HSVToRGB(float H, float S, float V)
	{
		if (S == 0f)
			return new Color(V,V,V);
		else if (V == 0f)
			return new Color(0,0,0);
		else
		{
			Color col = Color.black;
			float Hval = H * 6f;
			int sel = Mathf.FloorToInt(Hval);
			float mod = Hval - sel;
			float v1 = V * (1f - S);
			float v2 = V * (1f - S * mod);
			float v3 = V * (1f - S * (1f - mod));
			
			switch (sel + 1)
			{
			case 0:
				col.r = V;
				col.g = v1;
				col.b = v2;
				break;
			case 1:
				col.r = V;
				col.g = v3;
				col.b = v1;
				break;
			case 2:
				col.r = v2;
				col.g = V;
				col.b = v1;
				break;
			case 3:
				col.r = v1;
				col.g = V;
				col.b = v3;
				break;
			case 4:
				col.r = v1;
				col.g = v2;
				col.b = V;
				break;
			case 5:
				col.r = v3;
				col.g = v1;
				col.b = V;
				break;
			case 6:
				col.r = V;
				col.g = v1;
				col.b = v2;
				break;
			case 7:
				col.r = V;
				col.g = v3;
				col.b = v1;
				break;
			}
			col.r = Mathf.Clamp(col.r, 0f, 1f);
			col.g = Mathf.Clamp(col.g, 0f, 1f);
			col.b = Mathf.Clamp(col.b, 0f, 1f);
			return col;
		}
	}
}
