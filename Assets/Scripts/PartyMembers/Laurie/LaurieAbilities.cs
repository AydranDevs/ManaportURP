using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaurieNamespace {
    public class LaurieAbilities : MonoBehaviour {
        private Laurie laurie;
        private Spindash spindash;
        private Lightspeed lightspeed;
    
        // public float abilityCooldownLimit = 10; // The default cooldown time after using an ability
        public float abilityCooldown; // Set to the CooldownLimit, default 10 seconds
        public bool abilitiesAvailable = false; // Set to true when the cooldown is over

        void Start() {
            laurie = GetComponentInParent<Laurie>();
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
                laurie.state = State.Movement;
                laurie.abilityState = AbilityState.None;
            }
        }

        public void AuxMove() {
        if (abilitiesAvailable == true) {
            laurie.state = State.AuxMove;
            laurie.abilityState = AbilityState.AuxilaryMovement;
            laurie.movementState = MovementState.AuxilaryMovement;
        }
    }
    }
}

