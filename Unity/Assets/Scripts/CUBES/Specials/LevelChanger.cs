using UnityEngine;
using System.Collections;

public class LevelChanger : MonoBehaviour {
	public string[] LevelNames;
	public static string[] LevelName;
	
	void Awake()
	{
		LevelName = LevelNames;
	}

	public static void CollisionHandler(BLOCK block)
	{
		Application.LoadLevel(LevelName[block.metadata]);
	}
}
