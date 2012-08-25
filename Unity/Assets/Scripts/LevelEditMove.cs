using UnityEngine;
using System.Collections;

public class LevelEditMove : MonoBehaviour {	
	
	void Update()
	{
		if(GlobalSettings.LevelDev)
		{
			gameObject.SendMessage("DecDev");
			if(Input.GetKey(KeyCode.Space))
				gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + Time.deltaTime * 1.5f, gameObject.transform.position.z);
			if(Input.GetKey(KeyCode.LeftShift))
				gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - Time.deltaTime * 1.5f, gameObject.transform.position.z);
			
		}
	}
}
