using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ColorPickHandler : MonoBehaviour,IPointerClickHandler {
	UnityEngine.UI.Image image;

	Color color; 
	public EnvironmentManagement Envi_Manager;
	// Use this for initialization
	void Start () {
		image = gameObject.GetComponent<UnityEngine.UI.Image> ();
		color = image.color;
	}
	public void OnPointerClick(PointerEventData eventData){
		color = image.color;
		Envi_Manager.Set_color (color);
	}
}
