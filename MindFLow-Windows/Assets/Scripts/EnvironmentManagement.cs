using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManagement : MonoBehaviour {
	static  GameObject selected_Object=null;
	static Renderer renderer;
	static Vector3 center;
	static Color _color=Color.red;
	static bool rotate_forward=false;
	static bool rotate_backward=false;
	static bool move_object = false;
	bool scale_forward;
	GameObject cam;
	Vector3 offset;
	public LayersManager layersmanager;

	void Start () {
		cam=GameObject.Find("Main Camera");

	}
	
	// Update is called once per frame
	void Update () {
		if (move_object && selected_Object!=null ) {
			Vector3 newPos;
			newPos.x = cam.transform.position.x + offset.x;
			newPos.y = cam.transform.position.y + offset.y;
			if (selected_Object.GetComponent<text> () != null) {
				newPos.z = -1;
			} else if (selected_Object.GetComponent<Image> () != null) {
				newPos.z = 1;
			}else {
				newPos.z = 0;
			}
			selected_Object.transform.position = newPos;
			center = renderer.bounds.center;
		}

		if (rotate_forward && selected_Object!=null && move_object==false) {
			selected_Object.transform.RotateAround (center, Vector3.forward,
				20 * Time.deltaTime);
			}
		if (rotate_backward && selected_Object!=null && move_object==false) {
			selected_Object.transform.RotateAround (center, Vector3.back, 
				20 * Time.deltaTime);
		}

	}
	public void Rotate_f(){
		rotate_forward = true;
	}
	public void Rotate_b(){
		rotate_backward= true;
	}
	public void Stop_Rotate(){
		rotate_backward= false;
		rotate_forward = false;
	}
	public void Set_Object(GameObject  new_object){
		selected_Object = new_object;
		if (selected_Object != null) {
			renderer = selected_Object.GetComponent<Renderer> ();
			center = renderer.bounds.center;
			}

	}

	public void deSelect_Object(){
		selected_Object = null;
	}
	public void delete_Object(){
		if (selected_Object != null) {
			Destroy (selected_Object);
			selected_Object = null;
			}

	}
	public void Set_color(Color new_color){
		_color = new_color;
		if (selected_Object != null) {
			if (selected_Object.GetComponent<text> () != null) {
				selected_Object.GetComponent<text> ().switch_Color (_color);
			}
			else if(selected_Object.GetComponent<Branch> () != null && selected_Object != null ){
				selected_Object.GetComponent<Branch> ().switch_Color (_color);
			}

		}
	}
	public Color get_color(){
		return _color;
	}

	public void scale_object(float scale){
		if (selected_Object != null) {
			selected_Object.transform.localScale =new Vector3(scale,scale,scale);
		}
	}
	public void move_Object(){
		move_object = true;
		if (selected_Object != null) {
			offset = selected_Object.transform.position - cam.transform.position;
			}
	}
	public void place_Object(){
		move_object = false;
	}
	public  void Set_Active_layer(){
		if(selected_Object.GetComponent<Shape> () !=null){
		
			selected_Object.GetComponent<Shape> ().Set_active_layer ();
		}


	}
}
