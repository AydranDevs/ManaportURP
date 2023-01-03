using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manapotion.PartySystem;
using Manapotion.Stats;
using Manapotion.Actions.Projectiles;
using Manapotion.Animation;
using Aarthificial.Reanimation.Nodes;

namespace Manapotion.Actions
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/Actions/New Projectile Attack")]
    public class PartyMemberProjectileAttack : APartyMemberAction
    {   
        public enum AttackMode
        {
            Standalone,
            Combo_Attack
        }
        public AttackMode attackMode = AttackMode.Standalone;
        public PartyMemberCombo comboParent;

        public override string GetActionID()
        {
            return this.name;
        }
        
        [Tooltip("The action that is required to be active for this one to be performed.")]
        public PartyMemberAction requiredAction;
        public ProjectileHandler projectileHandler;

        public PointID costPointID;
        public int cost;

        [Tooltip("The stat whose modified value will be used when calculating the damage this action with inflict.")]
        public StatID modifierStatID;
        public override StatID GetModifierStatID()
        {
            return modifierStatID;
        }

        public List<DriverHandle> driverHandles;

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
            
            HandleAttack(member, damageInstance);
        }
        
        // handle the attack (spawn projectiles, run animations, etc)
        private void HandleAttack(PartyMember member, DamageInstance damageInstance)
        {
            if (projectileHandler == null)
            {
                throw new System.Exception($"Action {name} does not contain a ProjectileHandler! Please assign one to the object.");
            }

            member.StartCoroutine(projectileHandler.SpawnProjectile(
                member,
                new DamageInstance
                {
                    damageInstanceType = damageInstance.damageInstanceType,
                    damageInstanceElement = damageInstance.damageInstanceElement,
                    damageInstanceAmount = member.statsManagerScriptableObject.GetStat(modifierStatID).value.modifiedValue
                }
            ));
        }
    }
}