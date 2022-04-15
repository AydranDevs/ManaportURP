using System;
using UnityEngine;

[Serializable]
public struct SpriteIcons {
    public Sprite arcane;
    public Sprite pyro;
    public Sprite cryo;
    public Sprite toxi;
    public Sprite volt;
}

public abstract class Spell : MonoBehaviour {
    public int cost;
    public float cooldown;
    public int damage;
    public string spellId;
    public SpriteIcons icons;

    public bool coolingDown = false;

    // redefinable function called from derived classes
    public virtual void Cast(Vector2 direction, string element) { }
}