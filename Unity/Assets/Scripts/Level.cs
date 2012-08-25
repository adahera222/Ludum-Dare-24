using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Level : MonoBehaviour {
	
	public List<Vector3> blocks=new List<Vector3>();
	public Object CUBE;
	
	public void AddBlock(Vector3 pos)
	{
		blocks.Add(pos);
		GameObject.Instantiate(CUBE, pos, Quaternion.identity);
	}
	public void RemoveBlock(Vector3 pos, GameObject gameobj)
	{
		if(pos != new Vector3(0, 0, 0))
		{
			foreach(Vector3 block in blocks)
							{
								if((int)block.x == (int)pos.x & (int)block.y == (int)pos.y & (int)block.z == (int)pos.z)
								{
									blocks.Remove(block);
									break;
								}
							}
			Destroy(gameobj);
		}
	}
	public List<Vector3> GetBlocks()
	{
		return blocks;
	}
	void Start()
	{
		blocks.Add(new Vector3(0, 0, 0));
		loadname = levelname;
	}
	public string levelname = "";
	private string loadname = "";
	void OnGUI()
	{
		loadname = GUI.TextField(new Rect(0, 0, 200, 20), loadname);
		if(GUI.Button(new Rect(Screen.width-200, 0, 200, 20), "SAVE"))
		{
			SaveLevel(levelname);
		}
		if(GUI.Button(new Rect(Screen.width - 200, 25, 200, 20), "LOAD"))
		{
			LoadLevel(loadname);
		}
	}
	void SaveLevel(string name)
	{
		if(!Directory.Exists("Levels"))
		{
			Directory.CreateDirectory("Levels");
			Debug.Log("Created DIR");
		}
		using(FileStream stream = File.Open("Levels/"+name, FileMode.Create, FileAccess.Write))
		{
			using(BinaryWriter writer = new BinaryWriter(stream))
			{
				foreach(Vector3 block in blocks)
				{
					writer.Write((int)block.x);
					writer.Write((int)block.y);
					writer.Write((int)block.z);
				}
			}
		}
	}
	void LoadLevel(string name)
	{
		using(FileStream stream = File.Open("Levels/"+name, FileMode.Open))
		{
			using(BinaryReader reader = new BinaryReader(stream))
			{
				blocks = new List<Vector3>();
				try
				{
					while(true)
					{
						int x = reader.ReadInt32();
						int y = reader.ReadInt32();
						int z = reader.ReadInt32();
						AddBlock(new Vector3(x, y, z));
						//Debug.Log("Loaded cube at " + x + ":" + y + ":" + z);
					}
				}
				catch
				{}
			}
		}
	}
}
