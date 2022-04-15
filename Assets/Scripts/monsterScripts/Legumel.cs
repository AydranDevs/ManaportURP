using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legumel : Creature {
    public Transform player;
    public Vector2 targetPosition;

    public LegumelAttacking legumelAttacking;
    public LegumelSearch legumelSearch;

    public bool aggro = false;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        legumelAttacking = GetComponent<LegumelAttacking>();
        legumelSearch = GetComponent<LegumelSearch>();
    }

    private void Update() {
        targetPosition = (Vector2)player.transform.position;
    }

    public override void Damage(float damage) {
        Debug.Log("Hit! Damage: " + damage);
    }
}
