using System.Collections;
using System.Collections.Generic;
using Manapotion.PartySystem;
using UnityEngine;
using Manapotion.Actions.Targets;
using Manapotion.Stats;
using Manapotion.Animation;
using Manapotion.Input;

namespace Manapotion.Actions
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/Actions/New LockOn")]
    public class PartyMemberLockOn : APartyMemberAction
    {
        public override string GetActionID()
        {
            return this.name;
        }

        public StatID modifierStatID;
        public override StatID GetModifierStatID()
        {
            return modifierStatID;
        }

        public List<DriverHandle> performedDriverHandles;
        public List<DriverHandle> concludedDriverHandles;

        public CharacterControllerRestriction performedRestriction;
        public CharacterControllerRestriction activeRestriction;
        
        [System.NonSerialized]
        private bool _isActive = false;

        public override IEnumerator PerformAction(PartyMember member, DamageInstance damageInstance = null)
        {
            if (_isActive)
            {
                ConcludeAction(member);
                yield break;
            }

            _isActive = true;
            HandleTargeting(member);
            HandleAnimation(member, 0);
            yield break;
        }

        private void ConcludeAction(PartyMember member)
        {
            _isActive = false;
            member.characterTargeting.DropCurrentlyTargeted();
            HandleAnimation(member, 1);
        }

        public void HandleTargeting(PartyMember member)
        {
            var collider2Ds = Physics2D.OverlapCircleAll(
                member.transform.position,
                member.characterTargeting.lockOnRange
            );
            
            // the *closest* target in this list will be selected every time until there is an ability to switch targets.
            // so come back to this later on.
            ITargetable closestTarget = null;
            ITargetable currentlyAnalyzedTarget = null;
            for (int i = 0; i < collider2Ds.Length; i++)
            {
                if (!collider2Ds[i].TryGetComponent<ITargetable>(out ITargetable target))
                {
                    continue;
                }

                currentlyAnalyzedTarget = target;
                if (closestTarget == null)
                {
                    closestTarget = target;
                    continue;
                }

                closestTarget.GetPosition(out Vector2 currentClosestTargetPos);
                currentlyAnalyzedTarget.GetPosition(out Vector2 currentlyAnalyzedtargetPos);

                var closestTargetDist = Vector2.Distance(member.transform.position, currentClosestTargetPos);
                var currentlyAnalyzedTargetDist = Vector2.Distance(member.transform.position, currentlyAnalyzedtargetPos);

                if (currentlyAnalyzedTargetDist > closestTargetDist)
                {
                    continue;
                }

                closestTarget = currentlyAnalyzedTarget;
            }
            
            member.characterTargeting?.SetTarget(closestTarget);
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
                if (concludedDriverHandles.Count < 1)
                {
                    return;
                }

                for (int i = 0; i < concludedDriverHandles.Count; i++)
                {
                    var driverHandle = concludedDriverHandles[i];
                    
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
    }
}