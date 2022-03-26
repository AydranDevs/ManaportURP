using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    [Header("Movement")]
    public MovementState movementType;
    public DirectionState direction;
    public FacingState facing;
    public Vector2 move;
    public float speed;
    public float sprintModifier;
    public float skidThreshold;
    public bool willSkid;
    public Vector2 reconstructedMovement;
    public float angle;
    public float runDuration;

    [Header("Ability")]
    public AbilityState ability; 
    public AuxilaryMovementType auxilaryType;
    public float abilityCooldownLimit;
    public float abilityCooldown;
    public bool abilitiesAvailable;
}
