using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laurie : EnemyTarget, IEnemyDamageable {

    // Player experience point values
    [Header("Experience Points")]
    public float xpLevel;
    public float xpMax;
    public float xp;
    
    // Player health point values
    [Header("Hit Points")]
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
    public float manaPointsMax = 5f;
    public float manaPoints;

    // Movement
    [Header("Movement")]
    public float movementSp;
    private float defaultMovementSp = 2f;
    
    public float sprintMod;
    private float defaultSprintMod = 1.75f;

    public float dashMod;
    private float defaultDashMod = 2.5f;

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

    
    
    public float dps;
    public float dpsTimer = 1f;

    public int dpsCountMax = 20;
    public int dpsCount = 20;

    private bool fireParActive = false;
    private bool iceParActive = false;
    private bool toxicParActive = false;

    public Transform parent;

    [SerializeField]
    private Transform pfDust;
    [SerializeField]
    private Transform pfFire;
    [SerializeField]
    private Transform pfIce;
    [SerializeField]
    private Transform pfLightning;
    [SerializeField]
    private Transform pfToxic;
    [SerializeField]
    private Transform pfCrit;

    private void Start() {
        MaxHP();
        MaxMP();

        abilityCooldownLimit = 2f;

        spindashDist = 5f;
        blinkdashDist = 3f;

        movementSp = defaultMovementSp;
        sprintMod = defaultSprintMod;
        dashMod = defaultDashMod;
    }

    public void MaxHP() {
        hitPoints = hitPointsMax;
    }

    public void MaxMP() {
        manaPoints = manaPointsMax;
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
    
    // might add functionality for bypassing stress resistance
    // public void CalcStress() {
    //     float refactoredStress = stress - stressRes;
    //     if (stress < 0f) stress = 0f;
    //     float refactoredStability = stability - refactoredStress;

    //     movementSp = movementSp * refactoredStability;
    //     sprintMod = sprintMod * refactoredStability;
    //     dashMod = dashMod * refactoredStability;
    // }

    private float CalcMovementSpeedWithStress() {
        float refactoredStress = stress - stressRes;
        if (stress < 0f) stress = 0f;
        float refactoredStability = stability - refactoredStress;

        return defaultMovementSp * refactoredStability;
    }

    private float CalcSprintModWithStress() {
        float refactoredStress = stress - stressRes;
        if (stress < 0f) stress = 0f;
        float refactoredStability = stability - refactoredStress;

        return defaultSprintMod * refactoredStability;
    }

    private float CalcDashModWithStress() {
        float refactoredStress = stress - stressRes;
        if (stress < 0f) stress = 0f;
        float refactoredStability = stability - refactoredStress;

        return defaultDashMod * refactoredStability;
    }

    public float CheckRes(float damage, string statusType) {
        float refactoredDamage;
        
        if (statusType == "Pyro") {
            refactoredDamage = damage * pyroRes;
        }else if (statusType == "Cryo") {
            refactoredDamage = damage * cryoRes;
        }else if (statusType == "Bolt") {
            refactoredDamage = damage * boltRes;
        }else if (statusType == "Toxi") {
            refactoredDamage = damage * toxiRes;
        }else {
            refactoredDamage = damage * arcaneRes;
        }

        return refactoredDamage;
    }

    // When Laurie takes damage, this function is called.
    // it brings in baseDamage, critDamage, statusType and dps 
    // from the bullet that hit her.

    public void Damage(float damage, float critDamage, bool crit, bool status, string statusType, float dps, bool castByPlayer) {

        // If hit by a bullet cast by the caster, stop here.
        
        if (castByPlayer) return;

        // Logs the bullet stats on hit.

       Debug.Log("Damage: " + damage + " | Crit Damage: " + critDamage + "| Crit? " + crit + " | Status? " + status + " | Element: " + statusType + " | Damage/sec: " + dps); 

       // Check resistances and buffs.

       float refactoredDamage = CheckRes(damage, statusType);

       // Applies debuffs.

        if (statusType == "Pyro") {
            isOnFire = true;
        }
        if (statusType == "Cryo") {
            isFreezing = true;
        }
        if (statusType == "Bolt") {
            isZapped = true;
        }
        if (statusType == "Toxi") {
            isPoisoned = true;
        }

        this.dps = dps;

       // Subtracts the bullet damage from hp.

       hitPoints -= refactoredDamage;

       // Logs final hp calc.

       Debug.Log("Health: " + hitPoints);

       HandleParticles(statusType, crit);
    }

    // This function is only called if Laurie
    // needs to take damage from a status effect.

    public void DamageOverTime() {
        if (hitPoints <= 0f) {
            Die();
            return;
        }

        hitPoints -= dps;
        dpsCount -= 1;

        Debug.Log("Damage/sec: " + dps);
        Debug.Log("Health " + hitPoints);
    }

    // this function spawns particles when needed.

    private void HandleParticles(string statusType, bool isCrit) {
        if (isCrit) {
            Transform crit = Instantiate(pfCrit, GetPosition(), Quaternion.identity, parent);
        }
        
        if (statusType == "Arcane") { 
            Transform dust = Instantiate(pfDust, GetPosition(), Quaternion.identity, parent);
        }else if (isOnFire && !fireParActive) {
            Transform fire = Instantiate(pfFire, GetPosition(), new Quaternion(-1f, 0f, 0f, 1f), parent);
            fireParActive = true;
        }else if (isFreezing && !iceParActive) {
            Transform ice = Instantiate(pfIce, GetPosition(), Quaternion.identity, parent);
            iceParActive = true;
        }else if (isZapped) {
            Transform lightning = Instantiate(pfLightning, GetPosition(), Quaternion.identity, parent);
        }else if (isPoisoned && !toxicParActive) {
            Transform toxic = Instantiate(pfToxic, GetPosition(), Quaternion.identity, parent);
            toxicParActive = true;
        }
        
    }

    private void Update() {

        if (hitPoints <= 0f) {
            Die();
            return;
        }

        movementSp = CalcMovementSpeedWithStress();
        sprintMod = CalcSprintModWithStress();
        dashMod = CalcDashModWithStress();

        /* 
        This is a 1s timer. every time this timer hits 0,
        it is reset back to 1, and the DamageOverTime method 
        is called. essentially, there is a 1 in 20 chance that
        the debuff is removed on the first tick of damage. with
        every tick of damage, the chance of the debuff getting
        removed is higher, until the dpsCount is zero, where 20s
        have passed and the debuff is removed. 
        */

        float timerMax = 1f;

        dpsTimer = dpsTimer - Time.deltaTime;
        if (dpsTimer <= 0) {
            if(isOnFire) {DamageOverTime();}
            if(isFreezing) {DamageOverTime();}
            if(isZapped) {DamageOverTime();}
            if(isPoisoned) {DamageOverTime();}

            int dpsRand = Random.Range(1,dpsCount);
            
            if(dpsRand == 1) {

                // remove all debuffs
                isOnFire = false;
                isFreezing = false;
                isZapped = false;
                isPoisoned = false;

                // reset dpsCount
                dpsCount = dpsCountMax;
            }
            dpsTimer = timerMax;
        }
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    public void Die() {
        Destroy(gameObject);
    }
}
