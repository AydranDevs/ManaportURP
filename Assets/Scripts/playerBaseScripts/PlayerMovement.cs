using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    private Player Player;
    private Laurie laurie;
    private GameStateManager gameStateManager;
    public VectorValue vectorValue;

    public bool canHandleMovement = false;

    [HideInInspector]
    public Vector2 reconstructedMovement;
    [HideInInspector]
    public Vector3 position;
    public float angle;
    
    public float movementSp;
    public float runDuration;
    
    public bool runDustParActive = false;

    public bool dashPoofParActive = false;
    public bool dashDustParActive = false;

    public int horizontal;
    public int vertical;

    public event EventHandler<OnRunStartEventArgs> OnRunStart;
    public class OnRunStartEventArgs : EventArgs {}
    public event EventHandler<OnRunEndEventArgs> OnRunEnd;
    public class OnRunEndEventArgs : EventArgs {}

    public event EventHandler<OnDashStartEventArgs> OnDashStart;
    public class OnDashStartEventArgs : EventArgs {}
    public event EventHandler<OnDashEndEventArgs> OnDashEnd;
    public class OnDashEndEventArgs : EventArgs {}

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        Player = GetComponent<Player>();
        laurie = GetComponent<Laurie>();
        Player.transform.position = vectorValue.initialValue;

        movementSp = laurie.movementSp;
    }  
    
    public void Move(float d) {

        if(Player.movementType == MovementState.Run){
            runDuration += Time.deltaTime;

            if (!runDustParActive) {
                OnRunStart?.Invoke(this, new OnRunStartEventArgs{});
                runDustParActive = true;
            }
        }else {
            runDuration = 0;
            runDustParActive = false;
            
            OnRunEnd?.Invoke(this, new OnRunEndEventArgs{});

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

        // if (runDuration == 0f && Player.movementType != MovementState.Idle | Player.movementType != MovementState.Walk) {
        //    Debug.Log("Check");
        // }

        if(Input.GetAxisRaw("Horizontal") > 0f) {
            horizontal = 1;
            Player.facing = FacingState.East;
        }else if(Input.GetAxisRaw("Horizontal") < 0f) {
            horizontal = -1;
            Player.facing = FacingState.West;
        }else {
            horizontal = 0;
        }
        if(Input.GetAxisRaw("Vertical") > 0f) {
            vertical = 1;
            Player.facing = FacingState.North;
        }else if(Input.GetAxisRaw("Vertical") < 0f) {
            vertical = -1;
            Player.facing = FacingState.South;
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

        position = Player.transform.position;
        position = PixelPerfectClamp(position, 16f);


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
        reconstructedMovement = PixelPerfectClamp(reconstructedMovement, 16f);

        rb.MovePosition(new Vector2(position.x, position.y) + ((reconstructedMovement * laurie.movementSp) * d));        
    }

    private void Update() {
        
    }

    private Vector2 PixelPerfectClamp(Vector2 moveVector, float pixelsPerUnit) {

        Vector2 vectorInPixels = new Vector2(
            Mathf.RoundToInt(moveVector.x * pixelsPerUnit),
            Mathf.RoundToInt(moveVector.y * pixelsPerUnit)
        );
        
        return vectorInPixels / pixelsPerUnit;
    }
}