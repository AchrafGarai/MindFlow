using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Starting_point : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler {
	GameObject MouseMan;

	// Use this for initialization
	void Start () {
		MouseMan = GameObject.Find ("mouseManager");
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OnPointerEnter(PointerEventData eventData){
		MouseMan.GetComponent<MouseManager> ().QuickPanelMaximize();


	}
	public void OnPointerExit(PointerEventData eventData){
		MouseMan.GetComponent<MouseManager> ().QuickPanelMinimize();

	}
}
