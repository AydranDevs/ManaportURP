using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manapotion.PartySystem;
using Manapotion.Items;
using Manapotion.Stats;

namespace Manapotion.Actions
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/Actions/New ActionScriptableObject")]
    public class ActionScriptableObject : ScriptableObject
    {
        public ActionID action_id;
        public string action_animationName;

        [Header("Point Cost")]
        public PointID costPointID;
        public int cost;

        public virtual IEnumerator PerformAction(PartyMember member)
        {
            yield break;
        }

        public virtual IEnumerator PerformAction(PartyMember member, DamageInstance.DamageInstanceType type, DamageInstance.DamageInstanceElement element)
        {
            yield break;
        }

        public virtual IEnumerator PerformAction(PartyMember member, Stat stat, DamageInstance.DamageInstanceType type, DamageInstance.DamageInstanceElement element)
        {
            yield break;
        }
    }
}