using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegumelSearch : MonoBehaviour {
    private Legumel legumel;
    
    public float aggroRange = 7.5f;
    

    private void Start() {
        legumel = GetComponent<Legumel>();
    }

    private void Update() {
        if (legumel.dead) return;

        if (Vector2.Distance(transform.position, legumel.targetPosition) < aggroRange) {
            legumel.aggro = true;
        }
    }
}
