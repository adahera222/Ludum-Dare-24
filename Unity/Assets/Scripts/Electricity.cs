using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Electricity : MonoBehaviour {
    private List<GameObject> connections;
    public bool ON = false;
    private bool hasElectrocuter = false;
    public bool ISON
    {
        get
        {
            return ON;
        }
    }
    private int lastpulse = -1;
    private static int nextpulse = 0;
    public static int GetPulseId()
    {
        int res = nextpulse;
        nextpulse++;
        return res;
    }  

    public void Toggle(int pulseid)
    {
        //Debug.Log("Toggle @ " + transform.position.ToString() + "::" + pulseid);
        if(pulseid != lastpulse)
        {
            lastpulse = pulseid;
            ON = !ON;
            if(gameObject.GetComponent<BlockData>().id == 4)
                SendMessage("ElectricityToggle");
            if(connections == null)
            {
                connections = new List<GameObject>();
                foreach (GameObject go in BLOCK.electrics)
                {
                    float dist = Vector3.Distance(gameObject.transform.position, go.transform.position);
                    if(dist >  0.5f && dist < 1.5f)
                    {
                        connections.Add(go);                        
                        go.GetComponent<Electricity>().Toggle(pulseid);
                    }
                }
            }
            else
            {
                foreach (GameObject block in connections)
                {
                    Electricity ele = block.GetComponent<Electricity>();
                    ele.Toggle(pulseid);
                }
            }
        }
    }
}
