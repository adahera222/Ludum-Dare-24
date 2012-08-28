using UnityEngine;
using System.Collections;

public class HubChangeReciever : MonoBehaviour {
	void Start () {
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0, 1.5f, 0);
        GameObject message = GameObject.FindGameObjectWithTag("Message");
        if (message != null)
        {
            Debug.Log("Recieved message");            
            if (message.GetComponent<LevelchangerMessage>().type == TeleportType.Complete)
            {
                Debug.Log(message.GetComponent<LevelchangerMessage>().sender);
                Stats stats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
                switch (message.GetComponent<LevelchangerMessage>().sender)
                {
                    case "Mission":
                        stats.MissionLevel++;
                        break;
                }
            }
            DestroyImmediate(message);
        }
        GameObject level = GameObject.FindGameObjectWithTag("Level");
        Level leveldata = level.GetComponent<Level>();
        leveldata.levelname = "Hub";
        leveldata.StartLoading();
	}
}
