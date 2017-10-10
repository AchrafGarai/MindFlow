using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneMan : MonoBehaviour {
	public GameObject Map_name_Panel;
	public string Map_name;
	static GameObject canvas;
	GameObject tp;
	// Use this for initialization
	void Start () {
		canvas = GameObject.Find ("Canvas");
		Map_name_Panel = Resources.Load ("UI/New Map Panel") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Quit(){
		Application.Quit ();
	}
	public void to_Start_Menu(){
		if (Map_name != null) {
			SceneManager.LoadScene ("Start Menu");
		}

	}
	public void Set_Active_Project(){

			PlayerPrefs.SetString ("Active Project", Map_name+".json");
			to_Main_Menu ();

	
	}
	public void to_Main_Menu(){
		SceneManager.LoadScene ("Main Scene");
	}

	public void to_Map_Selection(){
		SceneManager.LoadScene ("Map Selection");
	}
	public void Text_changed(string name){
		Map_name = name;
	}
	public void Spawn_Text_Panel(){
		Vector3 pos = new Vector3 (0f, 0f, 0f);
		pos.x += Screen.width / 2;
		pos.y += Screen.height / 2;
	     tp = Instantiate (Map_name_Panel, pos ,Quaternion.identity) as GameObject;
		tp.transform.SetParent (canvas.transform);
	}
	public void Destroy_Panel(){
		Destroy (tp);
	}
}
