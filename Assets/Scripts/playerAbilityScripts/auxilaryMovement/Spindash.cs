using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PartyNamespace {
    namespace LaurieNamespace {
        public class Spindash : MonoBehaviour {
            private Laurie laurie;
            private LaurieAbilities laurieAbilities;
            private Rigidbody2D rb;
            private LaurieController controller;

            private bool spinDashParActive = false;

            public event EventHandler<OnSpinDashStartEventArgs> OnSpinDashStart;
            public class OnSpinDashStartEventArgs : EventArgs { }
            public event EventHandler<OnSpinDashEndEventArgs> OnSpinDashEnd;
            public class OnSpinDashEndEventArgs : EventArgs { }

            // public float range; // default 5
            public float speed; // default 5
            public float time;

            [SerializeField]
            public Vector3 dashTarget;

            private void Start() {
                laurieAbilities = GetComponent<LaurieAbilities>();
                laurie = GetComponentInParent<Laurie>();
                controller = laurie.controller;
                rb = GetComponentInParent<Rigidbody2D>();
            
                // time = laurie.spindashDist * 0.1f;
            }

            private void Update() {
                if (laurie.auxilaryMovementType == AuxilaryMovementType.Spindash && laurie.abilityState == AbilityState.AuxilaryMovement) {
                    if (!spinDashParActive) {
                        OnSpinDashStart?.Invoke(this, new OnSpinDashStartEventArgs { });

                        spinDashParActive = true;
                    }
                    float range = laurie.spindashDist;
                    dashTarget = laurie.transform.position + (Vector3)controller.reconstructedMovement * range;

                    time -= Time.deltaTime;

                    float step =  speed * Time.deltaTime; // calculate distance to move
                    laurie.transform.position = Vector3.MoveTowards(laurie.transform.position, dashTarget, step);

                    // reset all timers and player ability state
                    if (time <= 0f) {
                        laurie.abilityState = AbilityState.None;
                        laurie.movementState = MovementState.Idle;
                    
                        laurieAbilities.abilitiesAvailable = false;
                        laurieAbilities.abilityCooldown = laurie.abilityCooldownLimit;
                        time = laurie.spindashDist * 0.1f;

                        OnSpinDashEnd?.Invoke(this, new OnSpinDashEndEventArgs { });
                        spinDashParActive = false;
                    }
                }
            }
        }
    }
}
