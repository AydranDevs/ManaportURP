using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour {
    public Vector3 shootDir;

    [SerializeField]
    private Transform pfBullet;

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

    public event EventHandler<OnBulletShootEventArgs> OnBulletShoot;
    public class OnBulletShootEventArgs : EventArgs {
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

    public event EventHandler<OnSpellCastEventArgs> OnSpellCast;
    public class OnSpellCastEventArgs : EventArgs {
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

    public float limit = 5f;
    public float time = 5f;

    private void Awake() {
        OnBulletShoot += ShootBullet_OnBulletShoot;
    }

    private void Update() {

        if (time >= 0f) {
            time -= Time.deltaTime;
            return;
        } else if (time <= 0f) {
            time = limit;
            CallOnBulletShootEvent();
        }
    }

    public void CallOnBulletShootEvent() {
        OnBulletShoot?.Invoke(this, new OnBulletShootEventArgs {
            // Send spell attributes to bullets fired
        
            baseDamage = baseDamage,
            baseCooldown = baseCooldown,
            baseCost = baseCost,
            critChance = critChance,
            critDamage = critDamage,

            crit = crit,

            status = status,
            element = element,
            statusChance = statusChance,
            dps = dps,

            shootDir = shootDir
        });

    }

    public void ShootBullet_OnBulletShoot(object sender, BulletShooter.OnBulletShootEventArgs e) {
        Vector3 originPos = transform.position;
        Vector3 shootPos = GameObject.FindGameObjectWithTag("BulletShooterTarget").transform.position;

        Transform bulletTransform = Instantiate(pfBullet, originPos, Quaternion.identity);

        shootDir = (shootPos - originPos).normalized;

        // Debug.Log("shoot bullet lol");
        
        OnSpellCast?.Invoke(this, new OnSpellCastEventArgs {
            // Send spell attributes to bullets fired
         
            baseDamage = baseDamage,
            baseCooldown = baseCooldown,
            baseCost = baseCost,
            critChance = critChance,
            critDamage = critDamage,

            crit = crit,

            status = status,
            element = element,
            statusChance = statusChance,
            dps = dps,

            shootDir = shootDir
        });
    }
}
