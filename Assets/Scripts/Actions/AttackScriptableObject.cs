using System.Collections;
using UnityEngine;
using Manapotion.PartySystem;
using Manapotion.Stats;

namespace Manapotion.Actions
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/Actions/New AttackScriptableObject")]
    public class AttackScriptableObject : ActionScriptableObject
    {    
        protected override void HandlePerformAction(PartyMember member, DamageInstance damageInstance = null)
        {
            if (damageInstance == null)
            {
                return;
            }
            // check required action to see if this one can be performed
            if (requiredAction != null && !requiredAction.isActive)
            {
                return;
            }

            if (!member.pointsManagerScriptableObject.GetPointScriptableObject(costPointID).value.CanSubtract(cost))
            {
                // not enough points to use this attack
                return;
            }
            
            HandleAttack(member, damageInstance);
        }
        
        // handle the attack (spawn projectiles, run animations, etc)
        private void HandleAttack(PartyMember member, DamageInstance damageInstance)
        {
            if (projectileHandler == null)
            {
                // uhh put code here later
                return;
            }

            member.StartCoroutine(projectileHandler.SpawnProjectile(
                member,
                new DamageInstance
                {
                    damageInstanceType = damageInstance.damageInstanceType,
                    damageInstanceElement = damageInstance.damageInstanceElement,
                    damageInstanceAmount = (float)member.statsManagerScriptableObject.GetStat(modifierStatID).value.modifiedValue
                }
            ));
        }
    }
}