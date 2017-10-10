using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Set_Active_Project : MonoBehaviour {
	public GameObject Project_handler ;

	// Use this for initialization
	void Start () {
	    

		Project_handler = GameObject.Find("Button handler");
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Set_Project_name(){
		string project_name = gameObject.name;
		Project_handler.GetComponent<Project_Loading> ().set_Project_name (project_name);
	}

}
