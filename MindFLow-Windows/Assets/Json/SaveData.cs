using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;

public class SaveData
{

    public static ActorContainer actorContainer = new ActorContainer();

    public delegate void SerializeAction();
    public static event SerializeAction OnLoaded;
    public static event SerializeAction OnBeforeSave;

    public static void Load(string path)
    {
        actorContainer = LoadActors(path);
		foreach (LayerData data in actorContainer.Layers)
		{
			GameController.CreateLayer(data, GameController.LayerPath,
				data.pos, Quaternion.identity);
		}
        foreach (TextData data in actorContainer.Texts)
        {
            GameController.CreateText(data, GameController.TextPath,
                data.pos, Quaternion.identity);
        }
		foreach (ImageData data in actorContainer.Images)
		{
			GameController.CreateImage(data, GameController.ImagePath,
				data.pos, Quaternion.identity);
		}
		foreach (branchData data in actorContainer.branches)
		{
			GameController.Createbranch(data, GameController.branchPath,
				data.pos, Quaternion.identity);
		}

        OnLoaded();

		ClearActorList();
    }

    public static void Save(string path, ActorContainer actors)
    {
		
			OnBeforeSave();

			//ClearSave(path);

			SaveActors(path, actors);

			ClearActorList();


    }

    public static void AddTextData(TextData data)
    {
        actorContainer.Texts.Add(data);
    }
	public static void AddImageData(ImageData data)
	{
		actorContainer.Images.Add(data);
	}
	public static void AddbranchData(branchData data)
	{
		actorContainer.branches.Add(data);
	}
	public static void AddLayerData(LayerData data)
	{
		actorContainer.Layers.Add(data);
	}
	public static void ClearActorList()
    {
        actorContainer.Texts.Clear();
		actorContainer.Images.Clear ();
		actorContainer.branches.Clear ();
		actorContainer.Layers.Clear ();
    }

    private static ActorContainer LoadActors(string path)
    {
        string json = File.ReadAllText(path);

		return JsonUtility.FromJson<ActorContainer>(json);
    }

    private static void SaveActors(string path, ActorContainer actors)
    {
		string json = JsonUtility.ToJson(actors);

		StreamWriter sw = File.CreateText(path);
		sw.Close();

		File.WriteAllText(path, json);
    }
}
