using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleAnimation : MonoBehaviour {

    public float idleAnimTimer = 7f; // base timer for an idle animation to take place. Any input from the player will make this timer reset.

    private Player player;

    public event EventHandler<OnIdleWaveActiveEventArgs> OnIdleWaveActive;
    public class OnIdleWaveActiveEventArgs : EventArgs {}
    private bool idleWaveActive = false;
    private void Awake() {
        
        player = GetComponent<Player>();

    }

    private void Update() {
        if (player.movementType == MovementState.Idle) {

            if (player.facing == FacingState.South) {

                if (player.ability == AbilityState.None) {
                    if (!idleWaveActive) idleAnimTimer = idleAnimTimer - Time.deltaTime;

                    if (idleAnimTimer <= 0f) {
                        OnIdleWaveActive?.Invoke(this, new OnIdleWaveActiveEventArgs {});
                        idleWaveActive = true;
                        idleAnimTimer = 7f;
                    }

                }else {
                    idleAnimTimer = 7f;
                }
            }else {
                idleAnimTimer = 7f;
            }
        }else {
            idleAnimTimer = 7f;
        }
    }
    
}
