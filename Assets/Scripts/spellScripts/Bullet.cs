using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private BulletShooter bulletShooter;

    private Burston burston;
    private Automa automa;
    
    private Vector3 shootDir;

    public float damage;
    public float cooldown;
    public float spread;
    public float fireCount;
    public int cost;
    public float critChance;
    public float critDamage;

    public bool crit;

    public bool status;
    public string element;
    public float statusChance;
    public float dps;

    private bool castByPlayer;

    private void Awake() {
        // bulletShooter = GameObject.FindGameObjectWithTag("BulletShooter").GetComponent<BulletShooter>();

        burston = GameObject.FindGameObjectWithTag("Spellcaster").GetComponent<Burston>();
        automa = GameObject.FindGameObjectWithTag("Spellcaster").GetComponent<Automa>();

        // bulletShooter.OnSpellCast += SpellBullet_OnSpellCast;

        burston.OnBurstonCast += BurstonBullet_OnBurstonCast;
        automa.OnAutomaCast += AutomaBullet_OnAutomaCast;

        castByPlayer = false;
    }

    public void SpellBullet_OnSpellCast(object sender, BulletShooter.OnSpellCastEventArgs e) {
        shootDir = e.shootDir;

        damage = e.baseDamage;
        cooldown = e.baseCooldown;
        cost = e.baseCost;
        critChance = e.critChance;
        critDamage = e.critDamage;

        crit = e.crit;

        status = e.status;
        element = e.element;
        
        statusChance = e.statusChance;
        dps = e.dps;

        if (crit) damage = critDamage;

        castByPlayer = false;
    }

    public void BurstonBullet_OnBurstonCast(object sender, Burston.OnBurstonCastEventArgs e) {
        // Destroy(gameObject, 2f);
        
        shootDir = e.shootDir;

        damage = e.baseDamage;
        cooldown = e.baseCooldown;
        cost = e.baseCost;
        critChance = e.critChance;
        critDamage = e.critDamage;

        crit = e.crit;

        status = e.status;
        element = e.element;
        
        statusChance = e.statusChance;
        dps = e.dps;

        if (crit) damage = critDamage;

        castByPlayer = true;
    }

    public void AutomaBullet_OnAutomaCast(object sender, Automa.OnAutomaCastEventArgs e) {
        shootDir = e.shootDir;

        damage = e.baseDamage;
        cooldown = 0f;
        spread = e.baseDamage;
        fireCount = e.baseFireCount;
        cost = e.baseCost;
        critChance = e.critChance;
        critDamage = e.critDamage;

        status = e.status;
        element = e.element;

        statusChance = e.statusChance;
        dps = e.dps;

        castByPlayer = true;
    }

    private void Update() {
        float moveSpeed = 50f;
        transform.position += shootDir * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        IPlayerDamageable playerDamageable = collider.GetComponent<IPlayerDamageable>();
        IEnemyDamageable enemyDamagable = collider.GetComponent<IEnemyDamageable>();

        if (enemyDamagable != null) {
            // Hit a damagable player
            // Dont deal damage to player if the bullet was shot by player
            enemyDamagable.Damage(damage, critDamage, crit, status, element, dps, castByPlayer);

            if (!castByPlayer) Destroy(gameObject);
        }
        if (playerDamageable != null) {
            // Hit a damagable entity
            playerDamageable.Damage(damage, critDamage, crit, status, element, dps);
            Destroy(gameObject);
        }
        
    }
}
