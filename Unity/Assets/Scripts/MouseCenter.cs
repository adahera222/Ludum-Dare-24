using UnityEngine;
using System.Collections;

public class MouseCenter : MonoBehaviour {
	bool pause = false;
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			pause = !pause;
		}
		if(!pause)
		{
			Screen.lockCursor = true;
			Screen.lockCursor = false;
			Time.timeScale = 1;
		}
		else
		{
			Time.timeScale = 0;
		}
	}
}
