using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
This script handles one component of an AI for an enemy in a game. 
The goal here is to get Legumel (enemy) to move a random direction
a random amount of tiles after a set time between 1s and 7.5s. This
requires no input from the user and runs indefinitely until Legumel
is either defeated or out of range.

2 of the 4 methods here are called by the Unity API.
*/

public class Roam : MonoBehaviour {
    
    // scripts to be referenced
    private Rigidbody2D rb;
    private Legumel legumel;
    private LegumelAI legumelAI;

    // values that Legumel will go to
    private float roamDist;
    private Vector3 targetPos;

    // simple movement vectors
    private int horizontal;
    private int vertical;

    private bool awaitingDir = false;
    private bool isMoving = false;
    
    float speed = 3f;
    float time = 2f;

    // This function is called by Unity's API  when the script's instance is loaded.
    private void Awake() {
        legumel = GetComponent<Legumel>();
        legumelAI = GetComponent<LegumelAI>();

        // making the function "LegumelRoamMove_OnRoamWaitOver" a listener to the "OnRoamWaitOver" event, called from the script LegumelAI after a random time of waiting is over.
        legumelAI.OnRoamWaitOver += LegumelRoamMove_OnRoamWaitOver;
    }
    
    private void LegumelRoamMove_OnRoamWaitOver(object sender, LegumelAI.OnRoamWaitOverArgs e) {
        roamDist = e.dist;
        isMoving = true;
        awaitingDir = true;
    }

    // This function is called by Unity's API every frame during play mode.
    private void Update() {
        
        // if not moving, Legumel is Idle.
        if (!isMoving) {
            legumelAI.stateHandler.aIMovementState = AIMovementState.Idle;
            legumelAI.move = new Vector2(0, 0);

            return;
        }
        
        if (isMoving) {

            // Get a random direction if waiting for one.
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
    }

    // this function returns a random Vector3 representing 1 direction of the 8.
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
