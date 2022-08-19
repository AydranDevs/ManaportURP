using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manapotion;
using Manapotion.PartySystem;
using Manapotion.Status;

namespace Manapotion.StatusEffects {
    public class ComfortableBuff : StatusEffect {
        public PartyBuffs buff = PartyBuffs.Comfortable;
        private float timeMax = 5f;
        private float time;

        private Stat _attackDamage;
        private Stat _attackSpeed;
        private float baseAttackDamage;
        private float baseAttackSpeed;

        public override void OnStart(PartyMember afflictedMember) {
            statsAffected = new List<Stat>();
            statsAffected.Add(afflictedMember.stats.attackDamage);
            statsAffected.Add(afflictedMember.stats.attackSpeed);
            
            _attackDamage = statsAffected[0];
            _attackSpeed = statsAffected[1];
            baseAttackDamage = _attackDamage.value;
            baseAttackSpeed = _attackSpeed.value;
        }

        public override void OnTick(float deltaTime) {

        }

        public override void OnEnd() {
            
        }
    }
}
