using System;
using UnityEngine;
using PartyNamespace;
using Manapotion.StatusEffects;

[Serializable]
public struct HealSpriteIcons {
    public Sprite rejuvenating;
    public Sprite warming;
    public Sprite comforting;
    public Sprite caring;
    public Sprite loving;
}

[Serializable]
public struct HealParticles {
    public GameObject rejuvenatingBeam;
}

public abstract class HealingAbility : MonoBehaviour {
    public int cost;
    public float delay;
    public float range;
    public float cooldown;
    public StatusEffect buff;
    public float heal;
    public string spellId;
    
    public HealSpriteIcons icons;

    public bool coolingDown = false;
    public float cooldownTime;

    // redefinable function definied in derived classes
    public virtual void Cast(string type) { }

    public virtual void Uncast() { }
}
