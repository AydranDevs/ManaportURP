using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    private Player Player;
    private Laurie laurie;
    private GameStateManager gameStateManager;
    public VectorValue vectorValue;


    [HideInInspector]
    public Vector2 reconstructedMovement;
    public float angle;
    
    public float movementSp;
    public float runDuration;
    public bool dashPoofParActive = false;
    public bool dashDustParActive = false;
    public int horizontal;
    public int vertical;

    public event EventHandler<OnDashStartEventArgs> OnDashStart;
    public class OnDashStartEventArgs : EventArgs {}

    public event EventHandler<OnDashEndEventArgs> OnDashEnd;
    public class OnDashEndEventArgs : EventArgs {}

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        Player = GetComponent<Player>();
        laurie = GetComponent<Laurie>();
        // ps = GameObject.FindGameObjectWithTag("DashdustPS").GetComponent<ParticleSystem>();
        Player.transform.position = vectorValue.initialValue;
    }

    public void Move(float d) {

        if(Player.movementType == MovementState.Run){
            runDuration += Time.deltaTime;
        }else {
            runDuration = 0;
            Player.willSkid = false;
            Player.isDashing = false;
        }

        if(runDuration >= Player.skidThreshold){
            Player.willSkid = true;
            Player.isDashing = true;
            // Player.sprintModifier = 2.5f;
            
            // make cool dash particles appear
            if (!dashPoofParActive && !dashDustParActive) {
                OnDashStart?.Invoke(this, new OnDashStartEventArgs {});
                dashPoofParActive = true;
                dashDustParActive = true;
            }
        }else {
            dashPoofParActive = false;
            dashDustParActive = false;

            OnDashEnd?.Invoke(this, new OnDashEndEventArgs {});
        }

        if(Input.GetAxis("Horizontal") > 0f) {
            horizontal = 1;
        }else if(Input.GetAxis("Horizontal") < 0f) {
            horizontal = -1;
        }else {
            horizontal = 0;
        }
        if(Input.GetAxis("Vertical") > 0f) {
            vertical = 1;
        }else if(Input.GetAxis("Vertical") < 0f) {
            vertical = -1;
        }else {
            vertical = 0;
        }


        Player.move = new Vector2(horizontal, vertical);

        if (!Player.move.Equals(new Vector2(0, 0))) {
            Player.movementType = Input.GetKey(KeyCode.LeftShift) ? MovementState.Run : MovementState.Walk;
        }else {
            Player.movementType = MovementState.Idle;
        }

        if (Player.move.Equals(new Vector2(0, 0))) return;

        Vector3 position = Player.transform.position;


        float xDiff = Player.move.x;
        float yDiff = Player.move.y;
        angle = (float)(Mathf.Atan2(yDiff, xDiff));

        if (Player.isDashing == true) {
            movementSp = laurie.movementSp * laurie.dashMod;
        }else if (Player.movementType == MovementState.Run) {
            movementSp = laurie.movementSp * laurie.sprintMod;
        }else {
            movementSp = laurie.movementSp;
        }

        reconstructedMovement = new Vector2(Mathf.Cos(angle) * movementSp, Mathf.Sin(angle) * movementSp);
        rb.MovePosition(new Vector2(position.x, position.y) + ((reconstructedMovement * laurie.movementSp) * d));        
    }
}