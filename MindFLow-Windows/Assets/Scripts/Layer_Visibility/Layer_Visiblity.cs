using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer_Visiblity : MonoBehaviour {

	Image[] image;
	text[] Text;
	Branch[] branches;
	// Use this for initialization

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Layer_Set_Visible(){
		image = gameObject.GetComponentsInChildren<Image> ();
		Text = gameObject.GetComponentsInChildren<text> ();
		branches=gameObject.GetComponentsInChildren<Branch> ();
		foreach (Image im in image) {
			im.UnHide_image ();
		}
		foreach (text tx in Text) {
			tx.UnHide_text ();
		}
		foreach (Branch br in branches) {
			br.UnHide_branch ();
		}


	

	}
	public void Layer_Set_InVisible(){
		image = gameObject.GetComponentsInChildren<Image> ();
		Text = gameObject.GetComponentsInChildren<text> ();
		branches=gameObject.GetComponentsInChildren<Branch> ();
		foreach (Image im in image) {
			im.Hide_image ();
		}
		foreach (text tx in Text) {
			tx.Hide_text ();
		}
		foreach (Branch br in branches) {
			br.Hide_branch ();
		}

	}
}
