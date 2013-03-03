using UnityEngine;
using System.Collections;

public class DeformOnClick : MonoBehaviour 
{	
	public GameObject kinect;
	
	private bool clickOn = false;
	public int brushHeight = 3;
	
	public TerrainDeformer deformer;
	
	void Start () {
		deformer = GetComponent<TerrainDeformer>();
		kinect = GameObject.Find("Kinect");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( !kinect.transform.GetComponent<KinectOSC>().onMenu )
		{
			Vector3 rightHandCoord = kinect.transform.GetComponent<KinectOSC>().rightHandCoord;
			Vector3 handScreenPos = Camera.main.WorldToScreenPoint(rightHandCoord);
			
			if( kinect.transform.GetComponent<KinectOSC>().isBreathing )
			{
				brushHeight = Mathf.Abs(brushHeight);
			}
			else
			{
				brushHeight = (Mathf.Abs(brushHeight)) * -1;
			}

			
			
		}
		
	}
	
}
