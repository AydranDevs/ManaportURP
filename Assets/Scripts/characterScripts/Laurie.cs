using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laurie : EnemyTarget {

    // Player experience point values
    [Header("Experience Points")]
    public float xpLevel;
    public float xpMax;
    public float xp;
    
    // Player health point values
    [Header("Hit Points")]
    public const float HIT_POINTS_MAX_DEFAULT = 20f;
    public float hitPointsMax = 20f;
    public float hitPoints;

    // Player stability point values
    [Header("Stability Points")]
    public float stabilityMax = 1f;
    public float stability = 1f; // (100%)
    public float stressMax = 1f;
    public float stress = 0f;

    // Player mana point values
    [Header("Mana Points")]
    public const float  MANA_POINTS_MAX_DEFAULT = 5f;
    public float manaPointsMax = 5f;
    public float manaPoints;
    
    public float manaPointsRegen;
    public const float MANA_REGEN_DEFAULT = 0.5f;

    // Movement
    [Header("Movement")]
    public float pushSp;
    private float defaultPushSp = 1.5f;

    public float dashSp;
    public float sprintSp;
    public float walkSp;
    private float defaultwalkSp = 2f;
    
    public float sprintMod;
    private float defaultSprintMod = 1.35f;

    public float dashMod;
    private float defaultDashMod = 1.5f;

    // Abilities
    [Header("Abilities")]
    public float abilityCooldownLimit;

    public float spindashDist;
    public float blinkdashDist;
    public float pounceDist;

    // Defenses
    [Header("Defenses")]
    // percents
    public float stressRes = 0.50f;
    public float pyroRes = 0.00f;
    public float cryoRes = 0.00f;
    public float boltRes = 0.00f;
    public float toxiRes = 0.00f;
    public float arcaneRes = 0.00f;

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
    public bool isAbsorbing = false;

    private Player player;

    private float manaRegenTimer;
    public const float MANA_REGEN_TIMER_DEFAULT = 1f;

    private void Start() {
        hitPointsMax = HIT_POINTS_MAX_DEFAULT;
        player = GetComponent<Player>();

        MaxHP();
        MaxMP();

        abilityCooldownLimit = 2f;

        spindashDist = 5f;
        blinkdashDist = 3f;
 
        pushSp = defaultPushSp;
        walkSp = defaultwalkSp;
        sprintMod = defaultSprintMod;
        dashMod = defaultDashMod;

        sprintSp = defaultwalkSp * sprintMod;
        dashSp = defaultwalkSp * dashMod;

        manaRegenTimer = MANA_REGEN_TIMER_DEFAULT;
        manaPointsRegen = MANA_REGEN_DEFAULT;
    }

    public void MaxHP() {
        hitPoints = hitPointsMax;
    }

    public void MaxMP() {
        manaPoints = manaPointsMax;
    }

    public void RegenMP() {
        manaRegenTimer = manaRegenTimer - Time.deltaTime;

        if (manaRegenTimer <= 0f) {
            manaPoints = manaPoints + manaPointsRegen;
            manaRegenTimer = MANA_REGEN_TIMER_DEFAULT;
        }
    }

    public void AddXP(string type, float amount) {
        if (type == "points") {
            xp += amount;
        }else if (type == "levels") {
            xpLevel += amount;
        }else {
            Debug.Log("Error! incorrect xp type given. (Laurie.cs)");
            return;
        }
    }

    public void LevelUp() {
        xp = 0f;
        xpLevel++;
        xpMax = xpMax * 2;

        hitPointsMax += 2f;
        manaPointsMax += 2f;
    }

    private void Update() {
        if (hitPoints <= 0f) {
            Die();
            return;
        }

        if (!player.manaRegenCoolingDown && manaPoints < manaPointsMax) {
            RegenMP();
        }
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    public void Die() {
        Destroy(gameObject);
    }
}
