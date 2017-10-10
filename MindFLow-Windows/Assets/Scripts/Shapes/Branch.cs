using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Branch : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler {

	public  bool branchIsDone ;
	bool endbranch=false;
	bool HoverOver;
	public List<GameObject> Points;
	public bool pressed;
	public bool anchorPlaced;
	public PolygonCollider2D polygonCollider;
	// Distance Between points here 
	public float minDistance;
	private Vector2 firstPoint;
	private Vector2 secondPoint;
	// linerenderer
	public LineRenderer lineRenderer;
    float linewidhstart=1f;
	float lineWidhEnd=0.5f;
	//Curver
	public Vector3[] pointsTemp;
	public GameObject pointEmpty;
	//mousemanager
	private MouseManager mouseManager;
	public GameObject layers_Manager;
	public EnvironmentManagement Envi_Manager;


	// Use this for initialization
	void Start () {
	//	gameObject.AddComponent<UniqueID> ();
		List<GameObject> Points = new List<GameObject>();
		lineRenderer = gameObject.AddComponent<LineRenderer> ();
		branchIsDone = false;
		HoverOver=false;
		pressed=false;
		anchorPlaced=false;
		lineRenderer.startWidth = linewidhstart;
		lineRenderer.endWidth = lineWidhEnd;
		lineRenderer.numCapVertices = 5;
		polygonCollider= gameObject.AddComponent<PolygonCollider2D> ();
		lineRenderer.useWorldSpace = false;
		pointEmpty = new GameObject ();
		pointEmpty.transform.SetParent (gameObject.transform);
		// call mouse manager 
		mouseManager = GameObject.FindObjectOfType(typeof(MouseManager)) as MouseManager;
		lineRenderer.material = new Material(Shader.Find("Standard"));
		Set_Width ();
		if (first_load) {
			lineRenderer.material.color = Envi_Manager.get_color ();

			first_load = false;
		} else {
			lineRenderer.material.color = data.color;
		}

		if (data.data_points.Count != 0) {
			pointsTemp = data.data_points.ToArray ();
			branchIsDone = true;
		}
		layers_Manager = GameObject.Find ("LayersManager");
		if (Parent_layer_name.Equals("")) {
			GameObject go = layers_Manager.GetComponent<LayersManager> ().get_active_layer();
			Parent_layer_name = go.name;
			gameObject.transform.SetParent (go.transform);
		} else {
			GameObject go = GameObject.Find (Parent_layer_name);
			gameObject.transform.SetParent (go.transform);
		}
		  
		if (gameObject.transform.parent.name.Equals (layers_Manager.GetComponent<LayersManager> ().active_Layer.name)) {
			UnHide_branch ();
		} else {
			Hide_branch ();
		}

	}
	
	// Update is called once per frame
	void Update () {
		// Trigger to edge collider calculation 
		if(branchIsDone && endbranch == false){
			Points.Clear ();
			if (pointsTemp != null)
				ApplyCurve ();
				CreateCollider ();
			endbranch = true;
			}


		
		if (HoverOver == true && branchIsDone==false) {
			if(Input.GetMouseButtonDown(0)){
				pressed = true;
				if(anchorPlaced==false) {
					GameObject PE = Instantiate (pointEmpty, gameObject.transform.position, Quaternion.identity);
					PE.transform.SetParent (gameObject.transform);
					Points.Add (PE); 
					anchorPlaced = true;
					firstPoint = gameObject.transform.position;
				}
				}
				}

		if(Input.GetMouseButtonUp(0)){
			pressed = false;

			if (Points.Count > 2) {
				branchIsDone = true;
			}
		}

		if (pressed) {
			secondPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			if (Vector2.Distance (firstPoint, secondPoint) > minDistance) {
				firstPoint = secondPoint;
				GameObject pointE = Instantiate (pointEmpty, secondPoint, Quaternion.identity);
				pointE.transform.SetParent (gameObject.transform);
				Points.Add (pointE);
				CurveBranch ();
				}
		}

	}

	void CurveBranch(){
		int counter = 0;
		pointsTemp = new Vector3[Points.Count];
		foreach(GameObject i  in Points){
			pointsTemp [counter].x = Points [counter].transform.localPosition.x;
			pointsTemp [counter].y = Points [counter].transform.localPosition.y;
			pointsTemp [counter].z = 0;
			counter++;
		}
		pointsTemp = Curver.MakeSmoothCurve(pointsTemp,3.0f);
		ApplyCurve ();
	}
	public void SetupBranch(float min){
		this.minDistance = min;

	}

	void ApplyCurve(){
		int counter2=0;
		foreach(Vector3 i  in pointsTemp){

			lineRenderer.numPositions =pointsTemp.Length;
			lineRenderer.SetPosition(counter2, i);
			++counter2;
		}
	}


	void OnMouseOver(){
		HoverOver = true;
	}
	void OnMouseExit(){
		HoverOver = false;
	}


	void CreateCollider(){
		List<Vector2> edgePoints = new List<Vector2> ();

		for (int j = 1; j < pointsTemp.Length; j++) {
			Vector2 distanceBetweenPoints = pointsTemp[j-1] - pointsTemp[j];
			Vector3 crossProduct = Vector3.Cross (distanceBetweenPoints, Vector3.forward);
			Vector2 up = (linewidhstart/ 2f) * 
		     new Vector3(crossProduct.normalized.x, crossProduct.normalized.y,0) + pointsTemp [j-1];
			Vector2 down = -(linewidhstart/ 2f) * 
		    new Vector3(crossProduct.normalized.x, crossProduct.normalized.y,0) + pointsTemp [j-1];

			edgePoints.Insert(0, down);
			edgePoints.Add(up);

			if (j == pointsTemp.Length - 1) {
				// Compute the values for the last point on the Bezier curve
			up = (linewidhstart/ 2f) * new Vector3(crossProduct.normalized.x, 
					crossProduct.normalized.y,0) + pointsTemp [j];
			down = -(linewidhstart / 2) * new Vector3(crossProduct.normalized.x, 
					crossProduct.normalized.y,0) + pointsTemp [j];

			edgePoints.Insert(0, down);
			edgePoints.Add(up);
			}
		}

		polygonCollider.points = edgePoints.ToArray();

	}

	public void OnPointerEnter(PointerEventData eventData){
		mouseManager.QuickPanelMaximize ();
	}
	public void OnPointerExit(PointerEventData eventData){
		mouseManager.QuickPanelMinimize ();
	}
	public void switch_Color(Color new_color){
		lineRenderer.material.color = Envi_Manager.get_color ();
	}
	void OnGUI() 
	{
		Event e = Event.current;
		if (e.isMouse && e.type == EventType.MouseDown && e.clickCount == 2 && HoverOver)
		{
			Envi_Manager.Set_Object (this.gameObject);
		}
	}

	void Set_Width(){
		Vector2 pos = new Vector2 (0, 0);
		pos.x = gameObject.transform.position.x;
		pos.y =gameObject.transform.position.y;
		if (pos.x < 10) {
			lineRenderer.startWidth = 2;
			lineRenderer.endWidth = 1;

		} else if ((pos.x < 50) && (pos.x>10)) {
			lineRenderer.startWidth = 1;
			lineRenderer.endWidth = 0.5f;
		}else  {
			lineRenderer.startWidth = 1;
			lineRenderer.endWidth = 0.5f;
		}
	}
	/// <summary>
	/// /Hide / show 
	/// </summary>
	public void Hide_branch(){

		lineRenderer.enabled = false;
		polygonCollider.enabled = false;
		gameObject.GetComponent<MeshRenderer> ().enabled = false;
	}
	public void UnHide_branch(){
		lineRenderer.enabled = true;
		polygonCollider.enabled = true;
		gameObject.GetComponent<MeshRenderer> ().enabled = true;
	}
	///////////////////////////
	/// <summary>
	/// json store branch 
	/// </summary>

	public branchData data = new branchData();
	public Color col;
	public bool first_load=true;
	public string Parent_layer_name;

	public void StoreData()
	{
		
		if (data != null) {
			
			data.pos = transform.position;
			data.rot = transform.rotation;

			}
		data.data_points.Clear ();
		for(int i=0;i<pointsTemp.Length;i++){
			data.data_points.Add (pointsTemp [i]);
		}
		if (gameObject.GetComponent<LineRenderer> () != null) {
			data.color = lineRenderer.material.color;
		}
		data.firstload = first_load;
		data.layer=Parent_layer_name;
	}

	public void LoadData()
	{
		
		for(int i=0;i<pointsTemp.Length;i++){
			pointsTemp [i] = data.data_points [i];
		}
		transform.position = data.pos;
		transform.rotation=data.rot ;
		if (gameObject.GetComponent<LineRenderer> () != null) {
			lineRenderer.material.color = data.color; 
		}
		first_load = data.firstload;
		Parent_layer_name = data.layer;
	}

	public void ApplyData()
	{
		SaveData.AddbranchData(data);
	}

	void OnEnable()
	{
		SaveData.OnLoaded += LoadData;
		SaveData.OnBeforeSave += StoreData;
		SaveData.OnBeforeSave += ApplyData;
	}

	void OnDisable()
	{
		SaveData.OnLoaded -= LoadData;
		SaveData.OnBeforeSave -= StoreData;
		SaveData.OnBeforeSave -= ApplyData;
	}
}
[Serializable]
public class branchData
{
	public List<Vector3> data_points = new List<Vector3>();
	public string name;
	public Vector3 pos;
	public Quaternion rot;
	public Color color;
	public bool firstload;
	public string layer;

}