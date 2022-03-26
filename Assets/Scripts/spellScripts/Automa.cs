using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automa : MonoBehaviour {
    private PrimarySpell primary;
    private SecondarySpell secondary;

    private Player player;

    Vector3 shootDir;

    public float baseDamage;
    public float baseSpread;
    public float baseFireCount;
    public int baseCost;
    public float critChance;
    public float critDamage;

    public bool status;
    public float statusChance;
    public float dps;

    [SerializeField]
    private Transform PFBullet;

    public event EventHandler<OnAutomaCastEventArgs> OnAutomaCast;
    public class OnAutomaCastEventArgs : EventArgs {
        public float baseDamage;
        public float baseSpread;
        public float baseFireCount;
        public int baseCost;
        public float critChance;
        public float critDamage;

        public bool status;
        public string element;
        public float statusChance;
        public float dps;
        
        public Vector3 shootDir;
    }

    public float bulletNum = 1f;
    public float buffer = 0f;
    public float bufferMax = 10f;

    private void Start() {
        primary = GetComponent<PrimarySpell>();
        secondary = GetComponent<SecondarySpell>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        primary.OnPrimaryFire += CastAutoma_OnPrimaryFire;
        secondary.OnSecondaryFire += CastAutoma_OnSecondaryFire;
    }

    private void CastAutoma_OnPrimaryFire(object sender, PrimarySpell.OnPrimaryFireEventArgs e) {
        if (e.type != "Automa") return;
        Debug.Log("Automa Fired! Type: " + e.element);

        // Calculating what direction the bullet will go in when fired
        // (Where da bullet goin?)

        Vector3 originPos = GameObject.FindGameObjectWithTag("SpellOriginPos").transform.position;
        Vector3 shootPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Transform bulletTransform = Instantiate(PFBullet, originPos, Quaternion.identity);

        shootDir = (shootPos - originPos).normalized;

        // Sets the stats for the bullet fired
        // (How da bullet doin?)

        if (e.element == "Arcane") { // Base stats for an Arcane Automa spellstone
            baseDamage = 2f;
            baseSpread = 0.2f;
            baseFireCount = 5f;
            baseCost = 1;
            critChance = 0.05f;
            critDamage = 3f;
            status = false;

            statusChance = 0f;
            dps = 0f;
        }else { // Base stats for an Elemental Automa spellstone
            baseDamage = 3f;
            baseSpread = 0.3f;
            baseFireCount = 5f;
            baseCost = 2;
            critChance = 0.05f;
            critDamage = 6f;
            status = true;

            statusChance = 0.15f;
            dps = 2f;
        }

        // Code for automa bullet burst
        bulletNum = 1f;
        buffer = 0f;
        bufferMax = 10f;

        while (buffer < bufferMax) {
            buffer += 0.01f;

            while ((buffer >= bufferMax) & (bulletNum < baseFireCount + 1f)) {
                Transform bulletTransform = Instantiate(PFBullet, originPos, Quaternion.identity);

                OnAutomaCast?.Invoke(this, new OnAutomaCastEventArgs {
                    // Send spell attributes to bullets fired
                    baseDamage = baseDamage,
                    baseSpread = baseSpread,
                    baseFireCount = baseFireCount,
                    baseCost = baseCost,
                    critChance = critChance,
                    critDamage = critDamage,

                    status = status,
                    element = e.element,
                    statusChance = statusChance,
                    dps = dps,

                    shootDir = shootDir
                });

            bulletNum += 1f;
            buffer = 0f;
            }
        }

        
    }

    private void CastAutoma_OnSecondaryFire(object sender, SecondarySpell.OnSecondaryFireEventArgs e) {
        if (e.type != "Automa") return;
        Debug.Log("Automa Fired Type: " + e.element);
    }

    /* public override void SetStats(bool isPrimary) {
        // Debug.Log("Spell cast!");
        
        if (isPrimary) {
            if (player.primaryElement == PrimarySpellElement.Arcane) { // Base stats for an Arcane Automa spellstone
                baseDamage = 2f;
                baseSpread = 5f;
                baseFireRate = 0.2f;
                baseCost = 1;
                critChance = 0.10f;
                critDamage = 3f;
                status = false;

                statusChance = 0f;
                dps = 0f;
            }else { // Base stats for an Elemental Automa spellstone
                baseDamage = 3f;
                baseSpread = 5f;
                baseFireRate = 0.2f;
                baseCost = 1;
                critChance = 0.10f;
                critDamage = 6f;
                status = true;

                statusChance = 0.15f;
                dps = 2f;
            }
        }else {
            if (player.primaryElement == PrimarySpellElement.Arcane) { // Secondary stats for an Arcane Automa spellstone
                baseDamage = 1.5f;
                baseSpread = 5f;
                baseFireRate = 0.2f;
                baseCost = 1;
                critChance = 0.10f;
                critDamage = 2f;
                status = false;

                statusChance = 0f;
                dps = 0f;
            }else { // Secondary stats for an Elemental Automa spellstone
                baseDamage = 2f;
                baseSpread = 5f;
                baseFireRate = 0.2f;
                baseCost = 1;
                critChance = 0.10f;
                critDamage = 3f;
                status = true;

                statusChance = 0.15f;
                dps = 2f;
            }
        }
    } */
}
