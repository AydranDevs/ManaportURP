using Aarthificial.Reanimation;
using Manapotion.PartySystem;
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
            public const string STATE = "state";
            public const string MOVEMENT_STATE = "movementState";
            public const string FACING_STATE = "facingState";
            public const string AUXILARY_TYPE = "auxilaryType";
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
                _member.facingState = FacingState.South;
            }

            if (_member.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(0, 1))) 
            { // north 
                _member.facingState = FacingState.North; // north
            }
            else if (_member.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(1, 1)))
            { // northeast
                _member.facingState = FacingState.North; 
            }
            else if (_member.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(1, 0)))
            { // east
                _member.facingState = FacingState.East; // east
            }
            else if (_member.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(1, -1)))
            { // southeast
                _member.facingState = FacingState.East;
            }
            else if (_member.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(0, -1)))
            { // south
                _member.facingState = FacingState.South; // south
            }
            else if (_member.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(-1, -1))) 
            { // southwest
                _member.facingState = FacingState.South;
            }
            else if (_member.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(-1, 0)))
            { // west
                _member.facingState = FacingState.West; // west
            }
            else if (_member.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(-1, 1))) 
            { // northwest
                _member.facingState = FacingState.West;
            }

            _reanimator.Set(Drivers.FACING_STATE, ((int)_member.facingState));
            _reanimator.Set(Drivers.MOVEMENT_STATE, (int)_member.movementState);
        }
    }
}