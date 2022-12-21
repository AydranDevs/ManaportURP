using System;
using System.Collections.Generic;
using Aarthificial.Reanimation;
using Manapotion.PartySystem;
using Manapotion.Actions;
using Manapotion.Input;
using UnityEngine;

namespace Manapotion.Rendering
{
    [System.Serializable]
    public class CharacterRenderer
    {
        private PartyMember _member;

        [UnityEngine.SerializeField]
        private Reanimator _reanimator;

        // drivers that are modified by this class
        private static class Drivers 
        {
            public const string MOVEMENT_STATE = "movementState";
            public const string FACING_STATE = "facingState";
        }

        public void Init(PartyMember member)
        {
            // not a monobehaviour, sub to the Update event
            ManaBehaviour.OnUpdate += Update;   
            
            _member = member;

            // subscribe to every action's state event
            foreach (var action in _member.actionsManagerScriptableObject.possibleActions)
            {
                action.OnActionPerformedEvent += OnActionPerformedEvent_UpdateDriver;
                action.OnActionConcludedEvent += OnActionConcludedEvent_UpdateDriver;
            }
        }

        private void Update()
        {
            if (GameStateManager.Instance.state != GameState.Main)
            {
                _member.characterController.facingState = FacingState.South;
            }

            if (_member.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(0, 1))) 
            { // north 
                _member.characterController.facingState = FacingState.North; // north
            }
            else if (_member.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(1, 1)))
            { // northeast
                _member.characterController.facingState = FacingState.North; 
            }
            else if (_member.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(1, 0)))
            { // east
                _member.characterController.facingState = FacingState.East; // east
            }
            else if (_member.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(1, -1)))
            { // southeast
                _member.characterController.facingState = FacingState.East;
            }
            else if (_member.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(0, -1)))
            { // south
                _member.characterController.facingState = FacingState.South; // south
            }
            else if (_member.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(-1, -1))) 
            { // southwest
                _member.characterController.facingState = FacingState.South;
            }
            else if (_member.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(-1, 0)))
            { // west
                _member.characterController.facingState = FacingState.West; // west
            }
            else if (_member.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(-1, 1))) 
            { // northwest
                _member.characterController.facingState = FacingState.West;
            }

            _reanimator.Set(Drivers.FACING_STATE, ((int)_member.characterController.facingState));
            _reanimator.Set(Drivers.MOVEMENT_STATE, ((int)_member.characterController.movementState));

            if (_member.characterTargeting.currentlyTargeted == null)
            {
                return;
            }
            
            _member.characterController.facingState = (FacingState)_member.characterTargeting.GetFacingStateToTarget();
            _reanimator.Set(Drivers.FACING_STATE, _member.characterTargeting.GetFacingStateToTarget());
        }

        public void OnActionPerformedEvent_UpdateDriver(object sender, ActionScriptableObject.OnActionPerformedEventArgs e)
        {
           for (int i = 0; i < e.actionPerformed_driverSetsArray.Length; i++)
           {
                _reanimator.Set(e.actionPerformed_driverSetsArray[i].driverName, e.actionPerformed_driverSetsArray[i].set);
                // if (e.actionPerformed_driverSetsArray[i].watchDriver != "")
                // {
                //     var onCompleteDriver = e.actionPerformed_driverSetsArray[i].onComplete_driverName;
                //     var onCompleteSet = e.actionPerformed_driverSetsArray[i].onComplete_set;

                //     _reanimator.AddListener(e.actionPerformed_driverSetsArray[i].watchDriver, () => 
                //     {
                //         _reanimator.Set(onCompleteDriver, onCompleteSet);
                //     });
                // }
           }
        }

        public void OnActionConcludedEvent_UpdateDriver(object sender, ActionScriptableObject.OnActionConcludedEventArgs e)
        {
            for (int i = 0; i < e.actionConcluded_driverSetsArray.Length; i++)
            {
                _reanimator.Set(e.actionConcluded_driverSetsArray[i].driverName, e.actionConcluded_driverSetsArray[i].set);
                // if (e.actionConcluded_driverSetsArray[i].watchDriver != "")
                // {
                //     var onCompleteDriver = e.actionConcluded_driverSetsArray[i].onComplete_driverName;
                //     var onCompleteSet = e.actionConcluded_driverSetsArray[i].onComplete_set;

                //     _reanimator.AddListener(e.actionConcluded_driverSetsArray[i].watchDriver, () => 
                //     {
                //         _reanimator.Set(onCompleteDriver, onCompleteSet);
                //     });
                // }
            }
        }

        public Reanimator GetReanimator()
        {
            return this._reanimator;
        }
    }
}