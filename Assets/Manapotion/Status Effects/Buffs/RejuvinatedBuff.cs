using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PartyNamespace;
using Manapotion.Status;
using Manapotion.ManaBehaviour;

namespace Manapotion.StatusEffects {
    public class RejuvenatedBuff : StatusEffect {
        public PartyBuffs buff = PartyBuffs.Rejuvenated;
        private float timeMax = 5f;
        private float time;

        private float healthRegen = 2f;
        private GameObject particles;

        public override void OnStart(PartyMember afflictedMember) {
            statsAffected = new List<Stat>();
            statsAffected.Add(afflictedMember.hitPoints);

            particles = afflictedMember.SummonParticles(afflictedMember.buffParticles.rejuvenatedBuffParticles, afflictedMember.transform);
            time = timeMax;
        }

        public override void OnTick(float deltaTime) {
            time = time - deltaTime;
            if (time <= 0f) {
                statsAffected[0].value += healthRegen;
                time = timeMax;
            }
        }
    
        public override void OnEnd() {
            particles.GetComponent<ParticleSystem>().Stop();
            Manapotion.ManaBehaviour.ManaBehaviour.DestroyObject(particles, 6f);
        }
    }
}
