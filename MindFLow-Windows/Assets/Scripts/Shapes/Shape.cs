using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour {
	public GameObject prec_layer;
	public GameObject new_layer;
	GameObject activelayer;
	GameObject layerManager;
	protected bool hover_over = false;
	public string layer_Link_name;

	protected void Start () {
		layerManager = GameObject.Find("LayersManager");

		activelayer = layerManager.GetComponent<LayersManager>().get_active_layer ();
	}
	

	protected void Update () {
		
	}
	protected void AddCollider(){
		gameObject.AddComponent<BoxCollider2D> ();
	}
	void OnMouseOver(){
		hover_over = true;
	}
	void OnMouseExit(){
		hover_over = false;
	}


	public virtual void Set_active_layer(){
		if (layer_Link_name==null) {
			GameObject go = Instantiate (new_layer) as GameObject;
			layer_Link_name = go.GetComponent<Layer> ().uniqueId;
			go.GetComponent<Layer> ().Set_Layer ();
		
			layerManager.GetComponent<LayersManager> ().Set_active_layer (go);

		} else if (layer_Link_name.Equals("")){
			GameObject go = Instantiate (new_layer) as GameObject;
			layer_Link_name = go.GetComponent<Layer> ().uniqueId;
			go.GetComponent<Layer> ().Set_Layer ();

		}

		else {
			GameObject go2 = GameObject.Find (layer_Link_name);
			go2.GetComponent<Layer>().Set_Layer();

		}   

		}

		
	}




