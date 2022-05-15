using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilities : MonoBehaviour {

    private Player player;
    private Laurie laurie;
    private Controls controls;
    private Spindash spindash;
    private Lightspeed lightspeed;
    
    // public float abilityCooldownLimit = 10; // The default cooldown time after using an ability
    public float abilityCooldown; // Set to the CooldownLimit, default 10 seconds
    public bool abilitiesAvailable = false; // Set to true when the cooldown is over

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        laurie = GameObject.FindGameObjectWithTag("Player").GetComponent<Laurie>();
        controls = GameObject.FindGameObjectWithTag("Player").GetComponent<Controls>();
        spindash = GetComponent<Spindash>();
        lightspeed = GetComponent<Lightspeed>();

        abilityCooldown = laurie.abilityCooldownLimit; // Sets cooldown time to whatever CooldownLimit is set to
    }

    private void Update() {
        abilityCooldown = abilityCooldown - Time.deltaTime; // uses Time.deltaTime to make cooldown a consistent x seconds.

        if (abilityCooldown <= 0f) {
            abilitiesAvailable = true;  
        }else {
            abilitiesAvailable = false;
            player.state = State.Movement;
            player.ability = AbilityState.None;
        }
    }

    public void AuxMove() {
        if (abilitiesAvailable == true) {
            player.state = State.AuxMove;
            player.ability = AbilityState.AuxilaryMovement;
            player.movementType = MovementState.AuxilaryMovement;
        }
    }
}