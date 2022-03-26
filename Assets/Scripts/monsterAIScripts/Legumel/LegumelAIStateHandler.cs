using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This script handles AI state changing for Legumel. 
*/

public enum AIState { Roam, DetectTarget, Chase, Attack, TakeDamage, Forget, Daze, Die }
public enum AIMovementState { Idle, Walk }
public enum AIFacingState { None, North, East, South, West }
public enum AIStateDebuff { None, Bolt, Cryo, Pyro, Toxi }
public enum AIStateBuff { None, Bolt, Cryo, Pyro, Toxi }

public class LegumelAIStateHandler : MonoBehaviour {

    public AIState aIState = AIState.Roam; // default Roam state
    public AIMovementState aIMovementState = AIMovementState.Idle; // default idle
    public AIFacingState aIFacingState = AIFacingState.South;
    public AIStateDebuff aIStateDebuff = AIStateDebuff.None; // default no debuff
    public AIStateBuff aIStateBuff = AIStateBuff.None; // default no buff
    
}
