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
        
        [Tooltip("The action that is required to be active for this one to be performed.")]
        public ActionScriptableObject requiredAction;
        public ProjectileHandlerScriptableObject projectileHandler;
        public TargetDefinition targetDefinition;
        
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
        [Tooltip("If this and isToggled are true, this action will make the character begin and stop targeting.")]
        public bool startsTargeting = false;
        public bool isActive { get; private set; } = false;

        [Tooltip("If true, this action will apply Restrictions Applied After Performed Until Driver to the member until driverToWatchToUnrestrictAfterPerformed's callbacks are made.")]
        public bool whenPerformedWillRestrictUntilEvent = false;
        [Tooltip("The name of the driver that when invoked will apply _restrictionsWhileActive to the member that performed this action.")]
        public string driverToWatchToUnrestrictAfterPerformed;
        [SerializeField]
        private CharacterControllerRestriction _restrictionsAppliedAfterPerformedUntilDriver = CharacterControllerRestriction.NoRestrictions;
        [SerializeField]
        private CharacterControllerRestriction _restrictionsAppliedWhileActive = CharacterControllerRestriction.NoRestrictions;
        [SerializeField]
        private CharacterControllerRestriction _restrictionsAppliedAfterConcludedUntilDriver = CharacterControllerRestriction.NoRestrictions;

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

        [Tooltip("The stat whose modified value will be used when calculating the damage this action with inflict.")]
        public StatID modifierStatID;

        /// <summary>
        /// Perform this action.
        /// </summary>
        /// <param name="member">Member to perform the action</param>
        /// <returns>IEnumerator (Coroutine)</returns>
        public virtual IEnumerator PerformAction(PartyMember member)
        {
            // check required action to see if this one can be performed
            if (requiredAction != null && !requiredAction.isActive)
            {
                yield break;
            }

            if (isToggled)
            {
                if (isActive)
                {
                    ConcludeAction(member);
                    yield break;
                }
                else
                {
                    isActive = true;
                    if (startsTargeting)
                    {
                        member.characterTargeting.isTargeting = true;
                    }
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
            
            foreach (var target in targetDefinition.SelectTarget(member))
            {
                Debug.Log(target);
            }
            member.characterController.characterControllerRestriction = _restrictionsAppliedAfterPerformedUntilDriver;
            InvokeActionPerformedEvent();
            yield break;
        }

        /// <summary>
        /// Perform this action.
        /// </summary>
        /// <param name="member">Member to perform the action</param>
        /// <param name="type">Type of damage this action will inflict</param>
        /// <param name="element">Element type that this action will use</param>
        /// <returns>IEnumerator (Coroutine)</returns>
        public virtual IEnumerator PerformAction(PartyMember member, DamageInstance.DamageInstanceType type, DamageInstance.DamageInstanceElement element)
        {
            // check required action to see if this one can be performed
            if (requiredAction != null && !requiredAction.isActive)
            {
                yield break;
            }

            if (isToggled)
            {
                if (isActive)
                {
                    ConcludeAction(member);
                    yield break;
                }
                else
                {
                    isActive = true;
                    if (startsTargeting)
                    {
                        member.characterTargeting.isTargeting = true;
                    }
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
            
            foreach (var target in targetDefinition.SelectTarget(member))
            {
                Debug.Log(target);
            }
            member.characterController.characterControllerRestriction = _restrictionsAppliedAfterPerformedUntilDriver;
            InvokeActionPerformedEvent();
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
            if (!isActive)
            {
                Debug.LogWarning("Cannot conclude an action that hasn't been performed yet.");
                return;
            }
            isActive = false;

            if (startsTargeting)
            {
                member.characterTargeting.isTargeting = false;
            }
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