using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelEditor : MonoBehaviour {

	void Update()
	{
		if(GlobalSettings.LevelDev)
		{
			if(Input.GetMouseButtonDown(0))
			{
				Debug.Log("Left Mouse");
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if(Physics.Raycast(ray, out hit, 10))
				{
					if(hit.collider.gameObject.tag == "CUBE")
					{
						Debug.Log("Hit CUBE");
						GameObject.FindGameObjectWithTag("Level").GetComponent<Level>().RemoveBlock(hit.transform.position, hit.collider.gameObject);					
					}
				}
			}
			if(Input.GetMouseButtonDown(1))
			{
				Debug.Log("Right Mouse");
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if(Physics.Raycast(ray, out hit, 10))
				{
					if(hit.collider.gameObject.tag == "CUBE")
					{
						Debug.Log("Hit CUBE");
						Vector3 newcube = hit.collider.transform.position + hit.normal;
						GameObject.FindGameObjectWithTag("Level").GetComponent<Level>().AddBlock(newcube);
					}
				}
			}
		}
	}
}
