using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PartyNamespace;
using PartyNamespace.MirabelleNamespace;
using Manapotion.StatusEffects;

public class ShowerHealCast : HealingAbility {

    public HealParticles particles;

    private bool active = false;
    private StatusEffect effect;

    private List<PartyMember> targets;

    private void Start() {
        targets = new List<PartyMember>();
        foreach (var m in Party.Instance.members) {
            targets.Add(m.GetComponent<PartyMember>());
        }

        MirabelleRenderer.OnOpenUmbrella += UmbrellaOpened;
    }

    private void UmbrellaOpened() {
        active = true;
    }
    
    public override void Cast(string type) {
        if (coolingDown) return;

        if (type == "rejuvenating") {
            effect = new RejuvenatedBuff();
        }
    }

    public override void Uncast() {
        active = false;
    }

    private void Update() {
        if (active) { CastUpdate(effect); }
    }

    private void CastUpdate(StatusEffect effect) {
        foreach (var t in targets) {
            if (Vector3.Distance(transform.position, t.transform.position) <= range) {
                t.GetComponent<PartyMember>().AddStatusEffect(effect, 1, 8f);
            }
        }
    }
}
