using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Blockmechanics : MonoBehaviour {
	
	public delegate void CollisionDelegate(BLOCK block);
	public delegate void GameTickDelegate(BLOCK block);
	
	static CollisionDelegate[] collisionenter = new CollisionDelegate[10];
	GameTickDelegate[] tick = new GameTickDelegate[10];
	
	void Start()
	{
		InvokeRepeating("Tick", 0, 15);
	}
	public void Tick () {
		BLOCK me = null;
		Level level = GameObject.FindGameObjectWithTag("Level").GetComponent<Level>();
		foreach(BLOCK block in level.GetBlocks())
		{
			if(block.pos == transform.position)
			{
				me = block;
				break;
			}
		}
		GameTickDelegate tickdel = tick[gameObject.GetComponent<BlockData>().id];
		if(tickdel != null)
			tickdel(me);
	}
	public static void CharacterCollision(Vector3 pos, int id)
	{
		//Debug.Log("Collision");
		BLOCK me = null;
		Level level = GameObject.FindGameObjectWithTag("Level").GetComponent<Level>();
		foreach(BLOCK block in level.GetBlocks())
		{
			if(block.pos == pos)
			{
				me = block;
				break;
			}
		}
		CollisionDelegate col = collisionenter[id];
		if(col != null)
			col(me);
	}
	
	void Awake()
	{
		//collisionenter[1] = LevelChanger.CollisionHandler;
		tick[2] = SetMetadataRotation;
        tick[4] = gameObject.GetComponent<Electrocuter>().Tick;
	}
	
	
	
	void SetMetadataRotation(BLOCK block)
	{
		if(gameObject == null)
			return;
		switch(gameObject.GetComponent<BlockData>().metadata - (int)(block.metadata / 10f)*10)
		{
		case 0:
			break;
		case 1:
				gameObject.transform.eulerAngles = new Vector3(180, 0, 0);
			break;
		case 3:
				gameObject.transform.eulerAngles = new Vector3(90, 0, 0);
			break;
		case 4:
				gameObject.transform.eulerAngles = new Vector3(90, 0, 90);
			break;
		case 5:
				gameObject.transform.eulerAngles = new Vector3(90, 0, 180);
			break;
		case 6:
				gameObject.transform.eulerAngles = new Vector3(90, 0, 270);
			break;
		}
	}
}