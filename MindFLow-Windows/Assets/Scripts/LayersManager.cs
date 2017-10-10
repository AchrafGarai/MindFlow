using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayersManager : MonoBehaviour {
	public GameObject active_Layer;
	public GameObject main_Layer;
	// Event 



	void Awake () {
		main_Layer=GameObject.Find("Main Map");
		active_Layer = main_Layer;

	}
	void Start () {
		//active_Layer.GetComponent<Layer_Visiblity> ().Layer_Set_Visible ();
		}

	// Update is called once per frame
	void Update () {
		
	}
	public void Set_active_layer(GameObject layer){
		layer.GetComponent<Layer> ().Set_name ();
		active_Layer.GetComponent<Layer_Visiblity> ().Layer_Set_InVisible ();

		active_Layer = layer;
		active_Layer.GetComponent<Layer_Visiblity> ().Layer_Set_Visible ();


	}
	public GameObject get_active_layer(){
		return active_Layer;
	}
	public void back_to_main(){
		active_Layer.GetComponent<Layer_Visiblity> ().Layer_Set_InVisible ();
		active_Layer = main_Layer;
		active_Layer.GetComponent<Layer_Visiblity> ().Layer_Set_Visible ();


	}

}
