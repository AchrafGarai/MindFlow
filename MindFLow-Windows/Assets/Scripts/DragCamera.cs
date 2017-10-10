using UnityEngine;
using System.Collections;

public class DragCamera : MonoBehaviour
{
	public float dragSpeed = 2;
	private Vector3 dragOrigin;
	private MouseManager mouseManager;
	public bool cameraDragging = true;

	public float outerLeft = -100f;
	public float outerRight = 100f;

	public float outerUp = -10f;
	public float outerDown = 10f;
	void Start(){
		mouseManager = GameObject.FindObjectOfType(typeof(MouseManager)) as MouseManager;
	}
	void Update()
	{


		Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

		if (mouseManager.CheckMousePosition(Input.mousePosition)) {

			if (Input.GetMouseButtonDown(0) )
			{
				dragOrigin = Input.mousePosition;
				return;
			}

			if (!Input.GetMouseButton(0)) return;

			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
			Vector3 move = new Vector3(pos.x * dragSpeed, pos.y * dragSpeed, 0);

			if (move.x > 0f || move.y > 0f)
			{
				if (this.transform.position.x < outerRight || this.transform.position.y < outerUp) {
					transform.Translate (move, Space.World);
				} 

			}
			else{
				if(this.transform.position.x > outerLeft || this.transform.position.y < outerDown )
				{
					transform.Translate(move, Space.World);
				}

			}
		}
	}


}