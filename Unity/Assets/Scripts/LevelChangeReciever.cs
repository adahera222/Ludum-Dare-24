using UnityEngine;
using System.Collections;

public class LevelChangeReciever : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0, 1.5f, 0);
        GameObject message = GameObject.FindGameObjectWithTag("Message");
        if(message != null)
        {
            Debug.Log("Message recieved");
            GameObject level = GameObject.FindGameObjectWithTag("Level");
            Level leveldata = level.GetComponent<Level>();
            Debug.Log(leveldata.levelname);
            LevelchangerMessage messagedata = message.GetComponent<LevelchangerMessage>();
            leveldata.levelname += messagedata.data.ToString();
            Debug.Log(messagedata.data);
            Debug.Log(leveldata.levelname);
            leveldata.StartLoading();
            Destroy(message);
        }
	}
}