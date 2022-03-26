using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class Burston : MonoBehaviour {
    private PrimarySpell primary;
    private SecondarySpell secondary;
    private Controls controls;

    private Player player;

    Vector3 shootDir;

    public float baseDamage;
    public float baseCooldown;
    public int baseCost;
    public float critChance;
    public float critDamage;

    public bool crit;

    public bool status;
    public float statusChance;
    public float dps;

    public int rand;

    [SerializeField]
    private Transform PFBullet;

    public event EventHandler<OnBurstonCastEventArgs> OnBurstonCast;
    public class OnBurstonCastEventArgs : EventArgs {
        public float baseDamage;
        public float baseCooldown;
        public int baseCost;
        public float critChance;
        public float critDamage;

        public bool crit;

        public bool status;
        public string element;
        public float statusChance;
        public float dps;

        public Vector3 shootDir;
    }


    private void Start() {
        primary = GetComponent<PrimarySpell>();
        secondary = GetComponent<SecondarySpell>();
        controls = GameObject.FindGameObjectWithTag("Player").GetComponent<Controls>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        primary.OnPrimaryFire += CastBurston_OnPrimaryFire;
        secondary.OnSecondaryFire += CastBurston_OnSecondaryFire;
    }

    private void CastBurston_OnPrimaryFire(object sender, PrimarySpell.OnPrimaryFireEventArgs e) {
        if (e.type != "Burston") return;
        Debug.Log("Burston Fired! Type: " + e.element);

        // Calculating what direction the bullet will go in when fired
        // (Where da bullet goin?)
        
        // Debug.Log("Burston Fired.");
        Vector3 originPos = GameObject.FindGameObjectWithTag("SpellOriginPos").transform.position;
        Vector3 shootPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Transform bulletTransform = Instantiate(PFBullet, originPos, Quaternion.identity);

        shootDir = (shootPos - originPos).normalized;
        
        // bulletTransform.GetComponent<Bullet>().Setup(shootDir);

        // Sets the stats for the bullet fired 
        // (How da bullet doin?)

        if (e.element == "Arcane") { // Base stats for an Arcane Burston spellstone
            baseDamage = 3f;
            baseCooldown = 0.2f;
            baseCost = 2;
            critChance = 0.10f;
            critDamage = 5f;
            status = false;

            statusChance = 0f;
            dps = 0f;
        }else { // Base stats for an Elemental Burston spellstone
            baseDamage = 4f;
            baseCooldown = 0.2f;
            baseCost = 2;
            critChance = 0.10f;
            critDamage = 7f;
            status = true;

            statusChance = 0.25f;
            dps = 2f;
        }

        rand = Random.Range(1,10);
        if(rand == 1) {crit = true;}else {crit = false;}

        OnBurstonCast?.Invoke(this, new OnBurstonCastEventArgs {
            // Send spell attributes to bullets fired
            
            baseDamage = baseDamage,
            baseCooldown = baseCooldown,
            baseCost = baseCost,
            critChance = critChance,
            critDamage = critDamage,

            crit = crit,

            status = status,
            element = e.element,
            statusChance = statusChance,
            dps = dps,

            shootDir = shootDir
        });
    }

    private void CastBurston_OnSecondaryFire(object sender, SecondarySpell.OnSecondaryFireEventArgs e) {
        if (e.type != "Burston") return;
        Debug.Log("Burston Fired! Type: " + e.element);

        // Calculating what direction the bullet will go in when fired
        // (Where da bullet goin?)
        
        Debug.Log("Burston Fired.");
        Vector3 originPos = GameObject.FindGameObjectWithTag("SpellOriginPos").transform.position;
        Vector3 shootPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Transform bulletTransform = Instantiate(PFBullet, originPos, Quaternion.identity);

        shootDir = (shootPos - originPos).normalized;
        
        // bulletTransform.GetComponent<Bullet>().Setup(shootDir);

        // Sets the stats for the bullet fired 
        // (How da bullet doin?)

        if (e.element == "Arcane") { // Base stats for an Arcane Burston spellstone
            baseDamage = 1.5f;
            baseCooldown = 0.2f;
            baseCost = 1;
            critChance = 0.10f;
            critDamage = 3f;
            status = false;

            statusChance = 0f;
            dps = 0f;
        }else { // Base stats for an Elemental Burston spellstone
            baseDamage = 2f;
            baseCooldown = 0.2f;
            baseCost = 1;
            critChance = 0.10f;
            critDamage = 4f;
            status = true;

            statusChance = 0.25f;
            dps = 2f;
        }

        rand = Random.Range(1,10);
        if(rand == 1) {crit = true;}else {crit = false;}

        OnBurstonCast?.Invoke(this, new OnBurstonCastEventArgs {
            // Send spell attributes to bullets fired
            
            baseDamage = baseDamage,
            baseCooldown = baseCooldown,
            baseCost = baseCost,
            critChance = critChance,
            critDamage = critDamage,

            crit = crit,

            status = status,
            element = e.element,
            statusChance = statusChance,
            dps = dps,

            shootDir = shootDir
        });

    }

    /* public void SetStats(bool isPrimary) {
        // Debug.Log("Spell cast!");
        if (isPrimary) {
            if (player.primaryElement == PrimarySpellElement.Arcane) { // Base stats for an Arcane Burston spellstone
                baseDamage = 3f;
                baseCooldown = 0.2f;
                baseCost = 2;
                critChance = 0.10f;
                critDamage = 5f;
                status = false;

                statusChance = 0f;
                dps = 0f;
            }else { // Base stats for an Elemental Burston spellstone
                baseDamage = 4f;
                baseCooldown = 0.2f;
                baseCost = 2;
                critChance = 0.10f;
                critDamage = 7f;
                status = true;

                statusChance = 0.25f;
                dps = 2f;
            }
        }else {
            if (player.primaryElement == PrimarySpellElement.Arcane) { // Secondary stats for an Arcane Burston spellstone
                baseDamage = 1.5f;
                baseCooldown = 0.2f;
                baseCost = 1;
                critChance = 0.10f;
                critDamage = 3f;
                status = false;

                statusChance = 0f;
                dps = 0f;
            }else { // Secondary stats for an Elemental Burston spellstone
                baseDamage = 2f;
                baseCooldown = 0.2f;
                baseCost = 1;
                critChance = 0.10f;
                critDamage = 4f;
                status = true;

                statusChance = 0.25f;
                dps = 2f;
            }
        }

        
    } */
    
    /* private void HandleBurston() {
        if (controls.buttonSpellcastPrimary | controls.buttonSpellcastSecondary) {
            Debug.Log("Burston Fired.");
            Vector3 originPos = GameObject.FindGameObjectWithTag("SpellOriginPos").transform.position;
            Vector3 shootPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Transform bulletTransform = Instantiate(PFBullet, originPos, Quaternion.identity);

            Vector3 shootDir = (shootPos - originPos).normalized;
            bulletTransform.GetComponent<Bullet>().Setup(shootDir);
        }
    } */
}
