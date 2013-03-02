using UnityEngine;
using System.Collections;

public class DeformOnClick : MonoBehaviour 
{	
	public GameObject kinect;
	
	private bool clickOn = false;
	public int brushHeight = 3;
	
	TerrainDeformer deformer;
	
	void Start () {
		deformer = GetComponent<TerrainDeformer>();
		kinect = GameObject.Find("Kinect");
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		Vector3 rightHandCoord = kinect.transform.GetComponent<KinectOSC>().rightHandCoord;
		Vector3 handScreenPos = Camera.main.WorldToScreenPoint(rightHandCoord);
		
		if( Input.GetKeyDown("t") )
		{
			clickOn = !clickOn;
		}
		
		if( kinect.transform.GetComponent<KinectOSC>().isBreathing )
		{
			brushHeight = brushHeight;
		}
		else
		{
			brushHeight = -brushHeight;
		}
		
		StartCoroutine(ResizeMountain());
		
		
	}
	
	IEnumerator ResizeMountain()
	{
		if( clickOn )
		{
			RaycastHit hit;
			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000)) 
			{
				deformer.Damage(hit.point, brushHeight);
				
			}
		}
		yield return new WaitForSeconds(1.0f);
	}
}
