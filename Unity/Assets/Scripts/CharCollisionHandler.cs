using UnityEngine;
using System.Collections;

public class CharCollisionHandler : MonoBehaviour {
	string state="";
    string state2 = "";
	Vector3 LaunchNormals;
	int Launchpower;
    int Speedpower;
    Vector3 Initialspeed;
    float speedstart;
    bool justlanded;
    float lastswitch= -1;
	void OnControllerColliderHit(ControllerColliderHit hit) {
		if(!GlobalSettings.LevelDev)
		{
            if (justlanded)
            {
                justlanded = false;
                if (hit.collider.gameObject.GetComponent<BlockData>().id == 9)
                {
                    if (Time.time - lastswitch > 0.5f)
                    {
                        lastswitch = Time.time;
                        Debug.Log(BLOCK.electrics.Count);
                        hit.collider.gameObject.GetComponent<Switch>().Toggle();
                        Level level = GameObject.FindGameObjectWithTag("Level").GetComponent<Level>();
                        int pulseid = Electricity.GetPulseId();
                        foreach (GameObject go in BLOCK.electrics)
                        {
                            if (go != null)
                            {
                                float dist = Vector3.Distance(hit.transform.position, go.transform.position);
                                if (dist > 0.5f && dist < 1.5f)
                                {
                                    //Debug.Log(go.transform.position.ToString());
                                    // connections.Add(go);
                                    go.GetComponent<Electricity>().Toggle(pulseid);
                                }
                            }
                        }
                    }
                }
            }
            if (hit.collider.gameObject.GetComponent<BlockData>().id == 1)
            {
                audio.PlayOneShot(Resources.Load("Sounds/Portalfixed", typeof(AudioClip)) as AudioClip);
                Level level = GameObject.FindGameObjectWithTag("Level").GetComponent<Level>();
                BLOCK myblock = null;
                int reference = hit.collider.gameObject.GetComponent<BlockData>().reference;
                foreach (BLOCK block in level.GetBlocks())
                {
                    if (reference == block.reference)
                    {
                        myblock = block;
                    }
                }
                gameObject.transform.position = new Vector3(0, 1.5f, 0);
            }
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
			if(hit.collider.gameObject.GetComponent<BlockData>().id == 4 & hit.collider.gameObject.GetComponent<BlockData>().metadata == 0)
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
            if (hit.collider.gameObject.GetComponent<BlockData>().id == 7)
            {
                state2 = "Speed";
                speedstart = Time.time;
                Speedpower = hit.collider.gameObject.GetComponent<BlockData>().metadata / 5;
                CharacterController charctrl = gameObject.GetComponent<CharacterController>();
                Initialspeed = charctrl.GetComponent<CharacterMotor>().movement.velocity;
            }

		}
	}
	void FixedUpdate()
	{
		if(state == "Launch")
			Launch();
		if(state == "Land")
			Land();
        if (state2 == "Speed")
            Speed();
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
    void Speed()
    {
        CharacterController charctrl = gameObject.GetComponent<CharacterController>();
        float timepercentage = (Speedpower - (Time.time - speedstart)) / Speedpower;
        Vector3 movement = Initialspeed * Speedpower * timepercentage * Time.deltaTime;
        if(movement != Vector3.zero)
            charctrl.Move(movement);
        if (Time.time - speedstart >= Speedpower)
            state2 = "";
    }
    void OnLand()
    {
        justlanded = true;
    }
}
