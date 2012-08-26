using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour {
	public Texture[] frames;
	public float delay;
	public Texture currentframe;
	bool on = false;
	void Start()
	{
		DontDestroyOnLoad(gameObject);
	}
	public void Play()
	{
		if(on)
			return;
		on = true;
		//audio.Play();
		StartCoroutine("AnimationRoutine");
	}
	public void Stop()
	{
		if(!on)
			return;
		on = false;
		//audio.Stop();
		StopCoroutine("AnimationRoutine");
	}
	IEnumerator AnimationRoutine() {
		int frame = 0;
		while(true)
		{
			frame++;
			if(frame >= frames.Length)
				frame = 0;
			currentframe = frames[frame];
        	yield return new WaitForSeconds(delay);
		}
    }
	void OnGUI()
	{
		if(on)
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), currentframe);
	}
}
