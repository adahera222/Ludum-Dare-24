using UnityEngine;
using System.Collections;

public class CharCollisionHandler : MonoBehaviour {
	string state="";
	void OnControllerColliderHit(ControllerColliderHit hit) {
		if(hit.collider.gameObject.GetComponent<BlockData>().id == 1)
			audio.PlayOneShot(Resources.Load("Sounds/Portalfixed", typeof(AudioClip)) as AudioClip);
		Vector3 pos = hit.collider.transform.position;
		Blockmechanics.CharacterCollision(pos, hit.collider.gameObject.GetComponent<BlockData>().id);
		if(hit.collider.gameObject.GetComponent<BlockData>().id == 3)
		{			
			Debug.Log("LAUNCH");
			state = "Launch";
			audio.PlayOneShot(Resources.Load("Sounds/boing") as AudioClip);
		}
		if(hit.collider.gameObject.GetComponent<BlockData>().id == 4)
		{
			gameObject.GetComponent<Death>().Die();
			audio.Play();
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
		float movefactor = Time.deltaTime * 50f;
		charctrl.Move(new Vector3(0, movefactor, 0));
		cnt += movefactor;
		if(cnt >= 8)
		{
		cnt = 0;
		state="Land";
		}
	}
	void Land()
	{
		CharacterController charctrl = gameObject.GetComponent<CharacterController>();
		float movefactor = Time.deltaTime * 5f;
		charctrl.Move(new Vector3(0, movefactor, 0));
		if(gameObject.GetComponent<CharacterMotor>().grounded)
		{
			state="";
		}
	}
}
