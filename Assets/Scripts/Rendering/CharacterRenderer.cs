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
            public const string DIRECTION = "direction";
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
            _reanimator.Set(Drivers.FACING_STATE, ((int)_member.characterController.facingState));
            _reanimator.Set(Drivers.MOVEMENT_STATE, ((int)_member.characterController.movementState));

            if (_member.characterController.directionState == DirectionState.Left)
            {
                _reanimator.Set(Drivers.DIRECTION, 0);
            }
            else if (_member.characterController.directionState == DirectionState.Right)
            {
                _reanimator.Set(Drivers.DIRECTION, 1);
            }
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