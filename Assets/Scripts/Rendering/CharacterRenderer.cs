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

        public void SetDriver(string driverName, int driverValue)
        {
            _reanimator.Set(driverName, driverValue);
        }

        public void SetDriverConditional(string driverName, int driverValue, string conditionalDriverName, int conditionalDriverValue)
        {
            _reanimator.AddListener(conditionalDriverName, () => 
            {
                if (_reanimator.State.Get(conditionalDriverName) == conditionalDriverValue)
                {
                    Debug.Log($"{conditionalDriverName} is currently {conditionalDriverValue}");
                    _reanimator.Set(driverName, driverValue);
                }
            });
        }

        public Reanimator GetReanimator()
        {
            return this._reanimator;
        }
    }
}