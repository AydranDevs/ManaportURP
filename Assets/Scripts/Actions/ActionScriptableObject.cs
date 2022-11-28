using System;
using System.Collections;
using UnityEngine;
using Manapotion.PartySystem;
using Manapotion.Input;
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
        public event EventHandler<OnActionPerformedRestrictingMovementEventArgs> OnActionPerformedRestrictingMovementEvent;
        public class OnActionPerformedRestrictingMovementEventArgs : EventArgs
        {
            public ActionID action_id;
            public string watchDriver;
            public CharacterControllerRestriction restrictionsToApply;
        }

        public event EventHandler<OnActionConcludedEventArgs> OnActionConcludedEvent;
        public class OnActionConcludedEventArgs : EventArgs
        {
            public ActionID action_id;
            public DriverSet[] actionConcluded_driverSetsArray;
        }
        public event EventHandler<OnActionConcludedRestrictingMovementEventArgs> OnActionConcludedRestrictingMovementEvent;
        public class OnActionConcludedRestrictingMovementEventArgs : EventArgs
        {
            public ActionID action_id;
            public string watchDriver;
            public CharacterControllerRestriction restrictionsToApply;
        }
        
        /// <summary>
        /// This action's ID.
        /// </summary>
        public ActionID action_id;
        
        /// <summary>
        /// Require the action to be toggled on and off.
        /// </summary>
        [Header("Toggled Action Options")]
        public bool isToggled = false;
        private bool _isActive = false;
        public bool performed_willRestrictMovementUntilDriverEvent = false;
        public string performed_unrestrictDriverEventWatch;
        [SerializeField]
        private CharacterControllerRestriction _restrictionsUntilDriverEvent;
        [SerializeField]
        private CharacterControllerRestriction _restrictionsWhileActive;

        public bool concluded_willRestrictMovementUntilDriverEvent = false;
        public string concluded_unrestrictDriverEventWatch;

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
                    if (performed_willRestrictMovementUntilDriverEvent)
                    {
                        member.characterController.characterControllerRestriction = _restrictionsUntilDriverEvent;
                        OnActionPerformedRestrictingMovementEvent?.Invoke(
                            this,
                            new OnActionPerformedRestrictingMovementEventArgs
                            {
                                action_id = this.action_id,
                                watchDriver = this.performed_unrestrictDriverEventWatch,
                                restrictionsToApply = _restrictionsWhileActive
                            }
                        );
                    }
                }
            }
            member.characterController.characterControllerRestriction = _restrictionsUntilDriverEvent;
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
            _isActive = false;
            if (concluded_willRestrictMovementUntilDriverEvent)
            {
                member.characterController.characterControllerRestriction = _restrictionsUntilDriverEvent;
                OnActionConcludedRestrictingMovementEvent?.Invoke(
                    this,
                    new OnActionConcludedRestrictingMovementEventArgs
                    {
                        action_id = this.action_id,
                        watchDriver = this.concluded_unrestrictDriverEventWatch,
                        restrictionsToApply = CharacterControllerRestriction.NoRestrictions
                    }
                );
            }
            else
            {
                member.characterController.characterControllerRestriction = CharacterControllerRestriction.NoRestrictions;
            }
            InvokeActionConcludedEvent();
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