using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Button saveButton;
    public Button loadButton;


	public GameObject playerPrefab;
    public const string TextPath = "Managers/Text";
	public const string ImagePath = "Managers/Image";
	public const string branchPath = "Managers/branch";
	public const string LayerPath = "Elements/Layer";

    private static string dataPath = string.Empty;

    void Awake()
    {
		dataPath = System.IO.Path.Combine(Application.persistentDataPath, PlayerPrefs.GetString("Active Project"));
    }

    void Start()
	{   
			Load ();
			


    }

	/// <summary>
	/// Create Layers
	/// </summary>
	public static Layer CreateLayer(string path, Vector3 position, Quaternion rotation)
	{
		GameObject prefab = Resources.Load<GameObject>(path);

		GameObject go = Instantiate(prefab, position, rotation) as GameObject;

		Layer  actor = go.GetComponent<Layer>() ?? go.AddComponent<Layer>();


		return actor;
	}

	public static Layer CreateLayer(LayerData data, string path, Vector3 position, Quaternion rotation)
	{
		Layer actor = CreateLayer(path, position, rotation);

		actor.data = data;


		return actor;

	}
    public static text CreateText(string path, Vector3 position, Quaternion rotation)
    {
        GameObject prefab = Resources.Load<GameObject>(path);

        GameObject go = Instantiate(prefab, position, rotation) as GameObject;

		text actor = go.GetComponent<text>() ?? go.AddComponent<text>();

        return actor;
    }

    public static text CreateText(TextData data, string path, Vector3 position, Quaternion rotation)
    {
		text actor = CreateText(path, position, rotation);

        actor.data = data;
		actor.GetComponent<TextMesh> ().text = data.name;
		actor.GetComponent<TextMesh> ().color = data.color;
		actor.transform.rotation = data.rot;
		actor.transform.localScale = data.scale;
        return actor;

    }
	public static Image CreateImage(string path, Vector3 position, Quaternion rotation)
	{
		GameObject prefab = Resources.Load<GameObject>(path);

		GameObject go = Instantiate(prefab, position, rotation) as GameObject;

		Image actor = go.GetComponent<Image>() ?? go.AddComponent<Image>();

		return actor;
	}

	public static Image CreateImage(ImageData data, string path, Vector3 position, Quaternion rotation)
	{
		Image actor = CreateImage(path, position, rotation);

		actor.data_image = data;
		actor.transform.rotation = data.rot;
		actor.transform.localScale = data.scale;

		return actor;

	}
	/// <summary>
	/// branch store 
	/// </summary>
	/// 
	public static Branch Createbranch(string path, Vector3 position, Quaternion rotation)
	{
		GameObject prefab = Resources.Load<GameObject>(path);

		GameObject go = Instantiate(prefab, position, rotation) as GameObject;

		Branch actor = go.GetComponent<Branch>() ?? go.AddComponent<Branch>();


		return actor;
	}

	public static Branch Createbranch(branchData data, string path, Vector3 position, Quaternion rotation)
	{
		Branch actor = Createbranch(path, position, rotation);

		actor.data = data;
		actor.transform.rotation = data.rot;

		return actor;

	}


	/// <summary>
	/// Save this instance.
	/// </summary>
	public void Save()
	{
		
			SaveData.Save(dataPath, SaveData.actorContainer);

	}

	public void Load()
	{
		SaveData.Load(dataPath);
	}

    void OnEnable()
    {
        saveButton.onClick.AddListener(Save);
        loadButton.onClick.AddListener(Load);
    }
    void OnDisable()
    {
        saveButton.onClick.RemoveListener(Save);
		loadButton.onClick.RemoveListener(Load);
    }
}
