using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PartyNamespace;
using PartyNamespace.MirabelleNamespace;
using Manapotion.StatusEffects;

public class ShoweraHealCast : HealingAbility {

    public HealParticles particles;

    private bool active = false;
    private StatusEffect effect;

    private List<Transform> targets;
    public List<Transform> targetsInRange;

    private void Start() {
        targets = new List<Transform>();
        targetsInRange = new List<Transform>();

        MirabelleRenderer.OnOpenUmbrella += UmbrellaOpened;
    }

    private void UmbrellaOpened() {
        active = true;
    }
    
    public override void Cast(string type) {
        if (coolingDown) return;

        var t = GameObject.FindGameObjectsWithTag("PlayerPartyMember"); 
        foreach (var tar in t) {
            targets.Add(tar.transform);
        }

        if (type == "rejuvenating") {
            effect = new RejuvenatedBuff();
        }
    }

    public override void Uncast() {
        active = false;

        targets = new List<Transform>();
        targetsInRange = new List<Transform>();
    }

    private void Update() {
        if (active) { CastUpdate(effect); }
    }

    private void CastUpdate(StatusEffect effect) {
        foreach (var t in targets) {
            if (Vector3.Distance(transform.position, t.position) <= range) {
                if (!targetsInRange.Contains(t)) { targetsInRange.Add(t); }

                t.GetComponent<PartyMember>().AddBuff(effect, 1, 8f);
            }else {
                if (targetsInRange.Contains(t)) { targetsInRange.Remove(t); }
            }
        }
    }
}
