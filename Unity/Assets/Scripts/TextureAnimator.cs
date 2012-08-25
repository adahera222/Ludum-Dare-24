using UnityEngine;
using System.Collections;

public class TextureAnimator : MonoBehaviour {

	public Texture[] frames;
	public Texture[] normalmaps;
	public float delay;
	private bool usenormals;
	
	void Start()
	{
		if(normalmaps.Length > 0 && normalmaps.Length != frames.Length)
		{
			Debug.LogError("Not the same amount of frames and normalmaps");
			return;
		}
		if(normalmaps.Length > 0)
			usenormals = true;
		StartCoroutine("AnimationRoutine");
	}
	
	 IEnumerator AnimationRoutine() {
		int frame = 0;
		while(true)
		{
			frame++;
			if(frame >= frames.Length)
				frame = 0;
			gameObject.renderer.material.SetTexture("_MainTex", frames[frame]);
			gameObject.renderer.material.SetTexture("_BumpMap", normalmaps[frame]);
        	yield return new WaitForSeconds(delay);
		}
    }
}
