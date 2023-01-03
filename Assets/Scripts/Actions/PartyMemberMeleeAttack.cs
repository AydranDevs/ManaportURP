using System;
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
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/Actions/New Melee Attack")]
    public class PartyMemberMeleeAttack : APartyMemberAction
    {   
        public event EventHandler<OnMeleeAttackEndedArgs> OnMeleeAttackEnded;
        public class OnMeleeAttackEndedArgs : EventArgs
        {
            public PartyMember member;
        }

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

        public PointID costPointID;
        public int cost;

        [Tooltip("The stat whose modified value will be used when calculating the damage this action with inflict.")]
        public StatID modifierStatID;
        public override StatID GetModifierStatID()
        {
            return modifierStatID;
        }

        public List<DriverHandle> performedDriverHandles;
        public List<DriverHandle> endDriverHandles;

        public DriverHandle attackEndDriverHandle;

        [NonSerialized]
        public bool isActive = false;

        public override IEnumerator PerformAction(PartyMember member, DamageInstance damageInstance = null)
        {
            if (comboParent == null && damageInstance == null)
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
            if (isActive)
            {
                return;
            }
            isActive = true;
            HandleAnimation(member, 0);

            // end attack when attackEndDriverHandle is passed in the Reanimator current state
            member.characterRenderer.GetReanimator().AddListener(attackEndDriverHandle.driverName, () => {
                if (member.characterRenderer.GetReanimator().State.Get(attackEndDriverHandle.driverName) == attackEndDriverHandle.driverValue)
                {
                    EndAttack(member);
                }
            });
        }

        public void HandleAnimation(PartyMember member, int mode)
        {
            if (mode == 0)
            {
                if (performedDriverHandles.Count < 1)
                {
                    return;
                }

                for (int i = 0; i < performedDriverHandles.Count; i++)
                {
                    var driverHandle = performedDriverHandles[i];
                    
                    if (driverHandle.conditional)
                    {
                        member.characterRenderer.SetDriverConditional(
                            driverHandle.driverName,
                            driverHandle.driverValue,
                            driverHandle.conditionalDriverName,
                            driverHandle.conditionalDriverValue
                        );
                    }
                    else
                    {
                        member.characterRenderer.SetDriver(
                            driverHandle.driverName,
                            driverHandle.driverValue
                        );
                    }
                }
            }
            else
            {
                if (endDriverHandles.Count < 1)
                {
                    return;
                }

                for (int i = 0; i < endDriverHandles.Count; i++)
                {
                    var driverHandle = endDriverHandles[i];
                    
                    if (driverHandle.conditional)
                    {
                        member.characterRenderer.SetDriverConditional(
                            driverHandle.driverName,
                            driverHandle.driverValue,
                            driverHandle.conditionalDriverName,
                            driverHandle.conditionalDriverValue
                        );
                    }
                    else
                    {
                        member.characterRenderer.SetDriver(
                            driverHandle.driverName,
                            driverHandle.driverValue
                        );
                    }
                }
            }
        }
    
        private void EndAttack(PartyMember member)
        {
            if (!isActive)
            {
                return;
            }
            isActive = false;
            HandleAnimation(member, 1);
            OnMeleeAttackEnded?.Invoke(this, new OnMeleeAttackEndedArgs {member = member});
        }   
    }
}