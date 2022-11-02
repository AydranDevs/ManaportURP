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
            HandleAttack(member);
            yield break;
        }
        
        private void HandleAttack(PartyMember member)
        {
            DamageInstance damage = new DamageInstance
            {
                damageInstanceType = DamageInstance.DamageInstanceType.Physical,
                damageInstanceElement = (DamageInstance.DamageInstanceElement)member.primaryActionElement,
                damageInstanceAmount = (float)member.GetInstanceID()
            };
            
            Debug.Log(
                string.Format(
                    "New DamageInstance created (Type: {0}, Element: {1}, Amount: {2}",
                    damage.damageInstanceType,
                    damage.damageInstanceElement,
                    damage.damageInstanceAmount
                )
            );
        }
    }
}