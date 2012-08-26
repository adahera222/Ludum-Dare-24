using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Level : MonoBehaviour {
	
	private List<BLOCK> blocks=new List<BLOCK>();
	/*private int maxX = 0;
	private int minX = 0;
	private int maxY = 0;
	private int minY = 0;
	private int maxZ = 0;
	private int minZ = 0;*/
	
	public void AddBlock(Vector3 pos)
	{
		//Debug.Log("Adding default block at " + pos.ToString());
		blocks.Add(new BLOCK(pos));
		/*if((int)pos.x > maxX)
			maxX = (int)pos.x;
		if((int)pos.y > maxY)
			maxY = (int)pos.y;
		if((int)pos.z > maxZ)
			maxZ = (int)pos.z;
		if((int)pos.x < minX)
			minX = (int)pos.x;
		if((int)pos.y < minY)
			minY = (int)pos.y;
		if((int)pos.z < minZ)
			minZ = (int)pos.z;*/
	}
	public void AddBlock(Vector3 pos, sbyte id)
	{
		//Debug.Log("Adding block " + ((Block)id).ToString() + " at " + pos.ToString());
		blocks.Add(new BLOCK(id, pos));
		/*if((int)pos.x > maxX)
			maxX = (int)pos.x;
		if((int)pos.y > maxY)
			maxY = (int)pos.y;
		if((int)pos.z > maxZ)
			maxZ = (int)pos.z;
		if((int)pos.x < minX)
			minX = (int)pos.x;
		if((int)pos.y < minY)
			minY = (int)pos.y;
		if((int)pos.z < minZ)
			minZ = (int)pos.z;*/
	}
	public void AddBlock(Vector3 pos, sbyte id, int metadata)
	{
		//Debug.Log("Adding block " + ((Block)id).ToString() + " at " + pos.ToString() + " with metadata " + metadata);
		blocks.Add(new BLOCK(id, pos, metadata));
		/*if((int)pos.x > maxX)
			maxX = (int)pos.x;
		if((int)pos.y > maxY)
			maxY = (int)pos.y;
		if((int)pos.z > maxZ)
			maxZ = (int)pos.z;
		if((int)pos.x < minX)
			minX = (int)pos.x;
		if((int)pos.y < minY)
			minY = (int)pos.y;
		if((int)pos.z < minZ)
			minZ = (int)pos.z;*/
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
		int maxX = 0;
		int minX = 0;
		int maxY = 0;
		int minY = 0;
		int maxZ = 0;
		int minZ = 0;
		foreach(BLOCK block in blocks)
		{
			Vector3 pos = block.pos;
			if((int)pos.x > maxX)
			maxX = (int)pos.x;
			if((int)pos.y > maxY)
				maxY = (int)pos.y;
			if((int)pos.z > maxZ)
				maxZ = (int)pos.z;
			if((int)pos.x < minX)
				minX = (int)pos.x;
			if((int)pos.y < minY)
				minY = (int)pos.y;
			if((int)pos.z < minZ)
				minZ = (int)pos.z;
		}
		float start = Time.realtimeSinceStartup;
		if(!Directory.Exists("Levels"))
		{
			Directory.CreateDirectory("Levels");
			Debug.Log("Created DIR");
		}
		//List<int> meta = new List<int>();
		using(FileStream stream = File.Open("Levels/"+name + ".blocks", FileMode.Create, FileAccess.Write))
		{
			using(BinaryWriter writer = new BinaryWriter(stream))
			{
				using(FileStream metastream = File.Open("Levels/"+name+".meta", FileMode.Create, FileAccess.Write))
				{
					using(BinaryWriter metawriter = new BinaryWriter(metastream))
					{
						writer.Write((short)maxX);
						writer.Write((short)maxY);
						writer.Write((short)maxZ);
						writer.Write((short)minX);
						writer.Write((short)minY);
						writer.Write((short)minZ);
						bool[,,] exists = new bool[maxX - minX + 1, maxY - minY + 1, maxZ - minZ + 1];
						sbyte[,,] ids = new sbyte[maxX - minX + 1, maxY - minY + 1, maxZ - minZ + 1];
						int[,,] metas = new int[maxX - minX + 1, maxY - minY + 1, maxZ - minZ + 1];
						foreach(BLOCK block in blocks)
						{
							Vector3 arraypos = new Vector3((int)block.pos.x - minX, (int)block.pos.y - minY, (int)block.pos.z - minZ);
							/*Debug.Log(maxX);
							Debug.Log(maxY);
							Debug.Log(maxZ);
							Debug.Log(minX);
							Debug.Log(minY);
							Debug.Log(minZ);
							Debug.Log((maxX - minX + 1) + ":" + (maxY - minY + 1) + ":" + (maxZ - minZ + 1));
							Debug.Log(arraypos.ToString());
							Debug.Log(block.pos.ToString());*/
							ids[(int)arraypos.x, (int)arraypos.y, (int)arraypos.z] = block.id;		
							exists[(int)arraypos.x, (int)arraypos.y, (int)arraypos.z] = true;
							metas[(int)arraypos.x, (int)arraypos.y, (int)arraypos.z] = block.metadata;
						}
						sbyte airlenght = 0;
						for(int x = 0; x <= maxX - minX; x++)
						{
							for(int y = 0; y <= maxY - minY; y++)
							{
								for(int z = 0; z <= maxZ - minZ; z++)
								{									
									//Debug.Log(x + ":" + y + ":" + z);
									if(exists[x,y,z])
									{
										if(airlenght < 0)
										{
											writer.Write(airlenght);
											airlenght = 0;
										}
										writer.Write(ids[x,y,z]);
										if(GlobalSettings.UsesMetaData[ids[x,y,z]])
										{
											metawriter.Write(metas[x,y,z]);
										}
									}
									else
									{
										airlenght -= 1;
										if(airlenght == -127)
										{
											writer.Write((sbyte)-127);
											airlenght = 0;
										}
									}
								}
							}
						}
					}
				}
			}
		}
		Debug.Log(Time.realtimeSinceStartup - start);
	}
	IEnumerator LoadLevel(string name)
	{
		GameObject[] gameobjects = GameObject.FindGameObjectsWithTag("CUBE");
		
		int maxX = 0;
		int minX = 0;
		int maxY = 0;
		int minY = 0;
		int maxZ = 0;
		int minZ = 0;
		Debug.Log("Starting to load level");
		float start = Time.realtimeSinceStartup;
		using(FileStream stream = File.Open("Levels/"+name + ".blocks", FileMode.Open))
		{
			using(BinaryReader reader = new BinaryReader(stream))
			{
				using(FileStream metastream = File.Open("Levels/"+name + ".meta", FileMode.Open))
				{
					using(BinaryReader metareader = new BinaryReader(metastream))
					{
						maxX = reader.ReadInt16();
						maxY = reader.ReadInt16();
						maxZ = reader.ReadInt16();
						minX = reader.ReadInt16();
						minY = reader.ReadInt16();
						minZ = reader.ReadInt16();
						
						//int xlenght = maxX - minX;
						//int ylenght = maxY - minY;
						//int zlenght = maxZ - minZ;
						int x = minX;
						int y = minY;
						int z = minZ;
						int cnt = 0;
						int total = 0;
						foreach(GameObject obj in gameobjects)
						{
							Destroy(obj);
						}
						blocks = new List<BLOCK>();
						while(stream.Position < stream.Length)
						{
							sbyte id = reader.ReadSByte();
							int metadata = 0;							
							if(id >= 0)
							{
								cnt++;
								total++;
								if(GlobalSettings.UsesMetaData[id])
								{
									try{
									metadata = metareader.ReadInt32();
									}
									catch{}
								}
								AddBlock(new Vector3(x, y, z), id, metadata);
								z+= 1;
								if (z > maxZ)
								{									
									z = minZ;
									y += 1;
									if(y > maxY)
									{
										y = minY;
										x += 1;
										if(x > maxX)
											break;
									}
								}
							}
							else
							{
								cnt += 1;
								int amount = -id;
								for(int i = 0; i < amount; i++)
								{
									z+= 1;
									if (z > maxZ)
									{
										int diff = maxZ - minZ;
										z = minZ;
										y += 1;
										if(y > maxY)
										{
											y = minY;
											x += 1;
											if(x > maxX)
												break;
										}
									}
								}
							}							
							
							if(cnt > 75)
							{
								cnt = 0;
								yield return new WaitForEndOfFrame();
							}
						}
						Debug.Log(total);
					}
				}
			}
		}
		OnLoad();
		Debug.Log(Time.realtimeSinceStartup - start);
	}
	void OnLoad()
	{
		GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMotor>().movement.gravity = 20;
		GameObject.FindGameObjectWithTag("LoadScreen").GetComponent<LoadingScreen>().Stop();
	}
}
