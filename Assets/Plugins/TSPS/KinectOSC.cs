using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TSPS;

public class KinectOSC : MonoBehaviour  {
	
	public Vector3 rightHandCoord;
	public Vector3 leftHandCoord;
	public Vector3 headCoord;
	public bool isBreathing = false;
	
	private GameObject terrain;
	private GameObject centerOfWorld;
	
	void Start()
	{
		terrain = GameObject.Find("Terrain");
		centerOfWorld = GameObject.Find("CenterOfWorld");
	}

	public Vector3 coordinates;

	public void OnEnable(){
		UnityOSCReceiver.OSCMessageReceived += new UnityOSCReceiver.OSCMessageReceivedHandler(OSCMessageReceived);

	}
	public void OnDisable(){
		UnityOSCReceiver.OSCMessageReceived -= new UnityOSCReceiver.OSCMessageReceivedHandler(OSCMessageReceived);
	}

	public void OSCMessageReceived(OSC.NET.OSCMessage message){	
		string address = message.Address;
		ArrayList args = message.Values;
		

		if( address == "/righthand")
		{
			rightHandCoord = new Vector3((float)args[0], (float)args[1], (float)args[2]);
			Debug.Log(rightHandCoord);
		}
		
		if( address == "/lefthand")
		{
			leftHandCoord = new Vector3((float)args[0], (float)args[1], (float)args[2]);
			Debug.Log(leftHandCoord);
		}
		
		if( address == "/head")
		{
			headCoord = new Vector3((float)args[0], (float)args[1], (float)args[2]);
			Camera.main.transform.position = new Vector3((float)args[0], (float)args[1], (float)args[2]);
			Camera.main.transform.LookAt(centerOfWorld.transform.position);
			
			Debug.Log(headCoord);
		}
		
		if( address == "/breath")
		{
			if( (int)args[0] == 0 )
				isBreathing = false;
			if( (int)args[0] == 1 )
				isBreathing = true;
		//	Debug.Log(args[0]);
		}
		
		if( address == "/brushHeight")
		{
			terrain.transform.GetComponent<DeformOnClick>().brushHeight = (int)args[0];
	//		Debug.Log(args[0]);
		}
	}
}