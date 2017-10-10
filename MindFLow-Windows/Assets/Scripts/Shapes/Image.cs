using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SFB;
using System;


public class Image : Shape {
	string _path=null;
	Sprite tex;
	SpriteRenderer spriteRenderer ;
	public EnvironmentManagement Envi_Manager;
	public string layer_name;
	public GameObject layers_Manager;

	protected virtual void Start(){
		base.Start ();
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		if (_path == null) {
			WriteResult (StandaloneFileBrowser.OpenFilePanel ("Open File", "", "", false));
		}

		Addimage (_path);
		AddCollider ();
		layers_Manager = GameObject.Find ("LayersManager");
		if (layer_name.Equals("")) {
			GameObject go = layers_Manager.GetComponent<LayersManager> ().get_active_layer();
			layer_name = go.name;
			gameObject.transform.SetParent (go.transform);
		} else {
			GameObject go = GameObject.Find (layer_name);
			gameObject.transform.SetParent (go.transform);
			layer_name=go.name;
		}
		if (gameObject.transform.parent.name.Equals (layers_Manager.GetComponent<LayersManager> ().active_Layer.name)) {
			UnHide_image ();
		} else {
			Hide_image ();
		}


	}
	public void Hide_image(){
		spriteRenderer.enabled = false;
		gameObject.GetComponent<BoxCollider2D> ().enabled = false;

	}
	public void UnHide_image(){
		spriteRenderer.enabled = true;
		gameObject.GetComponent<BoxCollider2D> ().enabled = true;

	}


	public void Addimage(string path){

		tex = LoadNewSprite(path,100f);
	
		spriteRenderer.sprite = tex;
	}
	public Sprite LoadNewSprite(string FilePath, float PixelsPerUnit = 100.0f) {
	    Sprite NewSprite = new Sprite();
		Texture2D SpriteTexture = LoadTexture(FilePath);
		NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height),new Vector2(0,0), PixelsPerUnit);

		return NewSprite;
	}

	public Texture2D LoadTexture(string FilePath) {
		Texture2D Tex2D;
		byte[] FileData;
		if (File.Exists(FilePath)){
			FileData = File.ReadAllBytes(FilePath);
			Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
			if (Tex2D.LoadImage(FileData))           // Load the imagedata into the texture (size is set automatically)
				return Tex2D;                 // If data = readable -> return texture
		}  
		return null;                     // Return null if load failed
	}
	private void WriteResult(string[] paths) {
		if (paths.Length == 0) {
			return;
		}
		_path = "";
		foreach (var p in paths) {
			_path += p ;
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

	public override void Set_active_layer ()
	{
		base.Set_active_layer ();
	}

	/// <summary>
	/// / json save 
	/// </summary>
	public ImageData data_image = new ImageData();



	void StoreData()
	{   	
		
		if (data_image != null) {
				data_image.name = _path;
				data_image.pos = transform.position;
				data_image.rot = transform.rotation;
				data_image.scale = transform.localScale;

		}


		data_image.layer_Link=layer_Link_name ;
		data_image.layer_name = layer_name;

	}

	void LoadData()
	{
		_path = data_image.name;
		transform.position = data_image.pos;
		transform.rotation=data_image.rot ;
		transform.localScale=data_image.scale ;
		layer_Link_name = data_image.layer_Link;
		layer_name = data_image.layer_name;

	}

	void ApplyData()
	{
		SaveData.AddImageData(data_image);
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
public class ImageData
{
	public string name;
	public string layer_Link;
	public string layer_name;
	public Vector3 pos;
	public Quaternion rot;
	public Vector3 scale;


}