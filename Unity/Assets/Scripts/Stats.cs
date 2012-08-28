using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {
    public int IceLevel = 1;
    public int BounceLevel = 1;
    public int JumpLevel = 1;
    public int PuzzleLevel = 1;
    public float maxspeedmodifier = 1;
    public float accelerationmodifier = 1;
    public float JumpHeight = 1;
    public float JumpDistance = 1;
    //public float LessCloak = 0;

    public void Set()
    {
        if (JumpDistance > 45)
        {
            JumpDistance = 45;
            JumpHeight += 0.1f;
        }
        CharacterMotor motor = gameObject.GetComponent<CharacterMotor>();
        motor.movement.maxGroundAcceleration *= accelerationmodifier;
        motor.movement.maxForwardSpeed *= maxspeedmodifier;
        motor.jumping.baseHeight *= JumpHeight;
        motor.jumping.perpAmount = JumpDistance;
    }
}
