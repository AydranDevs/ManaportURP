using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SpellInfo {
    public string type;
    public string element;
}

public struct SpellStats {
    public float damage;
    public int cooldown;
    public int cost;
}

public abstract class Spell : MonoBehaviour {
    public SpellInfo spellInfo;
    public SpellStats spellStats;

    // public virtual void Cast(Vector2 direction, string element) { }

    public virtual void SetStats(bool isPrimary) { }
}
