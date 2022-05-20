using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spindash : MonoBehaviour {
    private Laurie laurie;
    private LaurieAbilities playerAbilities;
    private Rigidbody2D rb;
    private PlayerController controller;

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

    private void Awake() {
        playerAbilities = GetComponent<PlayerAbilities>();
        laurie = GetComponentInParent<Laurie>();
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    
        // time = laurie.spindashDist * 0.1f;
    }

    private void Update() {
        if (player.auxilaryType == AuxilaryMovementType.Spindash && player.ability == AbilityState.AuxilaryMovement) {
            if (!spinDashParActive) {
                OnSpinDashStart?.Invoke(this, new OnSpinDashStartEventArgs { });

                spinDashParActive = true;
            }
            float range = laurie.spindashDist;
            dashTarget = player.transform.position + (Vector3)controller.reconstructedMovement * range;

            time -= Time.deltaTime;

            float step =  speed * Time.deltaTime; // calculate distance to move
            player.transform.position = Vector3.MoveTowards(player.transform.position, dashTarget, step);

            // reset all timers and player ability state
            if (time <= 0f) {
                player.ability = AbilityState.None;
                player.movementType = MovementState.Idle;
             
                playerAbilities.abilitiesAvailable = false;
                playerAbilities.abilityCooldown = laurie.abilityCooldownLimit;
                time = laurie.spindashDist * 0.1f;

                OnSpinDashEnd?.Invoke(this, new OnSpinDashEndEventArgs { });
                spinDashParActive = false;
            }
        }
    }

    
}
