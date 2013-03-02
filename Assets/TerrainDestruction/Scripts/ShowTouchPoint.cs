using UnityEngine;
using System.Collections;

public class ShowTouchPoint : MonoBehaviour 
{
	public GameObject touchPointTexture;
	
	private GameObject gui;
	
	void Start()
	{
		gui = (GameObject)Instantiate(touchPointTexture, Vector3.zero, Quaternion.identity);
		//gui.transform.parent = GameObject.Find("Terrain").transform;
	}
	
	void Update()
	{

		Vector3 currentRightHandCoord = Camera.main.transform.GetComponent<KinectOSC>().rightHandCoord;

		currentRightHandCoord.z = 700;
		gui.transform.position = currentRightHandCoord;
	}
}
