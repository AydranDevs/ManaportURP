using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementState { Idle, Push, Walk, Sprint, Dash, Skid, AuxilaryMovement }
public enum DirectionState { North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest }
public enum FacingState { North, East, South, West }
public enum AbilityState { None, AuxilaryMovement, SpellcastPrimary, SpellcastSecondary }
public enum AuxilaryMovementType { Spindash, BlinkDash, Pounce }

public enum PrimarySpellType { Automa, Blasteur, Burston }
public enum PrimarySpellElement { Arcane, Pyro, Cryo, Toxi, Volt }
public enum SecondarySpellType { Automa, Blasteur, Burston }
public enum SecondarySpellElement { Arcane, Pyro, Cryo, Toxi, Volt }

public enum State { Movement, Attack, AuxMove }

// This class could hold all the values for player which would make for a nice singular place for everything
// to access them.  Sorta like the PlayerStateManager before

public class Player : MonoBehaviour {
    // Movement
    public MovementState movementType = MovementState.Idle; // sets "movementType" to None / Initializes MovementState
    public DirectionState direction = DirectionState.South; // sets "direction" to South / Initializes DirectionState
    public FacingState facing = FacingState.South; // sets "facing" to South / Initializes FacingState
    public Vector2 move = new Vector2(0, 0); // Used in playerMovement, determines the directin the player is moving
    public float sprintModifier = 1.75f; // Used in playerMovement, multiplies speed by value set here
    public float skidThreshold = 8f; // Used in playerMovement, how long the player must be sprinting for to make them skid to a stop and turn around upon stopping or pressing the opposite direction.
    public bool willSkid = false;
    public bool isDashing = false;
    public bool isPushing = false;

    // Ability
    public AbilityState ability = AbilityState.None; // sets "ability" to None / Initializes AbilityState
    public AuxilaryMovementType auxilaryType = AuxilaryMovementType.Spindash; //sets "auxilaryType" to Spindash / Initializes AuxilaryMovementType

    // Spellcasting
    public PrimarySpellType primary = PrimarySpellType.Blasteur;
    public PrimarySpellElement primaryElement = PrimarySpellElement.Arcane;
    
    public SecondarySpellType secondary = SecondarySpellType.Blasteur;
    public SecondarySpellElement secondaryElement = SecondarySpellElement.Arcane;

    public State state = State.Movement;


    public static Player instance;
    // public PartyMember partyMember;
    //public Laurie laurie;
    public PlayerCasting casting;
    
    public HealthBarScript healthBar;
    public ManaBarScript manaBar;

    public float manaRegenCooldown;
    public const float MANA_REGEN_COOLDOWN_DEFUALT = 1f;
    public bool manaRegenCoolingDown = false;
    
    private void Start() {
        instance = this;
        // laurie = GetComponent<Laurie>();
        casting = GetComponent<PlayerCasting>();
        
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBarScript>();
        manaBar = GameObject.FindGameObjectWithTag("ManaBar").GetComponent<ManaBarScript>();

        // healthBar.SetMaxHealth(laurie.hitPointsMax);
        // manaBar.SetMaxMana(laurie.manaPointsMax);
    }

    public void Damage(float damage) {
        // laurie.hitPoints = laurie.hitPoints - damage;
    }

    private void UpdateHealthBar() {
        // healthBar.SetHealth(laurie.hitPoints);
    }

    public void UseMana(float amount) {
        // Debug.Log("Mana used: " + amount);
        // laurie.manaPoints = laurie.manaPoints - amount;

        manaRegenCoolingDown = true;
        manaRegenCooldown = MANA_REGEN_COOLDOWN_DEFUALT;
    }

    // public float ManaPointsAfterUse(float amount) {
    //     // return laurie.manaPoints - amount;
    // }

    private void CoolDownManaRegen() {
        manaRegenCooldown = manaRegenCooldown - Time.deltaTime;
        if (manaRegenCooldown <= 0f) {
            manaRegenCoolingDown = false;
        }
    }

    private void Update() {
        UpdateHealthBar();
        UpdateManaBar();

        if (manaRegenCoolingDown) {
            CoolDownManaRegen();
        }
    }

    private void UpdateManaBar() {
        // manaBar.SetMana(laurie.manaPoints);
    }

    public void Debug_SetMaxHP(float hp) {
        // laurie.hitPointsMax = hp;
    }

    public void Debug_MaxHP() {
        // laurie.hitPoints = laurie.hitPointsMax;
    }

    public void Debug_SetMaxMP(float mp) {
        // laurie.manaPointsMax = mp;
        // healthBar.SetMaxHealth(laurie.hitPointsMax);
    }

    public void Debug_MaxMP() {
        // laurie.manaPoints = laurie.manaPointsMax;
        // manaBar.SetMaxMana(laurie.manaPointsMax);
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    public void Debug_SetPrimarySpell(int spell) {
        if (spell == 0) {
            primary = PrimarySpellType.Burston;
        }else if (spell == 1) {
            primary = PrimarySpellType.Blasteur;
        }else if (spell == 2) {
            primary = PrimarySpellType.Automa;
        }
    }

    public void Debug_SetSecondarySpell(int spell) {
        if (spell == 0) {
            secondary = SecondarySpellType.Burston;
        }else if (spell == 1) {
            secondary = SecondarySpellType.Blasteur;
        }else if (spell == 2) {
            secondary = SecondarySpellType.Automa;
        }
    }

    public void Debug_SetPrimaryElement(int element) {
        if (element == 0) {
            primaryElement = PrimarySpellElement.Arcane;
        }else if (element == 1) {
            primaryElement = PrimarySpellElement.Pyro;
        }else if (element == 2) {
            primaryElement = PrimarySpellElement.Cryo;
        }else if (element == 3) {
            primaryElement = PrimarySpellElement.Toxi;
        }else if (element == 4) {
            primaryElement = PrimarySpellElement.Volt;
        }
    }

    public void Debug_SetSecondaryElement(int element) {
        if (element == 0) {
            secondaryElement = SecondarySpellElement.Arcane;
        }else if (element == 1) {
            secondaryElement = SecondarySpellElement.Pyro;
        }else if (element == 2) {
            secondaryElement = SecondarySpellElement.Cryo;
        }else if (element == 3) {
            secondaryElement = SecondarySpellElement.Toxi;
        }else if (element == 4) {
            secondaryElement = SecondarySpellElement.Volt;
        }
    }
}
