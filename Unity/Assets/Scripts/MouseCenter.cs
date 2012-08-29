using UnityEngine;
using System.Collections;

public class MouseCenter : MonoBehaviour {
	bool pause = false;
    public bool mute = false;
	Texture crosshair;
	public GUIStyle style;
	void Awake()
	{
		crosshair = (Resources.Load("Crosshair") as Texture);
	}
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.M))
        {
            mute = !mute;
            if (mute)
                AudioListener.volume = 0;
            else
                AudioListener.volume = 1;
        }
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			pause = !pause;
			GlobalSettings.pause = pause;
		}
		if(!pause)
		{
			Screen.lockCursor = true;
			Screen.lockCursor = false;
			Screen.showCursor = false;
			Time.timeScale = 1;
		}
		else
		{
			Screen.showCursor= true;
			Time.timeScale = 0;
		}
	}
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(Screen.width / 2 - 7, Screen.height / 2 - 7, 15, 15),crosshair);
		if(pause)
		{			
			Vector2 size = style.CalcSize(new GUIContent("PAUSED"));
            if (GUI.Button(new Rect(Screen.width - 125, 0, 120, 25), "SAVE"))
                Save();
            if (GUI.Button(new Rect(Screen.width - 125, 30, 120, 25), "DELETE SAVE"))
            {
                PlayerPrefs.DeleteAll();
                GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>().MissionLevel = 1;
                //GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>().Reset();
            }
			GUI.Label(new Rect(Screen.width / 2 - size.x / 2, Screen.height / 2 - size.y / 2, size.x, size.y), "PAUSED", style);
            GUI.Label(new Rect(0, Screen.height - 20, 300, 20), "Press M to mute/unmute");
		}
	}
    void OnApplicationQuit()
    {
        Save();
    }
    void Save()
    {
        Stats stats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        if (stats != null)
        {
            PlayerPrefs.SetInt("Mission", stats.MissionLevel);
            //PlayerPrefs.SetFloat("maxspeedmodifier", stats.maxspeedmodifier);
            //PlayerPrefs.SetFloat("accelerationmodifier", stats.accelerationmodifier);
            //PlayerPrefs.SetFloat("JumpHeight", stats.JumpHeight);
            //PlayerPrefs.SetFloat("JumpDistance", stats.JumpDistance);
        }
    }
}
