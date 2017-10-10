using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CameraManager : MonoBehaviour 
{
	Camera cam;
	public float speed=5f;
	public float minScale=10f;
	public float maxScale=50f;
	float camDistance;

	public void Start()
	{
		cam = this.GetComponent<Camera>();
	
	}
	public void ScrollBarhandler(float Scrollmultiplier){
		cam.orthographicSize=Scrollmultiplier;
	}
	public void Update()
	{
		
		if (Input.GetAxis("Mouse ScrollWheel") != 0f ) 
		{
			
			camDistance += (Input.GetAxis("Mouse ScrollWheel")*speed);
			camDistance = Mathf.Clamp(camDistance, minScale, maxScale);
			cam.orthographicSize = camDistance;
		}

	}
}