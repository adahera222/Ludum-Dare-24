using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {
    private bool ON = false;
    public Texture2D active;
    public Texture2D inactive;
    public void Toggle()
    {
        Debug.Log("Switch");
        ON = !ON;
        if (ON)
        {
            gameObject.renderer.material.SetTexture("_MainTex", active);
        }
        else
        {
            gameObject.renderer.material.SetTexture("_MainTex", inactive);
        }
    }
}
