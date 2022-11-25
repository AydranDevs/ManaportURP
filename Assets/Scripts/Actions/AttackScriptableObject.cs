using System.Collections;
using UnityEngine;
using Manapotion.PartySystem;
using Manapotion.Stats;
using Manapotion.Actions.Projectiles;

namespace Manapotion.Actions
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/Actions/New AttackScriptableObject")]
    public class AttackScriptableObject : ActionScriptableObject
    {    
        [SerializeField]
        public ProjectileHandlerScriptableObject projectileHandler;
        
        public override IEnumerator PerformAction(PartyMember member, Stat stat, DamageInstance.DamageInstanceType type, DamageInstance.DamageInstanceElement element)
        {
            if (!member.pointsManagerScriptableObject.GetPointScriptableObject(costPointID).value.CanSubtract(cost))
            {
                // not enough points to use this attack
                yield break;
            }

            
            InvokeActionPerformedEvent();
            Debug.Log($"Attack {this.action_id} started. (member: {member})");
            HandleAttack(member, stat, type, element);
            yield break;
        }
        
        // handle the attack (spawn projectiles, run animations, etc)
        private void HandleAttack(PartyMember member, Stat stat, DamageInstance.DamageInstanceType type, DamageInstance.DamageInstanceElement element)
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
                    damageInstanceType = type,
                    damageInstanceElement = element,
                    damageInstanceAmount = (float)stat.value.modifiedValue
                }
            ));
        }
    }
}