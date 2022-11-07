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
            Debug.Log("Attack '" + action_id + "' started.");
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

            // Transform projectile = Instantiate(PREFAB_projectile, member.transform.position, Quaternion.identity);
            // projectile.GetComponent<ProjectileInstance>().Setup(
            //     ((Vector3)member.inputProvider.GetState().targetPos - member.transform.position).normalized,
            //     new DamageInstance
            //     {
            //         damageInstanceType = type,
            //         damageInstanceElement = element,
            //         damageInstanceAmount = (float)stat.value.modifiedValue
            //     }
            // );
        }
    }
}