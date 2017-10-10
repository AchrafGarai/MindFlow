using UnityEngine;
using System.Collections;

// Placeholder for UniqueIdDrawer script
using System;




public class Layer : MonoBehaviour {
	public GameObject Layer_Manager;
	public string uniqueId;
	public bool name_exist;
	void Start(){



		gameObject.name = uniqueId;
		//gameObject.name = uniqueId;

	}
	void Awake(){
		Layer_Manager = GameObject.Find ("LayersManager");
		if (uniqueId.Equals("")) {
			if (name_exist == false) {
				Guid guid = Guid.NewGuid();
				uniqueId= guid.ToString();
				name_exist = true;
			}
		}
	}
	public void Set_name(){
		if (uniqueId.Equals("")) {
			if (name_exist == false) {
				Guid guid = Guid.NewGuid();
				uniqueId= guid.ToString();
				name_exist = true;
			}
		}
	}
	public void Set_Layer(){
		Layer_Manager.GetComponent<LayersManager> ().Set_active_layer(this.gameObject);
	}
	public LayerData data = new LayerData();


	public void StoreData()
	{   data.name_set = name_exist;
		data.name = uniqueId;

	}

	public void LoadData()
	{
		name_exist = data.name_set;
		uniqueId = data.name;
	}

	public void ApplyData()
	{
		SaveData.AddLayerData(data);
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
public class LayerData
{
	public bool name_set;
	public string name;
	public Vector3 pos = new Vector3(0f,0f,0f);

}