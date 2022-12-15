using System.Collections;
using Manapotion.PartySystem;
using UnityEngine;
using Manapotion.Actions.Targets;

namespace Manapotion.Actions
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/Actions/New LockOnScriptableObject")]
    public class LockOnScriptableObject : ActionScriptableObject
    {
        public override IEnumerator PerformAction(PartyMember member, DamageInstance damageInstance = null)
        {
            InvokeActionPerformedEvent();
            HandleTargeting(member);
            yield break;
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
    }
}