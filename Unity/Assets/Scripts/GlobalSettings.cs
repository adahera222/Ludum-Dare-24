using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalSettings : MonoBehaviour {
	public GameObject[] BlockIDs;
	public static bool pause;
	public static GameObject[] idtable;
	public bool IsLevelDev = false;
	public static bool LevelDev = false;
	// Use this for initialization
	void Awake () {
		LevelDev = IsLevelDev;
		idtable = BlockIDs;
		//DontDestroyOnLoad(gameObject);
	}
}
