using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map_item_text : MonoBehaviour {
	public string text_name;
	// Use this for initialization
	void Start () {
		text_name = gameObject.transform.parent.name;
		gameObject.gameObject.GetComponent<Text> ().text = text_name;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
