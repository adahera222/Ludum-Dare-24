using UnityEngine;
using System.Collections;
public enum Block
{
	BaseBlock,
	LevelChanger,
	Lamp,
	Bouncer,
	Electrocuter
}

public class BLOCK {
	public int id;
	public int metadata;
	public Vector3 pos;
	public BLOCK(Vector3 position)
	{
		this.pos = position;
		id = (int)Block.BaseBlock;
		this.metadata = 0;
	}
	public BLOCK(int id, Vector3 position)
	{
		this.pos = position;
		this.id = id;
		this.metadata = 0;
	}
	public BLOCK(int id, Vector3 position, int metadata)
	{
		this.pos = position;
		this.id = id;
		this.metadata = metadata;
	}
}
