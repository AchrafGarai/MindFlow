using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using System;
//
public class text : Shape {
	TextMesh textMesh;
	string _text;

	public float pixelSize = 1;
	public Color outlineColor = Color.black;
	public bool resolutionDependant = false;
	public int doubleResolution = 1024;
	private MeshRenderer meshRenderer;
	public EnvironmentManagement Envi_Manager;
	public string layer_name;
	public GameObject layers_Manager;
	protected virtual void Start(){
		base.Start ();

		layers_Manager = GameObject.Find ("LayersManager");
		textMesh = gameObject.GetComponent<TextMesh> ();
		AddCollider ();
		meshRenderer = GetComponent<MeshRenderer>();

		for (int i = 0; i < 8; i++) {
			GameObject outline = new GameObject("outline", typeof(TextMesh));
			outline.transform.SetParent (gameObject.transform);
			Quaternion rot = new Quaternion ();
			outline.transform.rotation = rot;
			outline.transform.localScale = new Vector3(1, 1, 1);

			MeshRenderer otherMeshRenderer = outline.GetComponent<MeshRenderer>();
			otherMeshRenderer.material = new Material(meshRenderer.material);

			otherMeshRenderer.receiveShadows = false;
			otherMeshRenderer.sortingLayerID = meshRenderer.sortingLayerID;
			otherMeshRenderer.sortingLayerName = meshRenderer.sortingLayerName;
		}
		if (layer_name.Equals("")) {
			GameObject go = layers_Manager.GetComponent<LayersManager> ().get_active_layer();
			layer_name = go.name;
			gameObject.transform.SetParent (go.transform);
		} else {
			GameObject go = GameObject.Find (layer_name);
			gameObject.transform.SetParent (go.transform);
		}

		if (gameObject.transform.parent.name.Equals (layers_Manager.GetComponent<LayersManager> ().active_Layer.name)) {
			UnHide_text ();
		} else {
			Hide_text ();
		}

		}
void LateUpdate() {
		Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);

		outlineColor.a = textMesh.color.a * textMesh.color.a;

		// copy attributes
		for (int i = 0; i < transform.childCount; i++) {

			TextMesh other = transform.GetChild(i).GetComponent<TextMesh>();
			other.color = outlineColor;
			other.text = textMesh.text;
			other.alignment = textMesh.alignment;
			other.anchor = textMesh.anchor;
			other.characterSize = textMesh.characterSize;
			other.font = textMesh.font;
			other.fontSize = textMesh.fontSize;
			other.fontStyle = textMesh.fontStyle;
			other.richText = textMesh.richText;
			other.tabSize = textMesh.tabSize;
			other.lineSpacing = textMesh.lineSpacing;
			other.offsetZ = textMesh.offsetZ;

			bool doublePixel = resolutionDependant && (Screen.width > doubleResolution || Screen.height > doubleResolution);
			Vector3 pixelOffset = GetOffset(i) * (doublePixel ? 2.0f * pixelSize : pixelSize);
			Vector3 worldPoint = Camera.main.ScreenToWorldPoint(screenPoint + pixelOffset);
			other.transform.position = worldPoint;

			MeshRenderer otherMeshRenderer = transform.GetChild(i).GetComponent<MeshRenderer>();
			otherMeshRenderer.sortingLayerID = meshRenderer.sortingLayerID;
			otherMeshRenderer.sortingLayerName = meshRenderer.sortingLayerName;
		}
	}

	Vector3 GetOffset(int i) {
		switch (i % 8) {
		case 0: return new Vector3(0, 1, 0);
		case 1: return new Vector3(1, 1, 0);
		case 2: return new Vector3(1, 0, 0);
		case 3: return new Vector3(1, -1, 0);
		case 4: return new Vector3(0, -1, 0);
		case 5: return new Vector3(-1, -1, 0);
		case 6: return new Vector3(-1, 0, 0);
		case 7: return new Vector3(-1, 1, 0);
		default: return Vector3.zero;
		}
}
	void OnGUI() 
	{
		Event e = Event.current;
		if (e.isMouse && e.type == EventType.MouseDown && e.clickCount == 2 && hover_over)
		{
			Envi_Manager.Set_Object (this.gameObject);
		}
	}
	public  void switch_Color(Color new_color){
		textMesh.color = new_color;
	}
	public override void Set_active_layer ()
	{
		base.Set_active_layer ();
		print ("set layer");
	}


	/// <summary>
	/// /Hide / show 
	/// </summary>
	public void Hide_text(){
		MeshRenderer[] meshRenderer;
		meshRenderer = gameObject.GetComponentsInChildren<MeshRenderer> ();
		foreach (MeshRenderer sp in meshRenderer) {
			sp.enabled = false;
		}
		gameObject.GetComponent<MeshRenderer>().enabled= false;
		gameObject.GetComponent<BoxCollider2D> ().enabled = false;
	}
	public void UnHide_text(){
		MeshRenderer[] meshRenderer;
		meshRenderer = gameObject.GetComponentsInChildren<MeshRenderer> ();
		foreach (MeshRenderer sp in meshRenderer) {
			sp.enabled = true;
		}
		gameObject.GetComponent<BoxCollider2D> ().enabled = true;
	}
	///////////////////////////
	public TextData data = new TextData();

	public Vector3 scale; 

	public Quaternion rot;



	void StoreData()
	{
		
		if (data != null) {
			data.name = textMesh.text;
			data.pos = transform.position;
			data.rot = transform.rotation;
		    data.scale = transform.localScale;
			data.color = textMesh.color;
		}
		data.layer_Link=layer_Link_name ;
		data.layer_name = layer_name;
	}

	void LoadData()
	{
		name = data.name;
		transform.position = data.pos;
		if (textMesh !=null) textMesh.color = data.color;
		transform.rotation=data.rot ;
		transform.localScale=data.scale ;
		layer_Link_name = data.layer_Link;
		layer_name = data.layer_name;
	}

    void ApplyData()
	{
		SaveData.AddTextData(data);
	}
	/// <summary>
	/// /
	/// </summary>
	public void VisibleAction(){

		GameObject go = layers_Manager.GetComponent<LayersManager> ().active_Layer;
		string name;
		if (go.GetComponent<Layer>() != null) {
			name = go.GetComponent<Layer> ().uniqueId;
		} else {
			name = go.name;
		}

		if (layer_name.Equals (name)) {
			if (gameObject.GetComponent<BoxCollider2D> () == false) {
				gameObject.GetComponent<BoxCollider2D> ().enabled = true;

			}



		} else {
			
			gameObject.GetComponent<BoxCollider2D> ().enabled = false;
		}

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

	/// ////////////////////
}

[Serializable]
public class TextData
{
	public string layer_Link;
	public string layer_name;
	public string name;
	public Vector3 pos;
	public Quaternion rot;
	public Vector3 scale;
	public Color color;

}

