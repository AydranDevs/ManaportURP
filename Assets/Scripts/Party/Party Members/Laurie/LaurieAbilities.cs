using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manapotion.PartySystem.LaurieCharacter
{
    public class LaurieAbilities
    {
        private Laurie _laurie;
        private Spindash _spindash;
    
        public float abilityCooldown; // Set to the CooldownLimit, default 10 seconds
        public bool abilitiesAvailable = false; // Set to true when the cooldown is over

        public LaurieAbilities(Laurie laurie)
        {
            _laurie = laurie;
            // spindash = GetComponent<Spindash>();

            abilityCooldown = _laurie.stats.abilityCooldownLimit.value; // Sets cooldown time to whatever CooldownLimit is set to
        }

        public void Update()
        {
            abilityCooldown = abilityCooldown - Time.deltaTime; // uses Time.deltaTime to make cooldown a consistent x seconds.

            if (abilityCooldown <= 0f) 
            {
                abilitiesAvailable = true;  
            }
            else
            {
                abilitiesAvailable = false;
                _laurie.state = State.Movement;
                _laurie.abilityState = AbilityState.None;
            }
        }

        public void AuxMove()
        {
            if (abilitiesAvailable == true) 
            {
                _laurie.state = State.AuxMove;
                _laurie.abilityState = AbilityState.AuxilaryMovement;
                _laurie.movementState = MovementState.AuxilaryMovement;
            }
        }
    }
 
}

