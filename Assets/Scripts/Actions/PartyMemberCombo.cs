using System.Collections;
using System.Collections.Generic;
using Aarthificial.Reanimation;
using Manapotion.PartySystem;
using UnityEngine;
using Manapotion.Stats;

namespace Manapotion.Actions
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/Actions/New Combo")]
    public class PartyMemberCombo : APartyMemberAction
    {
        [Tooltip("The action that is required to be active for this one to be performed.")]
        public APartyMemberAction requiredAction;
        
        public override string GetActionID()
        {
            return this.name;
        }
        
        public StatID modifierStatID;
        public override StatID GetModifierStatID()
        {
            return modifierStatID;
        }
        public List<PartyMemberMeleeAttack> attacksList;
        
        [System.NonSerialized]
        private int currentlyPerformingAttackIndex;
        
        public override IEnumerator PerformAction(PartyMember member, DamageInstance damageInstance = null)
        {
            if (damageInstance == null)
            {
                yield break;
            }

            attacksList[0].PerformAction(member, damageInstance);
            yield break;
        }
    }
}
