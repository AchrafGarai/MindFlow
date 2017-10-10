using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class QuickPanelManager : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler{
	MouseManager mouseManager;
	// Use this for initialization
	void Start () {
		mouseManager = GameObject.FindObjectOfType(typeof(MouseManager)) as MouseManager;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OnPointerEnter(PointerEventData eventData){
		mouseManager.OverQuickPanelEnter ();
	}
	public void OnPointerExit(PointerEventData eventData){
		mouseManager.OverQuickPanelExit ();
	}
}
