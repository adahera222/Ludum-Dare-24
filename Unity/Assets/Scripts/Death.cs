using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {
	
	void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}
	public void Die () {
		gameObject.transform.position = new Vector3(0, 1.5f, 0);
		gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
	}

	void Update () {
		if(gameObject.transform.position.y < -25)
			Die();
	}
}
