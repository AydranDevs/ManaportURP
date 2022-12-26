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
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/Actions/New Action")]
    public class PartyMemberAction : APartyMemberAction
    {
        public event EventHandler<OnActionPerformedEventArgs> OnActionPerformedEvent;
        public class OnActionPerformedEventArgs : EventArgs
        {
            public string action_id;
            public DriverSet[] actionPerformed_driverSetsArray;
            public PointID costPointID;
            public int cost;
        }
        public event EventHandler<OnActionPerformedRestrictingMovementEventArgs> OnActionPerformedRestrictingMovementEvent;
        public class OnActionPerformedRestrictingMovementEventArgs : EventArgs
        {
            public string action_id;
            public string watchDriver;
            public CharacterControllerRestriction restrictionsToApply;
        }

        public event EventHandler<OnActionConcludedEventArgs> OnActionConcludedEvent;
        public class OnActionConcludedEventArgs : EventArgs
        {
            public string action_id;
            public DriverSet[] actionConcluded_driverSetsArray;
        }
        public event EventHandler<OnActionConcludedRestrictingMovementEventArgs> OnActionConcludedRestrictingMovementEvent;
        public class OnActionConcludedRestrictingMovementEventArgs : EventArgs
        {
            public string action_id;
            public string watchDriver;
            public CharacterControllerRestriction restrictionsToApply;
        }
        
        [Tooltip("The action that is required to be active for this one to be performed.")]
        public PartyMemberAction requiredAction;
        public ProjectileHandler projectileHandler;
        public TargetDefinition targetDefinition;
        
        public override string GetActionID()
        {
            return this.name;
        }
        
        /// <summary>
        /// Require the action to be toggled on and off.
        /// </summary>
        [Header("Toggled Action Options")]
        [Tooltip("If true, the action will need to be performed again to turn it off.")]
        public bool isToggled = false;
        public bool isActive { get; protected set; } = false;

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
        public override StatID GetModifierStatID()
        {
            return modifierStatID;
        }

        /// <summary>
        /// Perform this action.
        /// </summary>
        /// <param name="member">Member to perform the action</param>
        /// <param name="type">Type of damage this action will inflict</param>
        /// <param name="element">Element type that this action will use</param>
        /// <returns>IEnumerator (Coroutine)</returns>
        public override IEnumerator PerformAction(PartyMember member, DamageInstance damageInstance = null)
        {
            // check required action to see if this one can be performed
            if (requiredAction != null && !requiredAction.isActive)
            {
                yield break;
            }

            if (!isToggled)
            {
                HandlePerformAction(member, damageInstance);
                InvokeActionPerformedEvent();
                yield break;
            }
            if (isActive)
            {
                ConcludeAction(member);
                yield break;
            }
            
            isActive = true;
            HandlePerformAction(member, damageInstance);
            InvokeActionPerformedEvent();
            if (whenPerformedWillRestrictUntilEvent)
            {
                member.characterController.characterControllerRestriction = _restrictionsAppliedAfterPerformedUntilDriver;
                OnActionPerformedRestrictingMovementEvent?.Invoke(
                    this,
                    new OnActionPerformedRestrictingMovementEventArgs
                    {
                        action_id = this.GetActionID(),
                        watchDriver = this.driverToWatchToUnrestrictAfterPerformed,
                        restrictionsToApply = _restrictionsAppliedWhileActive
                    }
                );
            }

            member.characterController.characterControllerRestriction = _restrictionsAppliedAfterPerformedUntilDriver;
            yield break;
        }

        private void HandlePerformAction(PartyMember member, DamageInstance damageInstance = null)
        {
            
        }

        protected void InvokeActionPerformedEvent()
        {
            OnActionPerformedEvent?.Invoke(
                this,
                new OnActionPerformedEventArgs
                {
                    action_id = this.GetActionID(),
                    actionPerformed_driverSetsArray = this.actionPerformed_driverSetsArray,
                    costPointID = this.costPointID,
                    cost = this.cost
                }
            );
        }

        protected void ConcludeAction(PartyMember member)
        {
            if (!isActive)
            {
                Debug.LogWarning("Cannot conclude an action that hasn't been performed yet.");
                return;
            }

            isActive = false;
            if (whenConcludedWillRestrictUntilEvent)
            {
                member.characterController.characterControllerRestriction = _restrictionsAppliedAfterPerformedUntilDriver;
                OnActionConcludedRestrictingMovementEvent?.Invoke(
                    this,
                    new OnActionConcludedRestrictingMovementEventArgs
                    {
                        action_id = this.GetActionID(),
                        watchDriver = this.driverToWatchToUnrestrictAfterConcluded,
                        restrictionsToApply = CharacterControllerRestriction.NoRestrictions
                    }
                );
            }
            else
            {
                member.characterController.characterControllerRestriction = CharacterControllerRestriction.NoRestrictions;
            }
            HandleConcludeAction(member);
            InvokeActionConcludedEvent();
        }

        private void HandleConcludeAction(PartyMember member)
        {

        }

        private void InvokeActionConcludedEvent()
        {
            OnActionConcludedEvent?.Invoke(
                this,
                new OnActionConcludedEventArgs
                {
                    action_id = this.GetActionID(),
                    actionConcluded_driverSetsArray = this.actionConcluded_driverSetsArray
                }
            );
        }
    }
}