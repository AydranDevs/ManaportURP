using System.Collections;
using Manapotion.PartySystem;
using UnityEngine;
using Manapotion.Actions.Targets;

namespace Manapotion.Actions
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/Actions/New LockOnScriptableObject")]
    public class LockOnScriptableObject : ActionScriptableObject
    {
        [Tooltip("The range in units that this action searches within for targets.")]
        public float lockOnRange;

        public override IEnumerator PerformAction(PartyMember member, DamageInstance damageInstance = null)
        {
            InvokeActionPerformedEvent();
            HandleTargeting(member);
            yield break;
        }

        public void HandleTargeting(PartyMember member)
        {
            var collider2Ds = Physics2D.OverlapCircleAll(
                (Vector2)member.transform.position,
                lockOnRange
            );
            
            // the first target in this list will be selected every time until there is an ability to switch targets.
            // so come back to this later on.
            for (int i = 0; i < collider2Ds.Length; i++)
            {
                if (collider2Ds[i].TryGetComponent<ITargetable>(out ITargetable target))
                {
                    member.characterTargeting?.SetTarget(target);
                    break;
                }
            }
        }
    }
}