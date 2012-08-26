using UnityEngine;
using System.Collections;
using System.Threading;

public class LevelChanger : MonoBehaviour {
	public string[] LevelNames;
	public static string[] LevelName;
	
	void Awake()
	{
		LevelName = LevelNames;
	}
	
	public static void CollisionHandler(BLOCK block)
	{
		GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMotor>().movement.gravity = 0;
		GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0, 1.5f, 0);
		GameObject.FindGameObjectWithTag("LoadScreen").GetComponent<LoadingScreen>().Play();
		Application.LoadLevel(LevelName[block.metadata]);
	}
}
