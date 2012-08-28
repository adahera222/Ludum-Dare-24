using UnityEngine;
using System.Collections;
using System.Threading;

public class LevelChanger : MonoBehaviour {
	public string[] LevelNames;
	public static string[] LevelName;
    public Object MessageTemplate;
    public static Object Message;
	
	void Awake()
	{
		LevelName = LevelNames;
        Message = MessageTemplate;
	}
	
	public static void CollisionHandler(BLOCK block)
	{
		GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMotor>().movement.gravity = 0;
		GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0, 1.5f, 0);
		GameObject.FindGameObjectWithTag("LoadScreen").GetComponent<LoadingScreen>().Play();
        Stats stats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        int meta = block.metadata;
        GameObject CurMsg = (GameObject)Instantiate(Message);
        LevelchangerMessage CurMsgData = CurMsg.GetComponent<LevelchangerMessage>();
        CurMsg.GetComponent<LevelchangerMessage>();
        int type = meta % 10;
        CurMsgData.type = (TeleportType)type;
        string name = GameObject.FindGameObjectWithTag("Level").GetComponent<Level>().levelname;
        if (name == "Hub")
            CurMsgData.sender = "Hub";
        else
            CurMsgData.sender = "Mission";
        string levelname = LevelName[(meta / 10) - (meta / 100) * 10];
        switch (levelname)
        {
            case "Mission":
                CurMsgData.data = stats.MissionLevel;
                if (stats.MissionLevel > GlobalSettings.Missions)
                {
                    Debug.Log("COMPLETE");
                    Application.LoadLevel("Complete");
                    return;
                }
                break;
        }        
        //Debug.Log(((meta / 10) - (meta / 100) * 10));
        Debug.Log("Sending to " + levelname);
        Application.LoadLevel(levelname);
	}
}
