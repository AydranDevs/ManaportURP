using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legumel : Creature {
    public GameObject[] targets;
    public Vector2 targetPosition;

    public LegumelAttacking legumelAttacking;
    public LegumelSearch legumelSearch;

    public HealthBarScript healthBar;
    public Canvas canvas;
    public SpriteRenderer spriteRenderer;
    public GameObject deathParticles;

    public bool aggro = false;
    public bool dead = false;

    public float hitPoints;
    public float hitPointsMax = 10f;

    private void Start() {
        targets = GameObject.FindGameObjectsWithTag("Friendly");
        legumelAttacking = GetComponent<LegumelAttacking>();
        legumelSearch = GetComponent<LegumelSearch>();

        hitPoints = hitPointsMax;
        healthBar.SetMaxHealth(hitPointsMax);

        Creature.creatureList.Add(this);
    }

    private void Update() {
        if (dead) return;
        if (hitPoints <= 0f) {
            Die();
        }
        
        UpdateHealthBar();
        

        
        targetPosition = (Vector2)targets[0].transform.position;
    }

    public void Die() {
        if (!dead) {
            GameObject death = Instantiate(deathParticles, transform.position, new Quaternion());
            dead = true;
        }

        spriteRenderer.enabled = false;
        Destroy(canvas.gameObject);
    } 

    public override void Damage(float damage) {
        hitPoints = hitPoints - damage;
    }

    private void UpdateHealthBar() {
        healthBar.SetHealth(hitPoints);
    }
}
