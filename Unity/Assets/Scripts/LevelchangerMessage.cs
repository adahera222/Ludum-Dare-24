using UnityEngine;
using System.Collections;
public enum TeleportType
{
	Start,
	Complete,
	Cancel
}

public class LevelchangerMessage : MonoBehaviour {
	public TeleportType type;
	public int data;
	public string sender;
    void Start()
    {
        DontDestroyOnLoad(this);
    }
}

