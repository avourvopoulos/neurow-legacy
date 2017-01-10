using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class Arduino : MonoBehaviour
{
	public static Arduino arduino;
	static SerialPort sp;
//	public string port = "COM11";

	void Awake()
	{
		if(arduino == null)
		{
			arduino = this;
		}
		else
		if(arduino != this)
		{
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start()
	{
//		sp = new SerialPort("\\\\.\\"+Settings.comPort, 9600);
//		sp = new SerialPort("\\\\.\\"+port, 9600);
//		if(!sp.IsOpen)
//		{
//			sp.Open();
//		}

		ChangeComPort("9");	//only for study
	}

	public void ChangeComPort(string port)
	{
		print (port);
		if(sp != null && sp.IsOpen)
		{
			sp.Close();
		}

		Settings.comPort = port;

		sp = new SerialPort("\\\\.\\COM"+port, 9600);
		if(!sp.IsOpen)
		{
			sp.Open();
		}
	}

	void OnApplicationQuit()
	{
		DisableAllMottors();
		if(sp.IsOpen)
		{
			sp.Close();
		}
	}

	void OnDisable()
	{
		DisableAllMottors();
	}

	public static void Send(string data)
	{
		sp.Write(data);
	}

	void DisableAllMottors()
	{
		if(sp != null && sp.IsOpen)
		{
			print("disable all\n");
			int i = 0;
			for(i = 0; i <= 5; i++)
			{
				sp.Write("0;0;"+i+";");
				sp.Write("0;1;"+i+";");
			}
		}
	}
	
	// Update is called once per frame
	void Update()
	{

	}
}
