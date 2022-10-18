using System;
using UnityEngine;

[Serializable]
public struct BuffSpriteIcons {
    public Sprite strengthening;
    public Sprite healing;
    public Sprite swiftening;
    public Sprite defending;
    
    public Sprite pyroWarming;
    public Sprite cryoChilling;
    public Sprite toxiSickening;
    public Sprite voltAmplifying;
}

public abstract class BuffAbility : MonoBehaviour {
    public int cost;
    public float cooldown;
    // public int damage;
    public string spellId;
    public BuffSpriteIcons icons;

    public bool coolingDown = false;
    public float cooldownTime;

    // redefinable function called from derived classes
    public virtual void Cast(string effect) { }
}
