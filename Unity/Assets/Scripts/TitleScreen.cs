using UnityEngine;
using System.Collections;

public class TitleScreen: MonoBehaviour {
    public Texture2D background;
    public GUIStyle title;
    public GUIStyle button;
    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background);
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height / 4), "Evil tests", title);
        if(GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 35, 200, 30), "PLAY"))
        {
            Application.LoadLevel("Beginning");
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 5, 200, 30), "EXIT"))
        {
            Application.Quit();
        }
        GUI.Label(new Rect(0, Screen.width - 40, 150, 40), "Rao Zvorovski\n Ülari Taavel");
    }
}
