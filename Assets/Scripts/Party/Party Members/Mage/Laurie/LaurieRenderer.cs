using Aarthificial.Reanimation;
using UnityEngine;

namespace Manapotion.PartySystem.LaurieCharacter
{
    public class LaurieRenderer
    {
        private static class Drivers 
        {
            public const string STATE = "state";
            public const string MOVEMENT_STATE = "movementState";
            public const string FACING_STATE = "facingState";
            public const string AUXILARY_TYPE = "auxilaryType";
        }

        private Reanimator _reanimator;
        private Laurie _laurie;
        // private LaurieController _laurieController;
        private GameStateManager _gameManager;

        private int facingState;

        public LaurieRenderer(Laurie laurie)
        {
            _laurie = laurie;
            
            _gameManager = GameStateManager.Instance;
            _reanimator = _laurie.GetComponent<Reanimator>();
            // _laurieController = _laurie.laurieController;

            facingState = 2;
        }   

        public void Update() 
        {
            if (_gameManager.state != GameState.Main)
            {
                facingState = 2;
            }
            
            if (_laurie.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(0, 1))) 
            { // north 
                facingState = 0; // north
            }
            else if (_laurie.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(1, 1)))
            { // northeast
                facingState = 0; 
            }
            else if (_laurie.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(1, 0)))
            { // east
                facingState = 1; // east
            }
            else if (_laurie.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(1, -1)))
            { // southeast
                facingState = 1;
            }
            else if (_laurie.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(0, -1)))
            { // south
                facingState = 2; // south
            }
            else if (_laurie.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(-1, -1))) 
            { // southwest
                facingState = 2;
            }
            else if (_laurie.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(-1, 0)))
            { // west
                facingState = 3; // west
            }
            else if (_laurie.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(-1, 1))) 
            { // northwest
                facingState = 3;
            }

            _laurie.facingState = (FacingState)facingState;

            _reanimator.Set(Drivers.STATE, (int)_laurie.state);
            _reanimator.Set(Drivers.FACING_STATE, facingState);
            _reanimator.Set(Drivers.MOVEMENT_STATE, (int)_laurie.movementState);
            _reanimator.Set(Drivers.AUXILARY_TYPE, (int)_laurie.auxilaryMovementType);
        }
    }
}
