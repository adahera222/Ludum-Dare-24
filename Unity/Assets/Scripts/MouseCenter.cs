using UnityEngine;
using System.Collections;

public class MouseCenter : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if(!Input.GetKey(KeyCode.Escape))
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
