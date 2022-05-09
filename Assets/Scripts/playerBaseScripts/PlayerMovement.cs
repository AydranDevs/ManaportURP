using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    private const float WALK_PUSH_THRESHOLD = .07f;
    private const float RUN_PUSH_THRESHOLD = .13f;
    private const float DASH_PUSH_THRESHOLD = .2f;

    private float pushThreshold = WALK_PUSH_THRESHOLD;

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
    public float initialPushAngle;

    private Vector2 initialPosition;
    private Vector2 targetPosition;
    private Vector2 resultPosition;

    [SerializeField]
    private Vector2 targetDelta;
    [SerializeField]
    private Vector2 actualDelta;
    
    public float pushSp;
    public float movementSp;
    public float sprintMod;
    public float dashMod;

    public float runDuration;
    
    public bool runDustParActive = false;
    public bool dashPoofParActive = false;
    public bool dashDustParActive = false;
    public bool pushSweatParActive = false;

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

    public event EventHandler<OnPushStartEventArgs> OnPushStart;
    public class OnPushStartEventArgs : EventArgs {}
    public event EventHandler<OnPushEndEventArgs> OnPushEnd;
    public class OnPushEndEventArgs : EventArgs {}

    float time = 0.03f;
    Vector2 inputVector;
    bool shiftHeld = false;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        Player = GetComponent<Player>();
        laurie = GetComponent<Laurie>();

        gameStateManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameStateManager>();
        Player.transform.position = vectorValue.initialValue;

        
    }

    void Update() {
        if (gameStateManager.state == GameState.Main) {
            if (Player.ability != AbilityState.AuxilaryMovement) {
                // Move(Time.fixedDeltaTime);
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context) {
        inputVector = context.ReadValue<Vector2>();
    }

    public void ShiftHeld(InputAction.CallbackContext context) {
        if (!context.canceled) {
            shiftHeld = true;
        }else {
            shiftHeld = false;
        }
    }
    
    // public void Move(float d) {
    //     pushSp = laurie.pushSp;
    //     movementSp = laurie.movementSp;
    //     sprintMod = laurie.sprintMod;
    //     dashMod = laurie.dashMod;
        
    //     if(Player.movementType == MovementState.Run) {
    //         runDuration += Time.deltaTime;

    //         if (!runDustParActive && !Player.isPushing) {
    //             OnRunStart?.Invoke(this, new OnRunStartEventArgs{});
    //             runDustParActive = true;
    //         }
    //     }else {
    //         runDuration = 0;
    //         runDustParActive = false;
            
    //         OnRunEnd?.Invoke(this, new OnRunEndEventArgs{});

    //         Player.willSkid = false;
    //         Player.isDashing = false;
    //     }

    //     if(runDuration >= Player.skidThreshold){
    //         Player.willSkid = true;
    //         Player.isDashing = true;
         
    //         // make cool dash particles appear
    //         if (!dashPoofParActive && !dashDustParActive && !Player.isPushing) {
    //             OnDashStart?.Invoke(this, new OnDashStartEventArgs {});
    //             dashPoofParActive = true;
    //             dashDustParActive = true;
    //         }
    //     }else {
    //         dashPoofParActive = false;
    //         dashDustParActive = false;

    //         OnDashEnd?.Invoke(this, new OnDashEndEventArgs {});
    //     }

    //     Player.move = inputVector;

    //     if (!Player.move.Equals(new Vector2(0, 0))) {
    //         if (shiftHeld) {
    //             Player.movementType = MovementState.Run;
    //         }else {
    //             Player.movementType = MovementState.Walk;
    //         }
    //     }else {
    //         Player.movementType = MovementState.Idle;
    //     }

    //     if (Player.move.Equals(new Vector2(0, 0))) return;

    //     position = Player.transform.position;
    //     position = PixelPerfectClamp(position, 16f);

    //     float xDiff = Player.move.x;
    //     float yDiff = Player.move.y;
    //     angle = (float)(Mathf.Atan2(yDiff, xDiff));

    //     if (Player.isDashing == true) { 
    //         movementSp = movementSp * dashMod;
    //     }else if (Player.movementType == MovementState.Run) {
    //         movementSp = movementSp * sprintMod;
    //     }else {
    //         movementSp = movementSp;
    //     }

    //     if (Player.isPushing == true) {
    //         movementSp = pushSp;
    //     }

    //     reconstructedMovement = new Vector2(Mathf.Cos(angle) * movementSp, Mathf.Sin(angle) * movementSp);
    //     reconstructedMovement = PixelPerfectClamp(reconstructedMovement, 16f);
        
    //     rb.MovePosition(new Vector2(position.x, position.y) + ((reconstructedMovement * laurie.movementSp) * d));
    //     resultPosition = Player.transform.position;

    //     targetPosition = new Vector2(position.x, position.y) + ((reconstructedMovement * laurie.movementSp) * d);

    //     targetDelta = targetPosition - initialPosition;
    //     actualDelta = resultPosition - initialPosition;

    //     initialPosition = Player.transform.position;

    // }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag != "Pushable") return;

        initialPushAngle = angle;
        Player.isPushing = true;
    }

    private void OnCollisionStay2D(Collision2D col) {
        if (col.gameObject.tag != "Pushable") return;
        if (angle == initialPushAngle) return;
        
        Player.isPushing = false;
    }

    private void OnCollisionExit2D(Collision2D col) {
        Player.isPushing = false;
    }

    private Vector2 PixelPerfectClamp(Vector2 moveVector, float pixelsPerUnit) {

        Vector2 vectorInPixels = new Vector2(
            Mathf.RoundToInt(moveVector.x * pixelsPerUnit),
            Mathf.RoundToInt(moveVector.y * pixelsPerUnit)
        );
        
        return vectorInPixels / pixelsPerUnit;
    }
}