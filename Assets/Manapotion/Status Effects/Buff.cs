using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manapotion.ManaBehaviour;
using PartyNamespace;

namespace Manapotion.StatusEffects {
    
    [System.Serializable]
    public class Buff {
        public PartyBuffs buffType;
        public StatusEffect effect;
        public int power = 1;
        public float duration;

        public bool active { get; set; }

        [SerializeField] public float time { get; private set; }

        public Buff(StatusEffect effect, int power, float duration) {
            this.effect = effect;
            this.power = power;
            this.duration = duration;

            time = duration;
 
            Manapotion.ManaBehaviour.ManaBehaviour.OnUpdate += Update;
        }
        
        ~Buff() {
            Manapotion.ManaBehaviour.ManaBehaviour.OnUpdate -= Update;
        }
        
        public void Init(PartyMember member) {
            effect.OnStart(member);
        }

        void Update() {
            if (!active) return;

            time -= Time.deltaTime;
            effect.OnTick(Time.deltaTime);
            if (time <= 0f) {
                active = false;
                effect.OnEnd();
            }
        }
    }
}

