using UnityEngine;
using System.Collections;
public enum Block
{
	BaseBlock,
	LevelChanger,
	Lamp,
	Bouncer,
	Electrocuter,
	Ice
}

public class BLOCK {
	public sbyte id;
	public int metadata;
	public Vector3 pos;
	public int reference;
	public GameObject obj;
	private static int lastreference;
	public static int GetNewReference()
	{
		lastreference++;
		return lastreference - 1;
	}
	public BLOCK(Vector3 position)
	{
		this.pos = position;
		id = (int)Block.BaseBlock;
		this.metadata = 0;
		GameObject go = (GameObject)GameObject.Instantiate(GlobalSettings.idtable[0], position, Quaternion.identity);
		go.AddComponent<Blockmechanics>();
		go.GetComponent<BlockData>().id = 0;
		go.GetComponent<BlockData>().metadata = 0;
		this.reference = GetNewReference();
		this.obj = go;
		go.GetComponent<BlockData>().reference = reference;
	}
	public BLOCK(sbyte id, Vector3 position)
	{
		this.pos = position;
		this.id = id;
		this.metadata = 0;
		GameObject go = (GameObject)GameObject.Instantiate(GlobalSettings.idtable[id], position, Quaternion.identity);
		go.AddComponent<Blockmechanics>();
		go.GetComponent<BlockData>().id = id;
		go.GetComponent<BlockData>().metadata = 0;
		this.reference = GetNewReference();
		this.obj = go;
		go.GetComponent<BlockData>().reference = reference;
	}
	public BLOCK(sbyte id, Vector3 position, int metadata)
	{
		this.pos = position;
		this.id = id;
		this.metadata = metadata;
		GameObject go = (GameObject)GameObject.Instantiate(GlobalSettings.idtable[id], pos, Quaternion.identity);
		go.AddComponent<Blockmechanics>();
		go.GetComponent<BlockData>().id = id;
		go.GetComponent<BlockData>().metadata = metadata;
		this.reference = GetNewReference();
		this.obj = go;
		go.GetComponent<BlockData>().reference = reference;
	}
}
