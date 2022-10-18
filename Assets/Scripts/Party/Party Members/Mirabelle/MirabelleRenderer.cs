using System;
using Aarthificial.Reanimation;
using UnityEngine;

namespace Manapotion.PartySystem.MirabelleCharacter
{
    public class MirabelleRenderer
    {
        public static Action OnOpenUmbrella;
        public static Action OnCloseUmbrella;

        private static class Drivers
        {
            public const string STATE = "state";
            public const string MOVEMENT_STATE = "movementState";
            public const string FACING_STATE = "facingState";
            public const string ABILITY_TYPE = "abilityType";

            public const string HAS_UMBRELLA = "hasUmbrella";
        }

        private Reanimator _reanimator;
        private Mirabelle _mirabelle;
        private MirabelleController _mirabelleController;
        private MirabelleHealing _mirabelleHealing;
        private GameStateManager _gameManager;

        private int facingState;
        private int abilityState = 1;
        private int state;
        private int hasUmbrella = 0;

        public MirabelleRenderer(Mirabelle mirabelle)
        {
            _mirabelle = mirabelle;

            _reanimator = _mirabelle.GetComponent<Reanimator>();
            _mirabelleController = _mirabelle.mirabelleController;
            _mirabelleHealing = _mirabelle.mirabelleHealing;
            _gameManager = GameStateManager.Instance;

            facingState = 2;

            _reanimator.AddListener(
                "umbrellaOpenedEvent",
                () =>
                { 
                    _mirabelle.state = State.Movement;
                    _mirabelle.umbrellaState = UmbrellaState.UmbrellaOpened;
                    hasUmbrella = 1;

                    if (OnOpenUmbrella != null) OnOpenUmbrella();
                } 
            );

            _reanimator.AddListener(
                "umbrellaClosedEvent",
                () =>
                { 
                    _mirabelle.state = State.Movement;
                    _mirabelle.umbrellaState = UmbrellaState.UmbrellaClosed;

                    hasUmbrella = 0;

                    if (OnCloseUmbrella != null) OnCloseUmbrella();
                } 
            );
        }

        
        public void Update()
        {
            RenderUpdate();
        }

        private void RenderUpdate()
        {
            if (_gameManager.state != GameState.Main) 
            {
                facingState = 2;
            }

            if (_mirabelle.state == State.Movement)
            {
                state = 0;
            }
            else if (_mirabelle.state == State.Umbrella)
            {
                if (_mirabelle.umbrellaState == UmbrellaState.OpeningUmbrella)
                {
                    abilityState = 1;
                }
                else if (_mirabelle.umbrellaState == UmbrellaState.ClosingUmbrella)
                {
                    abilityState = 0;
                }
                state = 1;
            }

            if (_mirabelleController.movementDirection.Equals(new Vector2(0, 1)))
            { // north 
                facingState = 0; // north
            }
            else if (_mirabelleController.movementDirection.Equals(new Vector2(1, 1)))
            { // northeast
                facingState = 0; 
            }
            else if (_mirabelleController.movementDirection.Equals(new Vector2(1, 0)))
            { // east
                facingState = 1; // east
            }
            else if (_mirabelleController.movementDirection.Equals(new Vector2(1, -1)))
            { // southeast
                facingState = 1;
            }
            else if (_mirabelleController.movementDirection.Equals(new Vector2(0, -1)))
            { // south
                facingState = 2; // south
            }
            else if (_mirabelleController.movementDirection.Equals(new Vector2(-1, -1)))
            { // southwest
                facingState = 2;
            }
            else if (_mirabelleController.movementDirection.Equals(new Vector2(-1, 0)))
            { // west
                facingState = 3; // west
            }
            else if (_mirabelleController.movementDirection.Equals(new Vector2(-1, 1)))
            { // northwest
                facingState = 3;
            }
    
            _reanimator.Set(Drivers.STATE, state);
            _reanimator.Set(Drivers.FACING_STATE, facingState);
            _reanimator.Set(Drivers.MOVEMENT_STATE, (int)_mirabelle.movementState);
            _reanimator.Set(Drivers.ABILITY_TYPE, abilityState);
            _reanimator.Set(Drivers.HAS_UMBRELLA, hasUmbrella);
        }
    }
}
