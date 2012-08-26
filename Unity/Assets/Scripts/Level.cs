using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Level : MonoBehaviour {
	
	public List<BLOCK> blocks=new List<BLOCK>();
	
	public void AddBlock(Vector3 pos)
	{
		Debug.Log("Adding default block at " + pos.ToString());
		blocks.Add(new BLOCK(pos));		
	}
	public void AddBlock(Vector3 pos, int id)
	{
		Debug.Log("Adding block " + ((Block)id).ToString() + " at " + pos.ToString());
		blocks.Add(new BLOCK(id, pos));
		
	}
	public void AddBlock(Vector3 pos, int id, int metadata)
	{
		Debug.Log("Adding block " + ((Block)id).ToString() + " at " + pos.ToString() + " with metadata " + metadata);
		blocks.Add(new BLOCK(id, pos, metadata));
	}
	public void RemoveBlock(Vector3 pos, GameObject gameobj)
	{
		if(pos != new Vector3(0, 0, 0))
		{
			foreach(BLOCK block in blocks)
			{
				if(block.pos == pos)
				{
					blocks.Remove(block);
					break;
				}
			}
			Destroy(gameobj);
		}
	}
	public List<BLOCK> GetBlocks()
	{
		return blocks;
	}
	void Start()
	{
		blocks.Add(new BLOCK(new Vector3(0, 0, 0)));
		loadname = levelname;
		StartCoroutine(LoadLevel(levelname));
	}
	public string levelname = "";
	private string loadname = "";
	void OnGUI()
	{
		if(GlobalSettings.LevelDev)
		{
			loadname = GUI.TextField(new Rect(0, 0, 200, 20), loadname);
			if(GUI.Button(new Rect(Screen.width-200, 0, 200, 20), "SAVE"))
			{
				SaveLevel(levelname);
			}
			if(GUI.Button(new Rect(Screen.width - 200, 25, 200, 20), "LOAD"))
			{
				StartCoroutine(LoadLevel(loadname));
			}
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
				foreach(BLOCK block in blocks)
				{
					writer.Write(block.id);
					writer.Write(block.metadata);
					writer.Write((int)block.pos.x);
					writer.Write((int)block.pos.y);
					writer.Write((int)block.pos.z);
				}
			}
		}
	}
	IEnumerator LoadLevel(string name)
	{
		Debug.Log("Starting to load level");
		using(FileStream stream = File.Open("Levels/"+name, FileMode.Open))
		{
			using(BinaryReader reader = new BinaryReader(stream))
			{
				blocks = new List<BLOCK>();
				GameObject[] gameobjects = GameObject.FindGameObjectsWithTag("CUBE");
				int lenght = gameobjects.Length;
				Debug.Log(lenght);
				for(int i = 0; i < lenght; i++)
				{
					Destroy(gameobjects[i]);
					Debug.Log("Destroyed " + (i + 1) + " out of " + lenght);
				}
				AddBlock(new Vector3(0, 0, 0));
				try
				{
					Debug.Log("reading");
					int cnt = 0;
					while(reader.PeekChar() != -1)
					{
						int id = reader.ReadInt32();
						int metadata = reader.ReadInt32();
						int x = reader.ReadInt32();
						int y = reader.ReadInt32();
						int z = reader.ReadInt32();
						cnt++;
						AddBlock(new Vector3(x,y,z), id, metadata);
						if(cnt > 25)
						{
							cnt = 0;
							yield return new WaitForEndOfFrame();
						}
					}
				}
				finally
				{
					OnLoad();
				}
			}
		}
	}
	void OnLoad()
	{
		GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMotor>().movement.gravity = 20;
		GameObject.FindGameObjectWithTag("LoadScreen").GetComponent<LoadingScreen>().Stop();
	}
}
