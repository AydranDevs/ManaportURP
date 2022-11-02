using System.Collections;
using UnityEngine;
using Manapotion.PartySystem;


namespace Manapotion.Actions
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/AttackScriptableObject")]
    public class AttackScriptableObject : ActionScriptableObject
    {
        public override IEnumerator PerformAction(PartyMember member)
        {
            HandleAttack();
            return null;
        }
        
        private IEnumerator HandleAttack()
        {
            return null;
        }
    }
}