using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {
    public int DeathCount = 0;
    public Texture2D deathscreen;
    public bool dead;
    public GUIStyle style;
	
	void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}
	public void Die () {
		gameObject.transform.position = new Vector3(0, 1.5f, 0);
		gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        DeathCount++;
        dead = true;
	}

	void Update () {
		if(gameObject.transform.position.y < -40)
			Die();
        if (dead == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                dead = false;
        }
	}
    void OnGUI()
    {
        if (dead)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), deathscreen);
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 20, 300, 40), "Press space bar to continue", style);
        }
    }
}
