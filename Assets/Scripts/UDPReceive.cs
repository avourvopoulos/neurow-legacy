using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

public class UDPReceive : MonoBehaviour {

 // receiving Thread
    Thread receiveThread; 
	Thread applyThread; 
 // udpclient object
    UdpClient client = null;
	IPEndPoint IP;
	byte[] udpdata;

	string saveStr = "";
	
//	public Texture2D arrowUp;
//	public Texture2D arrowDown;

    public static List<string> datatypelst = new List<string>();
	public static List<string> devicelst = new List<string>();
	public static List<string> jointslst = new List<string>();
	public static List<string> transformTypelst = new List<string>();
	
	public static List<string> DataList = new List<string>();

	public static string datatype;
	public static string device;
	public static string joint;
	public static string transformationtype;
	public static float udpx, udpy, udpz, udpw;
	public static string data = string.Empty;
	public static string[] words;
	
	public static string tempstr = "";

    // public
    public int port; // define > init
	public static string portField = "1205";
	public static bool isConnected=true;
	
	public static string selection = "n/a";
	
	public Vector2 scrollPosition = Vector2.zero;//scrolling window
	
	bool activeWin = false;//activate window
	public Rect windowRect0 = new Rect(Screen.width - 220, Screen.height / 2-50, 180, 220);//posx, posy, width, height
	public GUIStyle winStyle;
	
	float timer = 10;

	
	public static List<string> Devices = new List<string>();
	
	public string[] values = new string [8];

	string[] sep = {";"};
	string[] wordsplit;
	string[] separators = {"[$]","[$$]","[$$$]",",", " ", ";"};

//	Semaphore sem1;
	bool isReceiving = false;

	bool firstPacket = true;
	
    void Start()
    {
//		sem1 = new Semaphore (1, 1);

		windowRect0 = new Rect(Screen.width - 220, Screen.height / 2-100, 200, 150);//posx, posy, width, height
		
		port = int.Parse(portField);
		init();
		isConnected = true;
		print("Start RehabNet Server");
    }
	
	
	void FixedUpdate () 
	{
//		port = int.Parse(portField);

		timer -= Time.deltaTime;
//		timestamp = DateTime.UtcNow.Hour.ToString ("00") +DateTime.UtcNow.Minute.ToString ("00") + DateTime.UtcNow.Second.ToString ("00") + DateTime.Now.Millisecond.ToString ("0000"); //time in min:sec:usec
				
		if(timer <= 0)
		{
			timer = 10;
			clearLists();
		}
	//	print(timer);
	}
	

	
    public void init()
    {		
//		sem1 = new Semaphore (1, 1);
        // Local endpoint define (where messages are received).
        // Create a new thread to receive incoming messages.
		isConnected = true;
        receiveThread = new Thread(ReceiveData);
        receiveThread.IsBackground = true;	
        receiveThread.Start();
		Debug.Log ("receiveThread");

//		isReceiving = true;
//		applyThread = new Thread(ApplyData);
//		applyThread.IsBackground = true;	
//		applyThread.Start();
//		Debug.Log ("applyThread"); 

	}
	

 

    // receive thread 
    public void ReceiveData() 
    {	
		//Debug.Log("ReceiveData!!!!!!!!!!!");
		if(client == null)
		{
			print("new client \n");
			client = new UdpClient(port);
//			client.Client.SetSocketOption (SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer, 0);
			Debug.Log ("Started UDP - port: " + port);
			
			// receive Bytes from 127.0.0.1
			IP = new IPEndPoint(IPAddress.Any, port);
		}

		while (isConnected)
        {
//			print("receive\n");
			//Debug.Log("isConnected "+isConnected);
//			sem1.WaitOne();

            try 
            {
    	        udpdata = client.Receive(ref IP);

            //  UTF8 encoding in the text format.
				data = Encoding.UTF8.GetString(udpdata);

				if(data == "")
				{
//					print("strait\n");
					MoveBoat.left = false;
					MoveBoat.right = false;
				}

				try 
				{ 
					if(data!= string.Empty)
					{
						wordsplit = data.Split(sep, StringSplitOptions.RemoveEmptyEntries);
						foreach (string wsp in wordsplit)
						{
							//	Debug.Log("1: " + wsp);
							if(wsp.Length >= 4)
							{
								TranslateData(wsp);
							}
						}
					}
					
				}//try
				
				catch (Exception err) 
				{
					print(err.ToString());
				}
				
				//			Thread.Sleep(100);
				
				data = string.Empty;


            }//try
            catch (Exception err) 
            {
                print(err.ToString());
            }
			
//		Thread.Sleep(100);

//			sem1.Release(1);

        }//while true		

    }//ReceiveData


//	void ApplyData()
//	{
//	//	Debug.Log("ApplyData!!!!!!!!!!!!");
//
////		long a, b, c;
//
//		while (isReceiving)
//		{
////			print("apply\n");
//
////			a = System.DateTime.Now.Ticks;
//			
//		//	Debug.Log("isReceiving "+isReceiving);
//			
////			sem1.WaitOne();
//			
//			try 
//			{ 
//				if(data!= string.Empty)
//				{
//					string[] sep = {";"};
//					string[] wordsplit = data.Split(sep, StringSplitOptions.RemoveEmptyEntries);
//					foreach (string wsp in wordsplit)
//					{
//					//	Debug.Log("1: " + wsp);
//						if(wsp.Length >= 4)
//						{
//							TranslateData(wsp);
//						}
//					}
//				}
//				
//			}//try
//			
//			catch (Exception err) 
//			{
//				print(err.ToString());
//			}
//			
////			Thread.Sleep(100);
//
//			data = string.Empty;
//			
////			sem1.Release(1);
//
//			Thread.Sleep(6); // makes the thread run at around 140Hz
//
////			try
////			{
////				b = System.DateTime.Now.Ticks;
//////				c = (1 / (b - a));
////				print(10000000.0/(b-a)+"\n");
//////				print("Frequencia: " + c.ToString("F8") + "\n");
////			}
////			catch(Exception err)
////			{
////				print(err.ToString());
////			}
////			c = (1 / (b - a)) / 10000000;
////			print("Frequencia: " + c + "\n");
//			
//		}//while	
//		
//	}//end of ApplyData()

	
	void TranslateData(string n_data)
	{
				//	[$]<data  type> , [$$]<device> , [$$$]<joint> , <transformation> , <param_1> , <param_2> , .... , <param_N>
				//	[$]GameData , [$$]TPT-VR , [$$$]<joint> , <transformation> , <param_1> , <param_2> , .... , <param_N>
//		Debug.Log("1: " + n_data);

				// Decompose incoming data based on the protocol rules


				words = n_data.Split(separators, StringSplitOptions.RemoveEmptyEntries);

				device = words [1];
				datatype = words [0];
				joint = words [2];
				transformationtype = words [3];
				
//				string emustring = device + ":"+ transformationtype +":"+ joint;
				
				tempstr = device + ":" + datatype + ":" + joint + ":" + transformationtype;
//		print (tempstr + "\n");

//		print(words[0]+"\n");
				
	if(Settings.isTraining)
	{
		if(firstPacket && MoveBoat.boatspeed == 0)
		{
			firstPacket = false;
			MoveBoat.boatspeed = 10;
		}

		if(transformationtype == "both" && words[5] == "00")
		{
//				MoveBoat.goFront();
			MoveBoat.left = false;
			MoveBoat.right = false;
			MoveBoat.front = true;
		}
		else
		{
			if(transformationtype == "both" && words[5] == "10")
			{
//					MoveBoat.goLeft();
				MoveBoat.left = true;
				MoveBoat.right = false;
				MoveBoat.front = false;
			}

			if(transformationtype == "both" && words[5] == "01")
			{
//					MoveBoat.goRight();
				MoveBoat.left = false;
				MoveBoat.right = true;
				MoveBoat.front = false;
			}
		}
		//if leftarrow and event=1 then true
//		if (joint == "leftarrow" && words [4] == "1") {
//			MoveBoat.left = true;
//			MoveBoat.right = false;
//		}
//
//		if (joint == "leftarrow" && words [4] == "0") {
//			MoveBoat.left = false;
//		}
//
//		if (joint == "rightarrow" && words [4] == "1") {
//			MoveBoat.left = false;
//			MoveBoat.right = true;
//		}
//
//		if (joint == "rightarrow" && words [4] == "0") {
//			MoveBoat.right = false;
//		}
	}

	if (!Settings.isTraining)
	{
//			print(transformationtype + " | - | " + words [4]+"\n");
		
//		if(transformationtype == "both" && words[5] == "00")
//		{
////				print("stop\n");
//			MoveBoat.left = false;
//			MoveBoat.right = false;
//		}
//		else
//		{
//				print(transformationtype+"\n");
//			if((transformationtype == "both" || words [2] == "leftarrow") && (Convert.ToSingle(words [4]) <  0))
//			print(Convert.ToSingle(words [4])+"\n");

			if(transformationtype == "both")
			{
				if(firstPacket && MoveBoat.boatspeed == 0)
				{
					firstPacket = false;
					MoveBoat.boatspeed = 10;
				}


				if(Convert.ToSingle(words [4]) <  0)
				{
//					print("left\n");
					MoveBoat.left = true;
					MoveBoat.right = false;
					MoveBoat.front = false;
					saveStr += System.DateTime.Now.Ticks + "," + words[4] + "\n";
				}
				else if(Convert.ToSingle(words [4]) > 0)
				{
//					print("right\n");
					MoveBoat.left = false;
					MoveBoat.right = true;
					MoveBoat.front = false;
					saveStr += System.DateTime.Now.Ticks + "," + words[4] + "\n";
				}
				else if(Convert.ToSingle(words [4]) == 0)
				{
//					print("strait\n");
					MoveBoat.left = false;
					MoveBoat.right = false;
					MoveBoat.front = true;
					saveStr += System.DateTime.Now.Ticks + "," + words[4] + "\n";
				}
			}

//			if((transformationtype == "both" && words [2] == "vrpn") && (Convert.ToSingle(words [4]) <  0))
//			{
//				print("left\n");
//				MoveBoat.left = true;
//				MoveBoat.right = false;
//			}
//
////			if((transformationtype == "both" || words [2] == "rightarrow") && (Convert.ToSingle(words [4]) > 0))
//			if((transformationtype == "both" && words [2] == "vrpn") && (Convert.ToSingle(words [4]) > 0))
//			{
//				print("right\n");
//				MoveBoat.left = false;
//				MoveBoat.right = true;
//			}
//
////			if((transformationtype == "both" || words [2] == "")&& (Convert.ToSingle(words [4]) == 0))
//			if((transformationtype == "both" && words [2] == "vrpn")&& (Convert.ToSingle(words [4]) == 0))
//			{
//				print("strait\n");
//				MoveBoat.left = false;
//				MoveBoat.right = false;
//			}
//		}
	}



//		switch (words.Length) {
//				case 5:
//				{
//					udpx = float.Parse (words [4]);
//					udpy = float.NaN;
//					udpz = float.NaN;
//					udpw = float.NaN;			
//					break;
//				}
//				case 6:
//				{
//					udpx = float.Parse (words [4]);
//					udpy = float.Parse (words [5]);
//					udpz = float.NaN;
//					udpw = float.NaN;				
//					break;
//				}
//				case 7:
//				{
//					udpx = float.Parse (words [4]);
//					udpy = float.Parse (words [5]);
//					udpz = float.Parse (words [6]);
//					udpw = float.NaN;		
//					break;
//				}
//				case 8:
//				{
//					udpx = float.Parse (words [4]);
//					udpy = float.Parse (words [5]);
//					udpz = float.Parse (words [6]);
//					udpw = float.Parse (words [7]);
//					break;
//				}	
//				default:
//				{
//					udpx = float.NaN;
//					udpy = float.NaN;
//					udpz = float.NaN;
//					udpw = float.NaN;	
//					break;
//				}
//				}//switch
//
//
////		Debug.Log("2: "+ datatype + "," + device + "," + joint + "," + transformationtype + "," + udpx.ToString () + "," + udpy.ToString () + "," + udpz.ToString () + "," + udpw.ToString () + ";");		
//
//		
//				if (!DataList.Contains (tempstr)) {
//					DataList.Add (tempstr);//add to datatype list
//				}							
//				
//				//populate lists categorizing the segmeneted data
//				if (!datatypelst.Contains (datatype)) {
//					datatypelst.Add (datatype);//add to datatype list
//					//Debug.Log("datatypelst: "+datatypelst.Count);
//				}
//				if (!devicelst.Contains (device)) {
//					devicelst.Add (device);//add to device list
//					//Debug.Log("devicelst: "+devicelst.Count);
//				}
//				if (!jointslst.Contains (joint)) {
//					jointslst.Add (joint);//add to device list
//					//Debug.Log("jointslst: "+jointslst.Count);
//				}
//				if (!transformTypelst.Contains (transformationtype)) {
//					transformTypelst.Add (transformationtype);//add to transformaton type list
//					//Debug.Log("transformTypelst: "+transformTypelst.Count);
//				}

	}
	
	
			
			
	public static void clearLists()
	{
	  DataList.Clear();
	  datatypelst.Clear();
	  devicelst.Clear();
	  jointslst.Clear();
	  transformTypelst.Clear();
	}
	
	void RehabNetWindow(int windowID) 
	{    
        GUI.BeginGroup (new Rect (0, 0, 190, 150));
		GUI.color = Color.yellow;
		//GUI.Box (new Rect (0,0,200,180), "RehabNet Protocol");
		GUI.color = Color.white;	
			
		//display local ip address
		GUI.Label(new Rect(20, 5, 200, 20), "IP Address: "+LocalIPAddress());
				
		GUI.Label(new Rect(20, 35, 100, 20), "Port:");
		GUI.enabled = !isConnected;
		portField = GUI.TextField (new Rect (55, 35, 50, 20), portField);
		GUI.enabled = true;
		
	GUI.enabled = !isConnected;				
		//Start sending UDP
		if (GUI.Button (new Rect (10, 80, 90, 30), "Start")) 
		{
			port = int.Parse(portField);	
			init();	
			print("Start Server");
		}
	GUI.enabled = true;	
		
	GUI.enabled = isConnected;		
		//Stop sending UDP
		if (GUI.Button (new Rect (100, 80, 90, 30), "Stop")) 
		{		
			isConnected = false;
			isReceiving = false;
				receiveThread.Abort();
				applyThread.Abort();
//				sem1.Close();
			client.Close();
			clearLists();
			print("Stop UDP");
		}
 	GUI.enabled = true;		
		
		//copy address to clipboard
		if (GUI.Button (new Rect (30,120,150,20), "Copy IP to Clipboard"))
		{
			ClipboardHelper.clipBoard = LocalIPAddress();
		}
	
		GUI.EndGroup ();//end network group
		
     //   GUI.DragWindow(new Rect(0, 0, 10000, 10000));
    }
	
	
	void OnGUI()
	{

//			if(activeWin == false)
//			{
//				if (GUI.Button (new Rect (Screen.width - 220, Screen.height / 2-130, 200, 30), "RehabNet Server")) 
//				{activeWin = true;}
//			GUI.Label(new Rect(Screen.width - 50, Screen.height / 2-137, 40, 40), " ");
//			}
//			else if (activeWin == true)
//			{
//				if (GUI.Button (new Rect (Screen.width - 220, Screen.height / 2-130, 200, 30), "RehabNet Server")) 
//				{activeWin = false;}
//				GUI.Label(new Rect(Screen.width - 50, Screen.height / 2-137, 40, 40), " ");
//			}
//				
//			if(activeWin)
//			{
//				windowRect0 = GUI.Window(0, windowRect0, RehabNetWindow, "", winStyle);	
//			}
				

	}

	 	
	public static string LocalIPAddress()
	 {
		if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
    	{
        return null;
    	}
		
	   IPHostEntry host;
	   string localIP = "";
	   host = Dns.GetHostEntry(Dns.GetHostName());
	   foreach (IPAddress ip in host.AddressList)
	   {
	     if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork )
	     {
	       localIP = ip.ToString();
	     }
	   }
	   return localIP;
	 }

	public void changeReceivePort(string newPort)
	{
		port = int.Parse(newPort);
		portField = newPort;
	}
	

	void OnDisable() 
	{ 
		if(isConnected)
    	{
			isConnected = false;
			isReceiving = false;
		 	receiveThread.Abort(); 
//		 	applyThread.Abort();
		 	client.Close(); 
		}

		if(saveStr != "")
		{
			print("saving input!\n");
			string fileName = "INPUT_" + System.DateTime.Now.Year + "-" + System.DateTime.Now.Month + "-" + System.DateTime.Now.Day + "_" + System.DateTime.Now.Hour + "-" + System.DateTime.Now.Minute + "-" + System.DateTime.Now.Second;
			System.IO.File.AppendAllText(Settings.logDir+"\\" + fileName + ".txt", saveStr);
		}
		else print("input string empty!\n");
	} 	
	
	void OnApplicationQuit () 
	{
		if(isConnected)
	    {
			isConnected = false;
			isReceiving = false;
		  	receiveThread.Abort();
//		  	applyThread.Abort();
			client.Close();
		}
    }
	
	
	
}
