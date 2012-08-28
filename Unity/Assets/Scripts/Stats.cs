using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {
    public int MissionLevel = 1;
    public float maxspeedmodifier = 1;
    public float accelerationmodifier = 1;
    public float JumpHeight = 1;
    public float JumpDistance = 1;
    //public float LessCloak = 0;

    public void Set()
    {
        if (JumpDistance > 0.5f)
        {
            JumpDistance = 0.5f;
            JumpHeight += 0.1f;
        }
        CharacterMotor motor = gameObject.GetComponent<CharacterMotor>();
        motor.movement.maxGroundAcceleration = 10* accelerationmodifier;
        motor.movement.maxForwardSpeed = 6* maxspeedmodifier;
        motor.jumping.baseHeight = JumpHeight;
        motor.jumping.perpAmount = JumpDistance;
    }
    public void Reset()
    {
       maxspeedmodifier = 1;
        accelerationmodifier = 1;
        JumpHeight = 1;
        JumpDistance = 1;
        Set();
    }
}
