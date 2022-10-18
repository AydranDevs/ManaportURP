using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manapotion;
using Manapotion.PartySystem;
using Manapotion.Status;

namespace Manapotion.StatusEffects
{
    public class RejuvenatedBuff : StatusEffect
    {
        private float timeMax = 5f;
        private float time;

        private float healthRegen = 2f;
        private GameObject particles;
        private PartyMember _afflictedMember;

        public RejuvenatedBuff()
        {
            buffType = PartyBuffs.Rejuvenated;
        }

        public override void OnStart(PartyMember afflictedMember)
        {
            _afflictedMember = afflictedMember;
            statsAffected = new List<Stat>();
            statsAffected.Add(afflictedMember.stats.hitPoints);

            _afflictedMember.SummonParticles(_afflictedMember.statusEffectParticles.rejuvenatedBuffParticles);
            time = timeMax;

            Debug.Log(afflictedMember);
        }

        public override void OnTick(float deltaTime)
        {
            time = time - deltaTime;
            if (time <= 0f)
            {
                statsAffected[0].value += healthRegen;
                time = timeMax;
            }
        }
    
        public override void OnEnd()
        {
            _afflictedMember.StopParticles(_afflictedMember.statusEffectParticles.rejuvenatedBuffParticles);
        }
    }
}
