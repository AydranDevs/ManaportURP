using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Dummy : MonoBehaviour {

    public float hitPoints = 100;

    private static List<Dummy> dummyList;

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

    public static Dummy GetClosest(Vector3 pos, float maxRange) {
        Dummy closest = null;
        foreach (Dummy dummy in dummyList) {
            if (Vector3.Distance(pos, dummy.GetPosition()) <= maxRange) {
                if (closest == null) {
                    closest = dummy;
                }else {
                    if (Vector3.Distance(pos, dummy.GetPosition()) < Vector3.Distance(pos, closest.GetPosition())) {
                        closest = dummy;
                    }
                }
            }
        }
        return closest;
    }
    
    private void Awake() {
        if (dummyList == null) dummyList = new List<Dummy>();
        dummyList.Add(this);

    }

    // When this dummy takes damage, this function is called.
    // it brings in baseDamage, critDamage, statusType and dps 
    // from the bullet that hit it.

    public void Damage(float damage, float critDamage, bool status, string statusType, float dps) {

        // Logs the bullet stats on hit.

       Debug.Log("Damage: " + damage + " | Crit Damage: " + critDamage + " | Status? " + status + " | Element: " + statusType + " | Damage/sec: " + dps); 

       // Subtracts the bullet damage from hp.

       hitPoints -= damage;

       // Logs final hp calc.

       Debug.Log("Health: " + hitPoints);

       HandleParticles(statusType);
    }

    private void HandleParticles(string statusType) {
        if (statusType == "Arcane") { 
            Transform dust = Instantiate(pfDust, GetPosition(), Quaternion.identity);
        }else if (statusType == "Pyro") {
            Transform fire = Instantiate(pfFire, GetPosition(), new Quaternion(-1f, 0f, 0f, 1f));
        }else if (statusType == "Cryo") {
            Transform ice = Instantiate(pfIce, GetPosition(), Quaternion.identity);
        }else if (statusType == "Bolt") {
            Transform lightning = Instantiate(pfLightning, GetPosition(), Quaternion.identity);
        }else if (statusType == "Toxi") {
            Transform toxic = Instantiate(pfToxic, GetPosition(), Quaternion.identity);
        }
        
    }

    public Vector3 GetPosition() {
        return transform.position;
    }



















    // Deez nuts
}
