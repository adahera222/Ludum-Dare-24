using UnityEngine;
using System.Collections;

public class Evolution : MonoBehaviour {
    public string Text;
    public GUIStyle style;
    public Texture2D background;
    public float time;
    //public GUIStyle buttonstyle;
    BLOCK block;
    public void Evolve(BLOCK _block)
    {
        //Debug.Log("Evolvin");
        block = _block;
        GameObject player = gameObject;
        int deathcount = player.GetComponent<Death>().DeathCount;
        int rand = Random.Range(0, 100 + deathcount);
        Stats stats = player.GetComponent<Stats>();
        time = Time.time;
        switch(rand)
        {
            case 0:
                stats.maxspeedmodifier += 0.1f;
                Text = "YOU HAVE EVOLVED!\n\nYou now run faster.";
                break;
            case 1:
                stats.maxspeedmodifier += 0.15f;
                Text = "YOU HAVE EVOLVED!\n\nYou now run faster.";
                break;
            case 2:
                stats.maxspeedmodifier += 0.05f;
                Text = "YOU HAVE EVOLVED!\n\nYou now run faster.";
                break;
            case 3:
                stats.JumpHeight += 0.1f;
                Text = "YOU HAVE EVOLVED!\n\nYou now jump higher.";
                break;
            case 4:
                stats.JumpHeight += 0.2f;
                Text = "YOU HAVE EVOLVED!\n\nYou now jump higher.";
                break;
            case 5:
                stats.JumpHeight += 0.3f;
                Text = "YOU HAVE EVOLVED!\n\nYou now jump higher.";
                break;
            case 6:
                stats.JumpDistance += 0.05f;
                Text = "YOU HAVE EVOLVED!\n\nYou now jump further away.";
                break;
            case 7:
                stats.JumpDistance += 2f;
                Text = "YOU HAVE EVOLVED!\n\nYou now jump further away.";
                break;
            case 8:
                stats.JumpDistance += 5f;
                Text = "YOU HAVE EVOLVED!\n\nYou now jump further away.";
                break;
            case 9:
                stats.maxspeedmodifier += 0.1f;
                stats.JumpHeight += 0.2f;
                Text = "YOU HAVE EVOLVED!\n\nYou now run faster and jump higher.";
                break;
            case 10:
                stats.JumpDistance += 5f;
                stats.JumpHeight += 0.3f;
                stats.maxspeedmodifier += 0.05f;
                Text = "YOU HAVE EVOLVED!\n\nYou now run faster and\n jump higher and further away.";
                break;
            default:
                
                break;
        }
        stats.Set();
        LevelChanger.CollisionHandler(block);
    }
    //void OnGUI()
    //{
    //    if (Text != "")
    //    {
    //        Debug.Log(Text);
    //        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background);
    //        Vector2 size = style.CalcSize(new GUIContent(Text));
    //        GUI.Label(new Rect(Screen.width / 2 - size.x / 2, Screen.height / 2 - size.y/2, size.x, size.y), Text, style);
    //        if (Time.time - time > 5)
    //        {
    //            Text = "";
    //            LevelChanger.CollisionHandler(block);
    //        }

    //    }
    //}
}
