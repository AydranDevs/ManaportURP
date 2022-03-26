using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blasteur : Spell {
    private PrimarySpell primary;
    private SecondarySpell secondary;

    private Player player;
    private GameObject defaultBullet;

    public float baseDamage;
    public float baseSpread;
    public float baseCooldown;
    public int baseCost;
    public float critChance;
    public float critDamage;

    public bool status;
    public float statusChance;
    public float dps;


    private void Start() {
        primary = GetComponent<PrimarySpell>();
        secondary = GetComponent<SecondarySpell>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        defaultBullet = GameObject.FindGameObjectWithTag("PlayerBullet");
    }

    public override void SetStats(bool isPrimary) {
        // Debug.Log("Spell cast!");
        
        if (isPrimary) {
            if (player.primaryElement == PrimarySpellElement.Arcane) { // Base stats for an Arcane Blasteur spellstone
                baseDamage = 4f;
                baseSpread = 20f; 
                baseCooldown = 0.5f;
                baseCost = 4;
                critChance = 0.10f;
                critDamage = 7f;
                status = false;

                statusChance = 0f;
                dps = 0f;
            }else { // Base stats for an Elemental Blasteur spellstone
                baseDamage = 6f;
                baseSpread = 20f;
                baseCooldown = 0.5f;
                baseCost = 5;
                critChance = 0.10f;
                critDamage = 9f;
                status = true;

                statusChance = 0.10f;
                dps = 2f;
            }
        }else {
            if (player.primaryElement == PrimarySpellElement.Arcane) { // Secondary stats for an Arcane Blasteur spellstone
                baseDamage = 2f;
                baseSpread = 20f; 
                baseCooldown = 0.5f;
                baseCost = 2;
                critChance = 0.10f;
                critDamage = 4f;
                status = false;

                statusChance = 0f;
                dps = 0f;
            }else { // Seconary stats for an Elemental Blasteur spellstone
                baseDamage = 3f;
                baseSpread = 20f;
                baseCooldown = 0.5f;
                baseCost = 3;
                critChance = 0.10f;
                critDamage = 5f;
                status = true;

                statusChance = 0.10f;
                dps = 2f;
            }
        }

        CastBlasteur();
        player.ability = AbilityState.None;
    }

    public void CastBlasteur() {
        /* Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = (Input.mousePosition - sp).normalized;
 
        // Calculate the angle
        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
 
        // Create a rotation from euler angles, using calculated angle for the z axis
        Quaternion bulletRotation =  Quaternion.Euler(new Vector3(0, 0, angle));

        // Instantiate the bullet using our new rotation
        GameObject bullet = (GameObject)GameObject.Instantiate(defaultBullet, transform.position, bulletRotation);

        // Apply stray
        bullet.transform.Rotate(0, 0, Random.Range(-10,10)); */
    }
}
