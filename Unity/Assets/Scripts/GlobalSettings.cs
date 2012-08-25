using UnityEngine;
using System.Collections;

public class GlobalSettings : MonoBehaviour {
	public bool IsLevelDev = false;
	public static bool LevelDev = false;
	// Use this for initialization
	void Start () {
		LevelDev = IsLevelDev;
		DontDestroyOnLoad(gameObject);
	}
}
