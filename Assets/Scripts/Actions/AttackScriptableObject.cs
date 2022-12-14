using System.Collections;
using UnityEngine;
using Manapotion.PartySystem;
using Manapotion.Stats;

namespace Manapotion.Actions
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/Actions/New AttackScriptableObject")]
    public class AttackScriptableObject : ActionScriptableObject
    {    
        public override IEnumerator PerformAction(PartyMember member, DamageInstance damageInstance = null)
        {
            if (damageInstance == null)
            {
                yield break;
            }
            // check required action to see if this one can be performed
            if (requiredAction != null && !requiredAction.isActive)
            {
                yield break;
            }

            if (!member.pointsManagerScriptableObject.GetPointScriptableObject(costPointID).value.CanSubtract(cost))
            {
                // not enough points to use this attack
                yield break;
            }
            
            InvokeActionPerformedEvent();
            HandleAttack(member, damageInstance);
            yield break;
        }
        
        // handle the attack (spawn projectiles, run animations, etc)
        private void HandleAttack(PartyMember member, DamageInstance damageInstance)
        {
            if (projectileHandler == null)
            {
                // uhh put code here later
                return;
            }

            ManaBehaviour.instance.StartCoroutine(projectileHandler.SpawnProjectile(
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