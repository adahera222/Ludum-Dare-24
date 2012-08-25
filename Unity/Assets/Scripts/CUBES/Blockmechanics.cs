using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Blockmechanics : MonoBehaviour {
	
	public delegate void CollisionDelegate(BLOCK block);
	public delegate void GameTickDelegate(float deltatime, BLOCK block);
	
	static CollisionDelegate[] collisionenter = new CollisionDelegate[3];
	GameTickDelegate[] tick = new GameTickDelegate[3];
	
	// Update is called once per frame
	void Update () {
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
			tickdel(Time.deltaTime, me);
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
		collisionenter[1] = LevelChanger.CollisionHandler;
		tick[2] = SetMetadataRotation;
	}
	
	void SetMetadataRotation(float f, BLOCK B)
	{
		if(gameObject == null)
			return;
		switch(gameObject.GetComponent<BlockData>().metadata)
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