using UnityEngine;
using System.Collections;

public class HubChangeReciever : MonoBehaviour {
	void Start () {
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0, 1.5f, 0);
        GameObject message = GameObject.FindGameObjectWithTag("Message");
        if (message != null)
        {
            if (message.GetComponent<LevelchangerMessage>().type == TeleportType.Complete)
            {
                Stats stats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
                switch (message.GetComponent<LevelchangerMessage>().sender)
                {
                    case "Jump":
                        stats.JumpLevel++;
                        break;
                    case "Bounce":
                        stats.BounceLevel++;
                        break;
                    case "Ice":
                        stats.IceLevel++;
                        break;
                    case "Puzzle":
                        stats.PuzzleLevel++;
                        break;
                }
            }
        }
        DestroyImmediate(message);
        GameObject level = GameObject.FindGameObjectWithTag("Level");
        Level leveldata = level.GetComponent<Level>();
        leveldata.StartLoading();
	}
}
