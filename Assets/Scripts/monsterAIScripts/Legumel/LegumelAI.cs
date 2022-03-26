using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This script actually does AI things.
*/

public class LegumelAI : MonoBehaviour, IEnemyAI {

    public LegumelAIStateHandler stateHandler;
    private Player player;

    public Vector2 move = new Vector2(0, 0);

    // called when the random wait time is over
    public event EventHandler<OnRoamWaitOverArgs> OnRoamWaitOver;
    public class OnRoamWaitOverArgs : EventArgs { public float dist; }
    
    // minimum and maximum possible legnths the AI can roam
    private const float MIN_ROAM_DIST = 3f;
    private const float MAX_ROAM_DIST = 6f;
    
    // minimum and maximum possible times the AI can wait before roaming a random distance
    private const float MIN_ROAM_WAIT = 1f;
    private const float MAX_ROAM_WAIT = 7.5f;

    // view values
    private const float VIEW_RADIUS = 5f;
    private const float VIEW_ROT = 0f;
    private const float VIEW_FOV = 0f;


    // roaming values
    private float waitTime = 0f;
    private bool awaitingWaitTime = true;
    private float roamDist = 0f;
    private bool currentlyWaiting = false; // pretty much used to check if lil nut boy is roaming

    

    // line of sight
    // [SerializeField]
    // private bool northLOSActive = false;
    // [SerializeField]
    // private bool eastLOSActive = false;
    // [SerializeField]
    // private bool southLOSActive = false;
    // [SerializeField]
    // private bool westLOSActive = false;

    // [SerializeField]
    // private bool northDetected = false;
    // [SerializeField]
    // private bool eastDetected = false;
    // [SerializeField]
    // private bool southDetected = false;
    // [SerializeField]
    // private bool westDetected = false;

    // public int los = 0;

    
    
    private void Awake() {
        
        stateHandler = GetComponent<LegumelAIStateHandler>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        // ActivateAllLinesOfSight();

    }

    private void Update() {

        // The first thing to do here is check lines of sight. [1 = north, 2 = east, 3 = south, 4 = west]
        // los = CheckLinesOfSight();

        // if (los != -1) {
            // player detected
            // stateHandler.aIState = AIState.DetectTarget;
        // }

        if (stateHandler.aIState == AIState.Roam) {
            if (!currentlyWaiting) {
                roamDist = GetRandomRoamDist();
                // stateHandler.aIMovementState = AIMovementState.Walk;
                currentlyWaiting = true;
            } else {
                RoamWaitUpdate();
                // stateHandler.aIMovementState = AIMovementState.Idle;
            }
        }
    }

    private void RoamWaitUpdate() {
        
        
        if (awaitingWaitTime) {
            waitTime = GetRandomRoamWaitTime();
            awaitingWaitTime = false;
        }

        waitTime = waitTime - Time.deltaTime;
        if (waitTime <= 0f) {
            OnRoamWaitOver?.Invoke(this, new OnRoamWaitOverArgs { dist = roamDist });
            
            currentlyWaiting = false;
            awaitingWaitTime = true;
        }
    }

    private float GetRandomRoamWaitTime() {
        return UnityEngine.Random.Range(MIN_ROAM_WAIT, MAX_ROAM_WAIT);
    }

    private float GetRandomRoamDist() {
        return UnityEngine.Random.Range(MIN_ROAM_DIST, MAX_ROAM_DIST);
    }

    // private void ActivateAllLinesOfSight() {
    //     northLOSActive = true;
    //     eastLOSActive = true;
    //     southLOSActive = true;
    //     westLOSActive = true;
    // }

    // private int CheckLinesOfSight() {
        
    //     // check current facing state
    //     int facing = (int)stateHandler.aIFacingState;
        
    //     // disable the opposite line of sight
    //     if (facing == 1) {
    //         ActivateAllLinesOfSight();
    //         southLOSActive = false;

    //         CheckIfPlayerInLineOfSight();
    //     } else if (facing == 2) {
    //         ActivateAllLinesOfSight();
    //         westLOSActive = false;

    //         CheckIfPlayerInLineOfSight();
    //     } else if (facing == 3) {
    //         ActivateAllLinesOfSight();
    //         northLOSActive = false;

    //         CheckIfPlayerInLineOfSight();
    //     } else {
    //         ActivateAllLinesOfSight();
    //         eastLOSActive = false;

    //         CheckIfPlayerInLineOfSight();
    //     } 
        
        
    //     if (northDetected && northLOSActive && player.transform.position.y <= transform.position.y) {
    //         return 0;
    //     }else if (eastDetected && eastLOSActive && player.transform.position.x <= transform.position.x) {
    //         return 1;
    //     }else if (southDetected && southLOSActive && player.transform.position.y >= transform.position.y) {
    //         return 2;
    //     }else if (westDetected && westLOSActive && player.transform.position.x >= transform.position.x) {
    //         return 3;
    //     }else {
    //         return -1; // no active lines are detecting anything.
    //     }
    // }

    // private void CheckIfPlayerInLineOfSight() {
    //     if (player.transform.position.y >= transform.position.y) {
    //         northDetected = true;
    //         eastDetected = false;
    //         southDetected = false;
    //         westDetected = false;
    //         Debug.Log("north");
    //     }
    //     if (player.transform.position.x >= transform.position.x) {
    //         northDetected = false;
    //         eastDetected = true;
    //         southDetected = false;
    //         westDetected = false;
    //         Debug.Log("east");
    //     }
    //     if (player.transform.position.y <= transform.position.y) {
    //         northDetected = false;
    //         eastDetected = false;
    //         southDetected = true;
    //         westDetected = false;
    //         Debug.Log("south");
    //     }
    //     if (player.transform.position.x <= transform.position.x) {
    //         northDetected = false;
    //         eastDetected = false;
    //         southDetected = false;
    //         westDetected = true;
    //         Debug.Log("west");
    //     }
    // }
}
