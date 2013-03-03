using UnityEngine;
using System.Collections;

public class HandCoordinates : MonoBehaviour 
{
	public GameObject leftHand;
	public GameObject rightHand;
	
	private GameObject kinect;
	
	void Start()
	{
		leftHand = GameObject.Find("LeftHand");
		rightHand = GameObject.Find("RightHand");
		
		kinect = GameObject.Find("Kinect");
	}
	
	void Update()
	{
		if( !kinect.transform.GetComponent<KinectOSC>().onMenu )
		{
			Vector3 currentLeftHandPos = kinect.GetComponent<KinectOSC>().leftHandCoord;
			Vector3 currentRightHandPos = kinect.GetComponent<KinectOSC>().rightHandCoord;
			
			float distance = 200;    
			// Transforms a forward position relative to your player into the world space    
			Vector3 throwPos = Camera.main.transform.TransformPoint( -Vector3.forward * distance );
			
			Vector3 leftScreenPos = Camera.main.WorldToScreenPoint(currentLeftHandPos);
			Vector3 rightScreenPos = Camera.main.WorldToScreenPoint(currentRightHandPos);
			
			// Instantate the object on the position    
			//GameObject place = Instantiate ( blank, throwPos, Quaternion.identity );
			
			leftHand.transform.position = new Vector3(-currentLeftHandPos.x, currentLeftHandPos.y, leftScreenPos.z + throwPos.z);
			rightHand.transform.position = new Vector3(-currentRightHandPos.x, currentRightHandPos.y, rightScreenPos.z + throwPos.z);
		}
		
	}
}