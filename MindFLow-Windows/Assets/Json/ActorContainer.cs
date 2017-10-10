using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class ActorContainer {
	public List<TextData> Texts = new List<TextData>();
	public List<ImageData> Images= new List<ImageData>();
	public List<branchData> branches= new List<branchData>();
	public List<LayerData> Layers= new List<LayerData>();
}
