using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legumel : MonoBehaviour, IPlayerDamageable {
    
    public float hitPointsMax = 10f;
    public float hitPoints;

    public float dps;
    public float dpsTimer = 1f;

    public int dpsCountMax = 20;
    public int dpsCount = 20;

    public bool isOnFire = false;
    public bool isFreezing = false;
    public bool isZapped = false;
    public bool isPoisoned = false;

    private bool fireParActive = false;
    private bool iceParActive = false;
    private bool toxicParActive = false;

    public event EventHandler<OnLegumelSummonParticlesEventArgs> OnLegumelSummonParticles;
    public class OnLegumelSummonParticlesEventArgs : EventArgs {
        public string particleType;
    }

    public event EventHandler<OnLegumelDestroyParticlesEventArgs> OnLegumelDestroyParticles;
    public class OnLegumelDestroyParticlesEventArgs : EventArgs {
        public string particleType;
        public bool persist;
    }

    private void Awake() {
        hitPoints = hitPointsMax;
    }
    
    // When this Monster takes damage, this function is called.
    // it brings in baseDamage, critDamage, statusType and dps 
    // from the bullet that hit it.

    public void Damage(float damage, float critDamage, bool crit, bool status, string statusType, float dps) {

        // Logs the bullet stats on hit.

       Debug.Log("Damage: " + damage + " | Crit Damage: " + critDamage + "| Crit? " + crit + " | Status? " + status + " | Element: " + statusType + " | Damage/sec: " + dps); 

       // If crit, summon crit particles
       
       if (crit) {
           OnLegumelSummonParticles?.Invoke(this, new OnLegumelSummonParticlesEventArgs {
               particleType = "Crit"
            });
       }
       
       // Applies debuff and summon particles

        if (statusType == "Pyro") {
            isOnFire = true;
            OnLegumelSummonParticles?.Invoke(this, new OnLegumelSummonParticlesEventArgs {
                particleType = statusType
            });
        }else if (statusType == "Cryo") {
            isFreezing = true;
            OnLegumelSummonParticles?.Invoke(this, new OnLegumelSummonParticlesEventArgs {
                particleType = statusType
            });
        }else if (statusType == "Bolt") {
            isZapped = true;
            OnLegumelSummonParticles?.Invoke(this, new OnLegumelSummonParticlesEventArgs {
                particleType = statusType
            });
        }else if (statusType == "Toxi") {
            isPoisoned = true;
            OnLegumelSummonParticles?.Invoke(this, new OnLegumelSummonParticlesEventArgs {
                particleType = statusType
            });
        }else { // if no debuff, assume Arcane.
            OnLegumelSummonParticles?.Invoke(this, new OnLegumelSummonParticlesEventArgs {
                particleType = "Arcane"
            });
        }

        this.dps = dps;

       // Subtracts the bullet damage from hp.

       hitPoints -= damage;

       // Logs final hp calc.

       Debug.Log("Health: " + hitPoints);

       if (hitPoints <= 0f) Die();
    }

    // This function is only called if this Monster
    // need to take damage from a status effect.

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

    private void Update() {

        /* 
        This is a 1s timer. every time this timer hits 0,
        it is reset back to 1, and the DamageOverTime method 
        is called. essentially, there is a 1 in 20 chance that
        the debuff is removed on the first tick of damage. With
        every tick of damage, the chance of the debuff getting
        removed is higher, until the dpsCount is zero, where 20s
        have passed and the debuff is removed. 
        */

        float timerMax = 1f;

        if (hitPoints <= 0f) {
            Die();
            return;
        }

        dpsTimer = dpsTimer - Time.deltaTime;
        if (dpsTimer <= 0) {
            if(isOnFire) {DamageOverTime();}
            if(isFreezing) {DamageOverTime();}
            if(isZapped) {DamageOverTime();}
            if(isPoisoned) {DamageOverTime();}

            int dpsRand = UnityEngine.Random.Range(1,dpsCount);
            if(dpsRand == 1) {

                // remove all debuffs
                isOnFire = false;
                OnLegumelDestroyParticles?.Invoke(this, new OnLegumelDestroyParticlesEventArgs {
                    particleType = "Pyro",
                    persist = false
                });
                isFreezing = false;
                OnLegumelDestroyParticles?.Invoke(this, new OnLegumelDestroyParticlesEventArgs {
                    particleType = "Cryo",
                    persist = false
                });
                isZapped = false;
                OnLegumelDestroyParticles?.Invoke(this, new OnLegumelDestroyParticlesEventArgs {
                    particleType = "Bolt",
                    persist = false
                });
                isPoisoned = false;
                OnLegumelDestroyParticles?.Invoke(this, new OnLegumelDestroyParticlesEventArgs {
                    particleType = "Toxi",
                    persist = false
                });

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
