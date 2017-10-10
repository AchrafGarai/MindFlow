using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MouseManager : MonoBehaviour  {
	public GameObject TextPanel;
	public GameObject Quickpanel;
	public GameObject noBranchQuickpanel;
	public GameObject branch;
	public GameObject image;
	public GameObject Text_3d;
	public string  text;
	public static  Vector2 anchorPoint;
	public Vector2 Panelpos;
	static GameObject canvas;
	static GameObject QuickP;
	static GameObject Text_P;
	public float offset;
	private bool minQuickP = true;
	private bool panelIsAlive=false;
	private bool hoverQuickPanel=false;
	Vector3 branchSpawnPoint;
	//Image 
	public LayersManager layerManager;
	GameObject active_layer;
	// Use this for initialization
	void Start () {
		canvas = GameObject.Find ("Canvas");

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) && panelIsAlive==true) {
			if(!hoverQuickPanel){
			Destroy (QuickP);
			panelIsAlive = false;
			}
		}

		if (Input.mousePosition.x < Screen.width / 2) {
			offset = +60;
		} else {
			offset = -60;
		}
		if (Input.GetMouseButtonDown (1) && CheckMousePosition(Input.mousePosition) ){
			
			createQuickPanel ();

		}

		}	
	public void spawnBranch(){
		if (layerManager.get_active_layer () != null) {
			active_layer = layerManager.get_active_layer ();
		}
		anchorPoint = Camera.main.ScreenToWorldPoint (anchorPoint);
		branchSpawnPoint = new Vector3 (anchorPoint.x, anchorPoint.y, 0f);
		GameObject new_branch=Instantiate (branch, branchSpawnPoint, Quaternion.identity);
		Destroy (QuickP);
		}
	/// <summary>
	/// // Image Spawn 
	/// </summary>
	public void spawnImage(){
		if (layerManager.get_active_layer () != null) {
			active_layer = layerManager.get_active_layer ();
		}
		if (panelIsAlive) {
			Destroy (QuickP);
		}
		panelIsAlive = true;
		anchorPoint = Camera.main.ScreenToWorldPoint (anchorPoint);
		branchSpawnPoint = new Vector3 (anchorPoint.x, anchorPoint.y, 1f);
		GameObject ImageInstance = Instantiate (image, branchSpawnPoint, Quaternion.identity) as GameObject;
		Destroy (QuickP);

	}
	public void spawnTextPanel(){
		
		if (panelIsAlive) {
			Destroy (QuickP);
		}
		panelIsAlive = true;
		anchorPoint = Camera.main.ScreenToWorldPoint (anchorPoint);
		branchSpawnPoint = new Vector3 (anchorPoint.x, anchorPoint.y, 0f);
		Text_P = Instantiate (TextPanel) as GameObject;
		Text_P.transform.SetParent (canvas.transform,false);
		Destroy (QuickP);
		}


	//Check if mouse position in 3d space
	public bool CheckMousePosition(Vector3 checkposition){
		if (checkposition.x < Screen.width && checkposition.x > Screen.width * 0.06f) {
			if (checkposition.y < Screen.height * 0.9f && checkposition.y > 0) {
					return true;
				} else {return false;}
		    }else return false;
		}

	void createQuickPanel(){
		if (panelIsAlive) {
			Destroy (QuickP);
		}
		panelIsAlive = true;
		anchorPoint = Input.mousePosition;
		Panelpos = anchorPoint;
		Panelpos.x += offset;
		Panelpos.y = Mathf.Clamp (Input.mousePosition.y,90f,Screen.height*0.9f-120f);
		if (minQuickP) {
			
			QuickP = Instantiate (noBranchQuickpanel, Panelpos, Quaternion.identity);
			QuickP.transform.SetParent(canvas.transform);
		} else {
			QuickP = Instantiate (Quickpanel, Panelpos, Quaternion.identity);
			QuickP.transform.SetParent(canvas.transform);
		}

	}
	public void spawText(){
		if (layerManager.get_active_layer () != null) {
			active_layer = layerManager.get_active_layer ();
		}

		if (text != null) {
			GameObject textMesh= Instantiate (Text_3d, branchSpawnPoint, Quaternion.identity) ;
			textMesh.GetComponent<TextMesh> ().text = text;
			GameObject enviMan = GameObject.Find("EnvironmentManagement");
			Color c = enviMan.GetComponent<EnvironmentManagement> ().get_color ();
			textMesh.GetComponent<TextMesh> ().color = c;
			text = null;
		}
		Destroy (Text_P);
		
	}
	public void Text_changed(string new_Text){
		text = new_Text;
	}

	public void QuickPanelMaximize(){
		minQuickP = false; 
	}
	public void QuickPanelMinimize(){
		minQuickP = true; 
	}
	public void OverQuickPanelEnter(){
		hoverQuickPanel = true;
	}
	public void OverQuickPanelExit(){
		hoverQuickPanel = false; 
	}
	//

	}


