using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TSPS;

public class KinectOSC : MonoBehaviour  {
	
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
		
		if( address == "/righthand_pos_world")
		{
			coordinates = new Vector3((float)args[0], (float)args[1], (float)args[2]);
			Debug.Log(coordinates);
		}
		/*
		if (address == "/TSPS/personEntered/") {
			addPerson(args);
		}
		else if(address == "/TSPS/personUpdated/"){				
			int person_id = (int)args[0];
			Person person = null;
			if (!people.ContainsKey(person_id)) {
				person = addPerson(args);
			}
			else{
				person = people[person_id];
				updatePerson(person, args);
				//personUpdated(person);
				BroadcastMessage("PersonUpdated", person, SendMessageOptions.DontRequireReceiver);
			}
		}
		else if(address == "/TSPS/personWillLeave/"){
			int person_id = (int)args[0];
			if (people.ContainsKey(person_id)) {
				Person personToRemove = people[person_id];				
				BroadcastMessage("PersonWillLeave", personToRemove, SendMessageOptions.DontRequireReceiver);
				people.Remove(person_id);
				//personWillLeave(personToRemove);
			}
		}
		else{
			print(address + " ");
		}
		*/
	}
}