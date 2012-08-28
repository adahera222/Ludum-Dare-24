using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalSettings : MonoBehaviour {
	public GameObject[] BlockIDs;
	public static bool pause;
	public static GameObject[] idtable;
	public bool IsLevelDev = false;
	public static bool LevelDev = false;
	public bool[] UsesMetadata;
	public static bool[] UsesMetaData;
    public int PuzzleLevels;
    public int BounceLevels;
    public int IceLevels;
    public int JumpLevels;
    public static int Puzzles;
    public static int Bounces;
    public static int Ices;
    public static int Jumps;
	// Use this for initialization
	void Awake () {
		LevelDev = IsLevelDev;
		idtable = BlockIDs;
		UsesMetaData = UsesMetadata;
        Puzzles = PuzzleLevels;
        Bounces = BounceLevels;
        Ices = IceLevels;
        Jumps = JumpLevels;
		//DontDestroyOnLoad(gameObject);
	}
}
