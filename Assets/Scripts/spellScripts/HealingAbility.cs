using System;
using UnityEngine;

[Serializable]
public struct HealSpriteIcons {
    public Sprite rejuvenating;
    public Sprite warming;
    public Sprite comforting;
    public Sprite caring;
    public Sprite loving;
}

public abstract class HealingAbility : MonoBehaviour {
    public int cost;
    public float delay;
    public float cooldown;
    // public int damage;
    public string spellId;
    public HealSpriteIcons icons;

    public bool coolingDown = false;
    public float cooldownTime;

    // redefinable function called from derived classes
    public virtual void Cast(Vector2 direction, string element) { }
}
