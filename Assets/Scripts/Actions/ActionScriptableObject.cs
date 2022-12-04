using System;
using System.Collections;
using UnityEngine;
using Manapotion.PartySystem;
using Manapotion.Input;
using Manapotion.Stats;
using Manapotion.Rendering;
using Manapotion.Actions.Projectiles;
using Manapotion.Actions.Targets;

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
        
        public ProjectileHandlerScriptableObject projectileHandler;
        public TargetDefinitionScriptableObject targetDefinition;
        
        /// <summary>
        /// This action's ID.
        /// </summary>
        [Tooltip("The ID/Name of the action.")]
        public ActionID action_id;
        
        /// <summary>
        /// Require the action to be toggled on and off.
        /// </summary>
        [Header("Toggled Action Options")]
        [Tooltip("If true, the action will need to be performed again to turn it off.")]
        public bool isToggled = false;
        private bool _isActive = false;

        [Tooltip("If true, this action will apply Restrictions Applied After Performed Until Driver to the member until driverToWatchToUnrestrictAfterPerformed's callbacks are made.")]
        public bool whenPerformedWillRestrictUntilEvent = false;
        [Tooltip("The name of the driver that when invoked will apply _restrictionsWhileActive to the member that performed this action.")]
        public string driverToWatchToUnrestrictAfterPerformed;
        [SerializeField]
        private CharacterControllerRestriction _restrictionsAppliedAfterPerformedUntilDriver;
        [SerializeField]
        private CharacterControllerRestriction _restrictionsAppliedWhileActive;
        [SerializeField]
        private CharacterControllerRestriction _restrictionsAppliedAfterConcludedUntilDriver;

        [Tooltip("If true, this action will apply Restrictions Applied After Concluded Until Driver to the member until driverToWatchToUnrestrictAfterConcluded's callbacks are made.")]
        public bool whenConcludedWillRestrictUntilEvent = false;
        [Tooltip("The name of the driver that when invoked will unrestrict the member that performed this action.")]
        public string driverToWatchToUnrestrictAfterConcluded;

        [Tooltip("When this action is performed, this action will set Reanimator drivers to those listed here.")]
        public DriverSet[] actionPerformed_driverSetsArray;
        [Tooltip("When this action is concluded, this action will set Reanimator drivers to those listed here.")]
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
                    if (whenPerformedWillRestrictUntilEvent)
                    {
                        member.characterController.characterControllerRestriction = _restrictionsAppliedAfterPerformedUntilDriver;
                        OnActionPerformedRestrictingMovementEvent?.Invoke(
                            this,
                            new OnActionPerformedRestrictingMovementEventArgs
                            {
                                action_id = this.action_id,
                                watchDriver = this.driverToWatchToUnrestrictAfterPerformed,
                                restrictionsToApply = _restrictionsAppliedWhileActive
                            }
                        );
                    }
                }
            }
            member.characterController.characterControllerRestriction = _restrictionsAppliedAfterPerformedUntilDriver;
            InvokeActionPerformedEvent();
            yield break;
        }

        public virtual IEnumerator PerformAction(PartyMember member, DamageInstance.DamageInstanceType type, DamageInstance.DamageInstanceElement element)
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
                    if (whenPerformedWillRestrictUntilEvent)
                    {
                        member.characterController.characterControllerRestriction = _restrictionsAppliedAfterPerformedUntilDriver;
                        OnActionPerformedRestrictingMovementEvent?.Invoke(
                            this,
                            new OnActionPerformedRestrictingMovementEventArgs
                            {
                                action_id = this.action_id,
                                watchDriver = this.driverToWatchToUnrestrictAfterPerformed,
                                restrictionsToApply = _restrictionsAppliedWhileActive
                            }
                        );
                    }
                }
            }
            member.characterController.characterControllerRestriction = _restrictionsAppliedAfterPerformedUntilDriver;
            InvokeActionPerformedEvent();
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
            if (whenConcludedWillRestrictUntilEvent)
            {
                member.characterController.characterControllerRestriction = _restrictionsAppliedAfterPerformedUntilDriver;
                OnActionConcludedRestrictingMovementEvent?.Invoke(
                    this,
                    new OnActionConcludedRestrictingMovementEventArgs
                    {
                        action_id = this.action_id,
                        watchDriver = this.driverToWatchToUnrestrictAfterConcluded,
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