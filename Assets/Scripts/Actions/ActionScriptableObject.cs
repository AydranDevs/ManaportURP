using System;
using System.Collections;
using UnityEngine;
using Manapotion.PartySystem;
using Manapotion.Items;
using Manapotion.Stats;
using Manapotion.Rendering;

namespace Manapotion.Actions
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/Actions/New ActionScriptableObject")]
    public class ActionScriptableObject : ScriptableObject
    {
        public event EventHandler<OnActionPerformedEventArgs> OnActionPerformedEvent;
        public class OnActionPerformedEventArgs : EventArgs
        {
            public ActionID action_id;
            public DriverSet[] actionPerformed_driverSetsArray;
            public PointID costPointID;
            public int cost;
        }
        public event EventHandler<OnActionConcludedEventArgs> OnActionConcludedEvent;
        public class OnActionConcludedEventArgs : EventArgs
        {
            public ActionID action_id;
            public DriverSet[] actionConcluded_driverSetsArray;
        }
        
        /// <summary>
        /// This action's ID.
        /// </summary>
        public ActionID action_id;
        
        /// <summary>
        /// Is the action toggled?
        /// </summary>
        public bool isToggled;
        private bool _isActive = false;

        public DriverSet[] actionPerformed_driverSetsArray;
        public DriverSet[] actionConcluded_driverSetsArray;

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
            if (isToggled)
            {
                if (_isActive)
                {
                    ConcludeAction(member);
                    yield break;
                }
                else
                {
                    _isActive = true;
                }
            }
            Debug.Log($"Action {this.action_id} started. (member: {member})");
            InvokeActionPerformedEvent();
            yield break;
        }

        public virtual IEnumerator PerformAction(PartyMember member, DamageInstance.DamageInstanceType type, DamageInstance.DamageInstanceElement element)
        {
            Debug.Log($"Action {this.action_id} started. (member: {member})");
            yield break;
        }

        public virtual IEnumerator PerformAction(PartyMember member, Stat stat, DamageInstance.DamageInstanceType type, DamageInstance.DamageInstanceElement element)
        {
            Debug.Log($"Action {this.action_id} started. (member: {member})");
            yield break;
        }

        protected void InvokeActionPerformedEvent()
        {
            OnActionPerformedEvent?.Invoke(
                this,
                new OnActionPerformedEventArgs
                {
                    action_id = this.action_id,
                    actionPerformed_driverSetsArray = this.actionPerformed_driverSetsArray,
                    costPointID = this.costPointID,
                    cost = this.cost
                }
            );
        }

        private void ConcludeAction(PartyMember member)
        {
            if (!_isActive)
            {
                Debug.LogWarning("Cannot conclude an action that hasn't been performed yet.");
                return;
            }
            Debug.Log($"Action {this.action_id} concluded. (member: {member})");
            InvokeActionConcludedEvent();
            _isActive = false;
        }

        private void InvokeActionConcludedEvent()
        {
            OnActionConcludedEvent?.Invoke(
                this,
                new OnActionConcludedEventArgs
                {
                    action_id = this.action_id,
                    actionConcluded_driverSetsArray = this.actionConcluded_driverSetsArray
                }
            );
        }
    }
}