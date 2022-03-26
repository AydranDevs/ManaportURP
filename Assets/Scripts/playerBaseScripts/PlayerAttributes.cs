using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour {
    
    // Player experience point values
    [Header("Experience Points")]
    public float xpLevel;
    public float xpMax;
    public float xp;
    
    // Player health point values
    [Header("Health Points")]
    public float healthMax;
    public float health;

    // Player mana point values
    [Header("Mana Points")]
    public float manaMax;
    public float mana;

    // Debuffs
    [Header("Debuffs")]
    public bool isOnFire = false;
    public bool isFreezing = false;
    public bool isZapped = false;
    public bool isPoisoned = false;

    // Buffs
    [Header("Buffs")]
    public bool isPyroBoosted = false; // Provides a bit of defense against pyro - increases damage dealt by pyro spells
    public bool isCryoBoosted = false; // Provides a bit of defense against cryo - increases damage dealt by cryo spells
    public bool isBoltBoosted = false; // Provides a bit of defense against bolt - increases damage dealt by bolt spells
    public bool isToxiBoosted = false; // Provides a bit of defense against toxi - increases damage dealt by toxi spells

    [Tooltip("increases the player's movement speed and sprint modifier by a set amount")]
    public bool isSpeedBoosted = false;
    
    [Tooltip("Increases crit chance and crit damage of all spells by a set amount")]
    public bool isCritBoosted = false;

    [Tooltip("Regenerates a fixed amount of health over a fixed amount of time")]
    public bool isRegenerating = false;
    
    [Tooltip("Increases overall damage and makes the player nearly 'invinicible', all damage taken is added together and instead taken over time. Similar to Payday 2's Stoic. also causes some visual things you'll see later.")]
    public bool isLashingOut = false; 

    [Tooltip("Makes the player invulnerable to ALL types of damage for a short amount of time")]
    public bool isInvincible = false;

    [Tooltip("Converts all damage taken to shield for a short amount of time")]
    public bool isAborbing = false;

    // Defenses
    [Header("Defenses")]
    public float pyroRes = 0f;
    public float cryoRes = 0f;
    public float boltRes = 0f;
    public float toxiRes = 0f;
    public float arcaneRes = 0f;

    // Movement
    [Header("Movement")]
    public float movementSp;
    public float sprintMod;
    public float dashMod;

    public float spindashDist;
    public float blinkdashDist;
    public float pounceDist;


    private void Start() {
        
        // XP Initialization
        xpLevel = 0f;
        xp = 0f;
        xpMax = 10f;

        // Health initialization
        healthMax = 5f;
        health = healthMax;
    }
}
