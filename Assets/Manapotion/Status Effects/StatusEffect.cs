using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manapotion.Status;
using PartyNamespace;

namespace Manapotion.StatusEffects {
    /*
    abstract class for Status Effects
    */
    public abstract class StatusEffect {
        public PartyBuffs buffType;
        public List<Stat> statsAffected;

        public abstract void OnStart(PartyMember afflictedMember);
    
        public abstract void OnTick(float deltaTime);
    
        public abstract void OnEnd();
    }
}
