using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manapotion;
using Manapotion.PartySystem;

namespace Manapotion.StatusEffects
{    
    [System.Serializable]
    public class Buff
    {
        public StatusEffect effect;
        public int power = 1;
        public float duration;
        public float time;

        public bool active { get; set; }

        public Buff(StatusEffect effect, int power, float duration)
        {
            this.effect = effect;
            this.power = power;
            this.duration = duration;

            time = duration;
 
            ManaBehaviour.OnUpdate += Update;
        }
        
        ~Buff()
        {
            ManaBehaviour.OnUpdate -= Update;
        }
        
        public void Init(PartyMember member)
        {
            effect.OnStart(member);
        }

        void Update()
        {
            if (!active)
            {
                return;
            }

            time -= Time.deltaTime;
            effect.OnTick(Time.deltaTime);
            if (time <= 0f)
            {
                active = false;
                effect.OnEnd();
            }
        }

        public void ResetTime()
        {
            time = duration;
        }
    }
}

