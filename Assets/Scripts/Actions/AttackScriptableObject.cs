using System.Collections;
using UnityEngine;
using Manapotion.PartySystem;


namespace Manapotion.Actions
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/AttackScriptableObject")]
    public class AttackScriptableObject : ActionScriptableObject
    {    
        public override IEnumerator PerformAction(PartyMember member, DamageInstance.DamageInstanceType type, DamageInstance.DamageInstanceElement element)
        {
            Debug.Log("Attack '" + action_id + "' started.");
            HandleAttack(member, type, element);
            yield break;
        }
        
        private void HandleAttack(PartyMember member, DamageInstance.DamageInstanceType type, DamageInstance.DamageInstanceElement element)
        {
            DamageInstance damage = new DamageInstance
            {
                damageInstanceType = type,
                damageInstanceElement = element,
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