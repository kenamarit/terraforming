using UnityEngine;
using System.Collections;

public class EachHand : MonoBehaviour
{
	public GameObject tree;
	
	private GameObject centerOfWorld;
	private GameObject t;
	private bool clickOn = false;
	private TerrainDeformer deformer;
	
	private GameObject currentHand;
	private GameObject kinect;
	
	void Start()
	{
		t = GameObject.Find("Terrain");
		deformer = GameObject.Find("Terrain").GetComponent<TerrainDeformer>();
		kinect = GameObject.Find("Kinect");
		centerOfWorld = GameObject.Find("CenterOfWorld");
	}
	
	void OnTriggerEnter(Collider c)
	{
		if( c.transform.name == "LeftHand" || c.transform.name == "RightHand" )
		{
			clickOn = !clickOn;
			currentHand = c.gameObject;
			//Debug.Log(clickOn);
		}
	}
	
	void Update()
	{
		
		if( currentHand )
			currentHand.transform.LookAt(centerOfWorld.transform.position);
		
		if( clickOn )
		{
			
			Vector3 screenPos = Camera.main.WorldToScreenPoint(currentHand.transform.position);
			RaycastHit hit;
			
			if(Physics.Raycast(Camera.main.ScreenPointToRay(screenPos), out hit)) 
			{
				deformer.Damage(hit.point, t.transform.GetComponent<DeformOnClick>().brushHeight);
			}
		}
	}
					
	IEnumerator delay(float seconds)
	{
		yield return new WaitForSeconds(seconds);
	}
					
}
