using System;
using UnityEngine;

[Serializable]
public struct SpellSpriteIcons
{
    public Sprite arcane;
    public Sprite pyro;
    public Sprite cryo;
    public Sprite toxi;
    public Sprite volt;
}

public abstract class Spell : MonoBehaviour
{
    public int cost;
    public float cooldown;
    public int damage;
    public string spellId;
    public SpellSpriteIcons icons;
    public Sprite icon;

    public bool coolingDown = false;
    public float cooldownTime;

    // redefinable function called from derived classes
    public virtual void Cast(Vector2 direction, string element) { }
}