using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour 
{
	
	public GameObject kinect;
	public GameObject menuCamera;
	public GameObject mainCamera;
	
	private bool startGame = false;
	
	public void Reset()
	{
		kinect = GameObject.Find("Kinect");
		menuCamera = GameObject.Find("MenuCamera");
		mainCamera = GameObject.Find("Main Camera");
		
		menuCamera.camera.active = true;
		mainCamera.camera.enabled = false;
		
		StartCoroutine(Wait());	
	}
	
	void Start()
	{
		Reset();
	}
	
	IEnumerator Wait()
	{
		yield return new WaitForSeconds(5.0f);
		startGame = true;
		yield return null;
	}
	
	void Update()
	{
		if( startGame )
		{
			if( !kinect.transform.GetComponent<KinectOSC>().onMenu )
			{
				menuCamera.camera.active = false;
				mainCamera.camera.enabled = true;
			} else
			{
				menuCamera.camera.active = true;
				mainCamera.camera.enabled = false;
			}
		}
		
		
	}
}
