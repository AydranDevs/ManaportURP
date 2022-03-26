using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegumelAnimation : MonoBehaviour {

    private GameStateManager gameStateManager; 
    private LegumelAI legumelAI;
    private LegumelAIStateHandler stateHandler;
    private Animator animator;

    private void Awake() {
        gameStateManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameStateManager>();
        legumelAI = GetComponent<LegumelAI>();
        stateHandler = GetComponent<LegumelAIStateHandler>();
        animator = GetComponentInChildren<Animator>(); 
    }

    void Update() 
    {
        if (gameStateManager.state == GameState.Main) { // check if the game isnt paused
            this.AnimateLegumel();
        }
    }

    public void AnimateLegumel() {
        if (legumelAI.move.sqrMagnitude > 0f) {
            animator.SetFloat("y", legumelAI.move.y);
            animator.SetFloat("x", legumelAI.move.x);
        }

        if (stateHandler.aIMovementState == AIMovementState.Idle) {
            animator.SetBool("idle", true);
        } else {
            animator.SetBool("idle", false);
        }

        if (stateHandler.aIMovementState == AIMovementState.Walk) {
            animator.SetBool("walking", true);
        } else {
            animator.SetBool("walking", false);
        }
    }
}
