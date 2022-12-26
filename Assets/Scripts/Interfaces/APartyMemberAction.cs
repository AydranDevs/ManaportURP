using Manapotion.PartySystem;
using System.Collections;
using UnityEngine;
using Manapotion.Stats;

namespace Manapotion.Actions
{
    public abstract class APartyMemberAction : ScriptableObject
    {
        public abstract string GetActionID();
        public abstract StatID GetModifierStatID();

        /// <summary>
        /// [COROUTINE] Perform this action.
        /// </summary>
        /// <param name="member">the PartyMember to perform this action.</param>
        /// <param name="damageInstance">[OPTIONAL] the DamageInstance to apply to the target.</param>
        /// <returns></returns>
        public abstract IEnumerator PerformAction(PartyMember member, DamageInstance damageInstance = null);
    }
}
