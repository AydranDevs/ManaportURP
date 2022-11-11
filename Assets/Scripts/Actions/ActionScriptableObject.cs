using System;
using System.Collections;
using UnityEngine;
using Manapotion.PartySystem;
using Manapotion.Items;
using Manapotion.Stats;

namespace Manapotion.Actions
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/Actions/New ActionScriptableObject")]
    public class ActionScriptableObject : ScriptableObject
    {
        public event EventHandler<OnActionPerformedEventArgs> OnActionPerformedEvent;
        public class OnActionPerformedEventArgs : EventArgs
        {
            public ActionID action_id;
            public string action_animationName;
            public PointID costPointID;
            public int cost;
        }
        
        
        /// <summary>
        /// This action's ID.
        /// </summary>
        public ActionID action_id;

        /// <summary>
        /// The name of the animation to play when this action is performed.
        /// </summary>
        public string action_animationName;

        [Header("Point Cost")]
        public PointID costPointID;
        public int cost;

        /// <summary>
        /// Perform this action.
        /// </summary>
        /// <param name="member">Member to perform the action</param>
        /// <returns>IEnumerator (Coroutine)</returns>
        public virtual IEnumerator PerformAction(PartyMember member)
        {
            yield break;
        }

        public virtual IEnumerator PerformAction(PartyMember member, DamageInstance.DamageInstanceType type, DamageInstance.DamageInstanceElement element)
        {
            yield break;
        }

        public virtual IEnumerator PerformAction(PartyMember member, Stat stat, DamageInstance.DamageInstanceType type, DamageInstance.DamageInstanceElement element)
        {
            yield break;
        }

        protected void InvokeActionPerformedEvent()
        {
            OnActionPerformedEvent?.Invoke(
                this,
                new OnActionPerformedEventArgs
                {
                    action_id = this.action_id,
                    action_animationName = this.action_animationName,
                    costPointID = this.costPointID,
                    cost = this.cost
                }
            );
        }
    }
}