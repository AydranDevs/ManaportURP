using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private GameStateManager gameStateManager;
    private Animator animator;
    private PlayerIdleAnimation idleAnimation;
    private Player Player;

    void Start() {
        gameStateManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameStateManager>();
        Player = GetComponent<Player>();
        animator = GameObject.FindGameObjectWithTag("PlayerAnimator").GetComponent<Animator>();     
        idleAnimation = GetComponent<PlayerIdleAnimation>();

        idleAnimation.OnIdleWaveActive += AnimatePlayer_OnIdleWaveActive;
    }

    void Update() {
        if (gameStateManager.state == GameState.Main) {
            AnimatePlayer();
        }
    }

    public void AnimatePlayer() {
        if (Player.move.sqrMagnitude > 0f) {
            animator.SetFloat("y", Player.move.y);
            animator.SetFloat("x", Player.move.x);
        }

        if (Player.movementType == MovementState.Idle) {
            animator.SetBool("idle", true);
        }else {
            animator.SetBool("idle", false);
        }

        if (Player.movementType == MovementState.Walk) {
            animator.SetBool("walking", true);
        }else {
            animator.SetBool("walking", false);
        }

        if (Player.movementType == MovementState.Run) {
            animator.SetBool("running", true);
        }else {
            animator.SetBool("running", false);
        }

        if (Player.isDashing == true) {
            animator.SetBool("dashing", true);
        }else {
            animator.SetBool("dashing", false);
        }

        if (Player.movementType == MovementState.Skid) {
            animator.SetBool("skidding", true);
        }else {
            animator.SetBool("skidding", false);
        }
        
        if (Player.auxilaryType == AuxilaryMovementType.Spindash && Player.ability == AbilityState.AuxilaryMovement) {
            animator.SetBool("spindashing", true);
        }else {
            animator.SetBool("spindashing", false);
        }

        if (Player.auxilaryType == AuxilaryMovementType.BlinkDash && Player.ability == AbilityState.AuxilaryMovement) {
            animator.SetBool("blinkdashing", true);
        }else {
            animator.SetBool("blinkdashing", false);
        }
    }

    private void AnimatePlayer_OnIdleWaveActive(object sender, PlayerIdleAnimation.OnIdleWaveActiveEventArgs e) {
        animator.SetTrigger("idleWave");

        Debug.Log("check");
    }
}
