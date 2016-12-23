using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuTabs : MonoBehaviour
{
	Color c;

	// Use this for initialization
	void Start()
	{
		c = gameObject.GetComponent<Image>().color;
		if(gameObject.GetComponent<Toggle>().isOn)
		{
			gameObject.GetComponent<Image>().color = new Color(c.r,c.g, c.b, 1);
		}
	}

	public void updateState(bool state)
	{
		c = gameObject.GetComponent<Image>().color;
//		print(state.ToString());
		if(state)
		{
			gameObject.GetComponent<Image>().color = new Color(c.r,c.g, c.b, 1);
		}
		else
		{
			gameObject.GetComponent<Image>().color = new Color(c.r,c.g, c.b, 0.5f);
		}
	}
}
