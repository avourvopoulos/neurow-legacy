using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Net;

public class UpdateValueLabel : MonoBehaviour
{
	Slider thisSlider;
	Text thisSliderValueText;

	Toggle thisToggle;

	InputField thisInputField;

	// Use this for initialization
	void Start()
	{
		thisSlider = gameObject.GetComponent<Slider>();
		thisToggle = gameObject.GetComponent<Toggle>();
		thisInputField = gameObject.GetComponent<InputField>();

		if(thisSlider != null)
		{
			thisSliderValueText = transform.FindChild("Value").GetComponent<Text>();
			thisSlider.onValueChanged.AddListener(delegate {updateValue();});
		}

		if(thisToggle != null)
			thisToggle.onValueChanged.AddListener(delegate {updateValue();});

		if(thisInputField != null)
//			thisInputField.onValueChange.AddListener(delegate {updateValue();});
			thisInputField.onEndEdit.AddListener(delegate {updateValue();});
	}

	void Update()
	{
		if (thisInputField != null && thisInputField.placeholder.GetComponent<Text>().text.Equals("local ip"))
		{
			thisInputField.placeholder.GetComponent<Text>().text = UDPReceive.LocalIPAddress();
		}

		if (thisInputField != null && thisInputField.placeholder.GetComponent<Text>().text.Equals("local port"))
		{
			thisInputField.placeholder.GetComponent<Text>().text = UDPReceive.portField;
		}
	}

//	public string LocalIPAddress()
//	{
//		if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
//		{
//			return null;
//		}
//		
//		IPHostEntry host;
//		string localIP = "";
//		host = Dns.GetHostEntry(Dns.GetHostName());
//		foreach (IPAddress ip in host.AddressList)
//		{
//			if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork )
//			{
//				localIP = ip.ToString();
//			}
//		}
//		return localIP;
//	}

	void updateValue()
	{
		if(thisSlider != null)
		{
			thisSliderValueText.text = thisSlider.value+"";
			Settings.updateVariables(thisSlider.transform.name, thisSlider.value);
		}

		if(thisToggle != null)
		{
			Settings.updateVariables(thisToggle.transform.name, thisToggle.isOn);
		}

		if(thisInputField != null)
		{
			Settings.updateVariables(thisInputField.transform.name, thisInputField.text);
		}
	}

}
