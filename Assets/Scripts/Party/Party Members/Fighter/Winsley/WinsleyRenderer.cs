using Aarthificial.Reanimation;
using UnityEngine;

namespace Manapotion.PartySystem.WinsleyCharacter
{
    public class WinsleyRenderer
    {
        private static class Drivers
        {
            public const string STATE = "state";
            public const string MOVEMENT_STATE = "movementState";
            public const string FACING_STATE = "facingState";
        }

        private Reanimator _reanimator;
        private Winsley _winsley;
        // private WinsleyController _winsleyController;
        private GameStateManager _gameManager;

        private int facingState;

        public WinsleyRenderer(Winsley winsley)
        {
            _winsley = winsley;

            _reanimator = _winsley.GetComponent<Reanimator>();
            // _winsleyController = winsley.winsleyController;
            _gameManager = GameStateManager.Instance;

            facingState = 2;
        }

        
        public void Update()
        {
            if (_gameManager.state != GameState.Main)
            {
                facingState = 2;
            }

            if (_winsley.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(0, 1)))
            { // north 
                facingState = 0; // north
            }
            else if (_winsley.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(1, 1)))
            { // northeast
                facingState = 0; 
            }
            else if (_winsley.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(1, 0)))
            { // east
                facingState = 1; // east
            }
            else if (_winsley.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(1, -1)))
            { // southeast
                facingState = 1;
            }
            else if (_winsley.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(0, -1)))
            { // south
                facingState = 2; // south
            }
            else if (_winsley.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(-1, -1)))
            { // southwest
                facingState = 2;
            }
            else if (_winsley.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(-1, 0)))
            { // west
                facingState = 3; // west
            }
            else if (_winsley.characterInput.GetInputProvider().GetState().movementDirection.Equals(new Vector2(-1, 1)))
            { // northwest
                facingState = 3;
            }

            _reanimator.Set(Drivers.STATE, (int)_winsley.state);
            _reanimator.Set(Drivers.FACING_STATE, facingState);
            _reanimator.Set(Drivers.MOVEMENT_STATE, (int)_winsley.movementState);
        }
    }
}
