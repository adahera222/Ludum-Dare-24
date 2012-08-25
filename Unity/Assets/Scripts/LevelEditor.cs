using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelEditor : MonoBehaviour {
	public int selection = 0;
	const int min = 0;
	const int max = 3;
	void Update()
	{
		if(GlobalSettings.LevelDev)
		{
			if(Input.GetKeyDown(KeyCode.Q))
			{
				int metaint;
				bool sc = int.TryParse(metadata, out metaint);
				if(sc)
					metadata = (metaint - 1).ToString();
			}
			if(Input.GetKeyDown(KeyCode.E))
			{
				int metaint;
				bool sc = int.TryParse(metadata, out metaint);
				if(sc)
					metadata = (metaint + 1).ToString();
			}
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
						int meta = 0;
						int.TryParse(metadata, out meta);
						GameObject.FindGameObjectWithTag("Level").GetComponent<Level>().AddBlock(newcube, selection, meta);
					}
				}
			}
			//Debug.Log(Input.GetAxis("Mouse ScrollWheel").ToString());
			selection += (int)(Input.GetAxis("Mouse ScrollWheel") * 10);
				if(selection > max)
					selection = min;
				if(selection < min)
					selection = max;
		}
	}
	string metadata = "0";
	void OnGUI()
	{
		if(GlobalSettings.LevelDev)
		{
			GUI.Label(new Rect(0, Screen.height - 20, 200, 20), ((Block)(selection)).ToString());
			metadata = GUI.TextField(new Rect(Screen.width - 200, Screen.height - 20, 200, 20), metadata);
			int hold;
			if(int.TryParse(metadata, out hold))
				GUI.Label(new Rect(Screen.width - 200, Screen.height - 45, 200, 20), "CORRECT");
		}
	}
}
