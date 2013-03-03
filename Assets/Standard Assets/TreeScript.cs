using UnityEngine;
using System.Collections;

public class TreeScript : MonoBehaviour
{
	void Update()
	{
		 transform.LookAt(Camera.main.transform.position, Vector3.up);
	}
}
