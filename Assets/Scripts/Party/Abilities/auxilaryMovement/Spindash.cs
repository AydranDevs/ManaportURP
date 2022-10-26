using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manapotion.PartySystem.LaurieCharacter
{
    public class Spindash : MonoBehaviour
    {
        private Laurie laurie;
        private LaurieAbilities laurieAbilities;
        private Rigidbody2D rb;
        private LaurieController controller;

        private bool spinDashParActive = false;

        public Action OnSpinDashStart;
        public Action OnSpinDashEnd;

        // public float range; // default 5
        public float speed; // default 5
        public float time;

        [SerializeField]
        public Vector3 dashTarget;

        private void Start()
        {
            laurie = GetComponentInParent<Laurie>();
            controller = laurie.laurieController;
            laurieAbilities = laurie.laurieAbilities;
            rb = GetComponentInParent<Rigidbody2D>();
        
            // time = laurie.spindashDist * 0.1f;
        }

        private void Update()
        {
            if (laurie.auxilaryMovementType == AuxilaryMovementType.Spindash && laurie.abilityState == AbilityState.AuxilaryMovement)
            {
                if (!spinDashParActive)
                {
                    if (OnSpinDashStart != null) 
                    {
                        OnSpinDashStart();
                    }

                    spinDashParActive = true;
                }
                float range = laurie.stats.manaport_stat_ability_distance.GetValue();
                dashTarget = laurie.transform.position + (Vector3)controller.reconstructedMovement * range;

                time -= Time.deltaTime;

                float step =  speed * Time.deltaTime; // calculate distance to move
                laurie.transform.position = Vector3.MoveTowards(laurie.transform.position, dashTarget, step);

                // reset all timers and player ability state
                if (time <= 0f)
                {
                    laurie.abilityState = AbilityState.None;
                    laurie.movementState = MovementState.Idle;
                
                    laurieAbilities.abilitiesAvailable = false;
                    laurieAbilities.abilityCooldown = laurie.stats.manaport_stat_ability_cooldown.GetValue();
                    time = laurie.stats.manaport_stat_ability_distance.GetValue() * 0.1f;

                    if (OnSpinDashEnd != null)
                    {
                        OnSpinDashEnd();
                    }
                    spinDashParActive = false;
                }
            }
        }
    }
}
