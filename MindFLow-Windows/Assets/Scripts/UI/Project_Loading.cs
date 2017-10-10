using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Project_Loading : MonoBehaviour {
	public string project_name;
	public GameObject selected_Project;
	public Button load_button;
	SceneMan sceneMan;
	GameObject SceneManage;
	// Use this for initialization
	void Start () {
		SceneManage= GameObject.Find ("SceneMan");
		sceneMan = SceneManage.GetComponent<SceneMan> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void set_Project_name(string P_M){
		selected_Project.GetComponent<Text> ().text = P_M;
		project_name = P_M;
	}
	public void load(){
		if (project_name != null) {
			PlayerPrefs.SetString ("Active Project", project_name);
			print(PlayerPrefs.GetString("Active Project").ToString());
			sceneMan.to_Main_Menu ();	

		}
	}

}
