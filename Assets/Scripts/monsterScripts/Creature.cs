using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : MonoBehaviour {
    public static Creature instance;

    public float health;

    public static List<Creature> creatureList;

    private void Awake() {
        if (creatureList == null) creatureList = new List<Creature>();
        instance = this;
    }

    public virtual void Damage(float damage) { }
    
    public void KillAllCreatures() {
        GameObject[] creatures = GameObject.FindGameObjectsWithTag("Creature");
        foreach(GameObject creature in creatures) GameObject.Destroy(creature);

        Debug.Log("Killed all creatures");
    }
}
