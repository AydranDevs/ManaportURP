using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : MonoBehaviour {
    public float health;

    public virtual void Damage(float damage) { }
}
