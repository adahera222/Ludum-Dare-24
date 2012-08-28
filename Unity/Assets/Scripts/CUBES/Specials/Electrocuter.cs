using UnityEngine;
using System.Collections;

public class Electrocuter : MonoBehaviour {
    public Texture2D Active;
    public Texture2D InActive;
    public void Start()
    {
        gameObject.GetComponent<Electricity>().ON = true;
        ElectricityToggle();
    }
    public void Tick(BLOCK block)
    {
        if (gameObject.GetComponent<Electricity>().ISON)
        {
            Debug.Log("Electrocuter electricity on");
            gameObject.renderer.material.SetTexture("_MainTex", Active);
            gameObject.GetComponent<BlockData>().metadata = 0;
            block.metadata = 0;
        }
        else
        {
            Debug.Log("Electrocuter electricity off");
            gameObject.renderer.material.SetTexture("_MainTex", InActive);
            gameObject.GetComponent<BlockData>().metadata = 1;
            block.metadata = 0;
        }
    }
    void ElectricityToggle()
    {
        gameObject.GetComponent<Blockmechanics>().Tick();
        Debug.Log("Electrocuter electricity change");
    }
}
