using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roam : MonoBehaviour {
    
    private Rigidbody2D rb;
    private Legumel legumel;
    private LegumelAI legumelAI;

    private float roamDist;

    private Vector3 targetPos;

    private int horizontal;
    private int vertical;

    private bool awaitingDir = false;

    private bool isMoving = false;
    float speed = 3f;
    [SerializeField]
    float time = 2f;

    private void Awake() {
        legumel = GetComponent<Legumel>();
        legumelAI = GetComponent<LegumelAI>();
        legumelAI.OnRoamWaitOver += LegumelRoamMove_OnRoamWaitOver;
    }
    
    private void LegumelRoamMove_OnRoamWaitOver(object sender, LegumelAI.OnRoamWaitOverArgs e) {
        roamDist = e.dist;
        isMoving = true;
        awaitingDir = true;
    }

    private void Update() {
        
        if (!isMoving) {
            legumelAI.stateHandler.aIMovementState = AIMovementState.Idle;
            legumelAI.move = new Vector2(0, 0);

            return;
        }
        
        if (isMoving) {
            if (awaitingDir) {
                int rand = UnityEngine.Random.Range(1, 9);
                targetPos = GetRandomDir(rand);
                
                legumelAI.stateHandler.aIMovementState = AIMovementState.Walk;
                
                awaitingDir = false;
            }
                
            
            legumelAI.move = new Vector2(horizontal, vertical);

            // determines what direction the monster is facing
            if (legumelAI.move.y == 1) legumelAI.stateHandler.aIFacingState = AIFacingState.North;
            if (legumelAI.move.y == -1) legumelAI.stateHandler.aIFacingState = AIFacingState.South;
            if (legumelAI.move.x == 1) legumelAI.stateHandler.aIFacingState = AIFacingState.East;
            if (legumelAI.move.x == -1) legumelAI.stateHandler.aIFacingState = AIFacingState.West;
            

            // Debug.Log(targetPos);

            time = time - Time.deltaTime;

            float step =  speed * Time.deltaTime; // calculate distance to move
            legumel.transform.position = Vector3.MoveTowards(legumel.transform.position, targetPos, step);

            if (time <= 0f) {
                // time over
                time = 2f;
                awaitingDir = false;
                isMoving = false;
            }
        }

        // Debug.Log(legumelAI.stateHandler.aIMovementState);
    }

    // this function gets a random position of the 8.
    private Vector3 GetRandomDir(int num) { 
        
        Vector2 right = new Vector2(roamDist, 0); // right
        Vector2 downRight = new Vector2(roamDist, -roamDist); // down-right
        Vector2 down = new Vector2(0, -roamDist); // down
        Vector2 downLeft = new Vector2(-roamDist, -roamDist); // down-left
        Vector2 left = new Vector2(-roamDist, 0); // left
        Vector2 upLeft = new Vector2(-roamDist, roamDist); // up-left 
        Vector2 up = new Vector2(0, roamDist); // up
        Vector2 upRight = new Vector2(roamDist, roamDist); // up-right

        // int rand = UnityEngine.Random.Range(1, 9);

        if (num == 1) {
            horizontal = 1;
            vertical = 0;
            return transform.position + (Vector3)right;
        } else if (num == 2) {
            horizontal = 1;
            vertical = -1;
            return transform.position + (Vector3)downRight;
        } else if (num == 3) {
            horizontal = 0;
            vertical = -1;
            return transform.position + (Vector3)down;
        } else if (num == 4) {
            horizontal = -1;
            vertical = -1;
            return transform.position + (Vector3)downLeft;
        } else if (num == 5) {
            horizontal = -1;
            vertical = 0;
            return transform.position + (Vector3)left;
        } else if (num == 6) {
            horizontal = -1;
            vertical = 1;
            return transform.position + (Vector3)upLeft;
        } else if (num == 7) {
            horizontal = 0;
            vertical = 1;
            return transform.position + (Vector3)up;
        } else {
            horizontal = 1;
            vertical = 1;
            return transform.position + (Vector3)upRight;
        }

    }
}
