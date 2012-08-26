using UnityEngine;
using System.Collections;

public class CharCollisionHandler : MonoBehaviour {
	string state="";
	Vector3 LaunchNormals;
	int Launchpower;
	void OnControllerColliderHit(ControllerColliderHit hit) {
		if(!GlobalSettings.LevelDev)
		{
			if(hit.collider.gameObject.GetComponent<BlockData>().id == 1)
				audio.PlayOneShot(Resources.Load("Sounds/Portalfixed", typeof(AudioClip)) as AudioClip);
			Vector3 pos = hit.collider.transform.position;
			Blockmechanics.CharacterCollision(pos, hit.collider.gameObject.GetComponent<BlockData>().id);
			if(hit.collider.gameObject.GetComponent<BlockData>().id == 3)
			{			
				Debug.Log("LAUNCH");
				state = "Launch";
				audio.PlayOneShot(Resources.Load("Sounds/boing") as AudioClip);
				LaunchNormals = hit.normal;
				Launchpower = hit.collider.gameObject.GetComponent<BlockData>().metadata;
			}
			if(hit.collider.gameObject.GetComponent<BlockData>().id == 4)
			{
				gameObject.GetComponent<Death>().Die();
				audio.Play();
			}
			if(hit.collider.gameObject.GetComponent<BlockData>().id == 5)
			{
				gameObject.GetComponent<CharacterMotor>().iceSliding = true;
			}
			else
			{
				gameObject.GetComponent<CharacterMotor>().iceSliding = false;
			}
		}
	}
	void Update()
	{
		if(state == "Launch")
			Launch();
		if(state == "Land")
			Land();
	}
	float cnt = 0;
	void Launch()
	{
		CharacterController charctrl = gameObject.GetComponent<CharacterController>();
		float movefactor = Time.deltaTime * Launchpower * 6f;
		charctrl.Move(LaunchNormals * movefactor);
		charctrl.gameObject.GetComponent<CharacterMotor>().movement.gravity = 0;
		cnt += movefactor;
		if(cnt >= Launchpower)
		{
		charctrl.gameObject.GetComponent<CharacterMotor>().movement.gravity = 10;
		cnt = 0;
		state="Land";
		}
	}
	void Land()
	{
		CharacterController charctrl = gameObject.GetComponent<CharacterController>();
		if(gameObject.GetComponent<CharacterMotor>().grounded)
		{
			state="";
			charctrl.gameObject.GetComponent<CharacterMotor>().movement.gravity = 20;
		}
	}
}
