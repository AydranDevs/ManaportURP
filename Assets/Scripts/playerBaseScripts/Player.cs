using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementState { Idle, Walk, Run, Dash, Skid, AuxilaryMovement }
public enum DirectionState { North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest }
public enum FacingState { North, East, South, West }
public enum AbilityState { None, AuxilaryMovement, SpellcastPrimary, SpellcastSecondary }
public enum AuxilaryMovementType { Spindash, BlinkDash, Pounce }

public enum PrimarySpellType { Automa, Blasteur, Burston }
public enum PrimarySpellElement { Arcane, Pyro, Cryo, Toxi, Bolt }
public enum SecondarySpellType { Automa, Blasteur, Burston }
public enum SecondarySpellElement { Arcane, Pyro, Cryo, Toxi, Bolt }

// This class could hold all the values for player which would make for a nice singular place for everything
// to access them.  Sorta like the PlayerStateManager before

public class Player : MonoBehaviour
{
    // Movement
    public MovementState movementType = MovementState.Idle; // sets "movementType" to None / Initializes MovementState
    public DirectionState direction = DirectionState.South; // sets "direction" to South / Initializes DirectionState
    public FacingState facing = FacingState.South; // sets "facing" to South / Initializes FacingState
    public Vector2 move = new Vector2(0, 0); // Used in playerMovement, determines the directin the player is moving
    // public float speed = 0.1f; // Used in playerMovement, Units/sec
    public float sprintModifier = 1.75f; // Used in playerMovement, multiplies speed by value set here
    public float skidThreshold = 8f; // Used in playerMovement, how long the player must be sprinting for to make them skid to a stop and turn around upon stopping or pressing the opposite direction.
    public bool willSkid = false;
    public bool isDashing = false;

    // Ability
    public AbilityState ability = AbilityState.None; // sets "ability" to None / Initializes AbilityState
    public AuxilaryMovementType auxilaryType = AuxilaryMovementType.Spindash; //sets "auxilaryType" to Spindash / Initializes AuxilaryMovementType

    // Controls

    // Spellcasting
    public PrimarySpellType primary = PrimarySpellType.Blasteur;
    public PrimarySpellElement primaryElement = PrimarySpellElement.Arcane;
    
    public SecondarySpellType secondary = SecondarySpellType.Blasteur;
    public SecondarySpellElement secondaryElement = SecondarySpellElement.Arcane;
    


    
    
}
