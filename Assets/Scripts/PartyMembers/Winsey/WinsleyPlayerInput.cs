using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Manapotion.PartySystem.WinsleyCharacter
{
    public class WinsleyPlayerInput
    {
        private Party _party;
        private Winsley _winsley;
        private GameStateManager _gameManager;
        private InputProvider _provider;

        private InputActionAsset _controls;
        private InputActionMap _inputActionMap;

        private InputAction _movement;
        private InputAction _mouseMovement;
        private InputAction _sprint;
        private InputAction _primary;
        private InputAction _secondary;
        private InputAction _auxMove;

        public WinsleyPlayerInput(Winsley winsley)
        {
            _winsley = winsley;

            _party = _winsley.party;
            _gameManager = GameStateManager.Instance;
            _provider = _winsley.inputProvider;

            _controls = _winsley.controls;
            _inputActionMap = _controls.FindActionMap("Player");

            CreateInputAction(OnMove, _movement, "Movement");
            CreateInputAction(OnMouseMove, _mouseMovement, "MousePosition");
            CreateInputAction(OnSprint, _sprint, "Sprint");
            CreateInputAction(OnPrimary, _primary, "PrimaryCast");
            CreateInputAction(OnSecondary, _secondary, "SecondaryCast");
            CreateInputAction(OnAuxMove, _auxMove, "AuxilaryMovement");
        }

        private void CreateInputAction(Action<InputAction.CallbackContext> subscriber, InputAction action, string actionName)
        {
            action = _inputActionMap.FindAction(actionName);
            action.Enable();
            action.started += subscriber;
            action.performed += subscriber;
            action.canceled += subscriber;
        }
        
        public void OnMove(InputAction.CallbackContext context)
        {
            if (_party.partyLeader != PartyLeader.Winsley)
            {
                return;
            }

            _provider.inputState.movementDirection = context.ReadValue<Vector2>();
        }

        public void OnMouseMove(InputAction.CallbackContext context)
        {
            if (_party.partyLeader != PartyLeader.Winsley)
            {
                return;
            } 

            var simpleTargetPos = context.ReadValue<Vector2>();
            _provider.inputState.targetPos = Camera.main.WorldToScreenPoint(simpleTargetPos);
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            if (_party.partyLeader != PartyLeader.Winsley)
            {
                return;
            } 
            if (!context.canceled)
            {
                _provider.inputState.isSprinting = true;
            }
            else
            {
                _provider.inputState.isSprinting = false;
            }
        }

        public void OnPrimary(InputAction.CallbackContext context)
        {
            if (_party.partyLeader != PartyLeader.Winsley)
            {
                return;
            } 
            if (!context.started)
            {
                return;
            }

            if (_gameManager.state == GameState.Main)
            {
                _provider.InvokePrimary();
            }
        }

        public void OnSecondary(InputAction.CallbackContext context)
        {
            if (_party.partyLeader != PartyLeader.Winsley)
            {
                return;
            } 

            if (!context.started)
            {
                return;
            }

            if (_gameManager.state == GameState.Main)
            {
                _provider.InvokeSecondary();
            }
        }

        public void OnAuxMove(InputAction.CallbackContext context)
        {
            if (_party.partyLeader != PartyLeader.Winsley)
            {
                return;
            } 
            
            if (!context.started)
            {
                return;
            }
            
            if (_gameManager.state == GameState.Main)
            {
                _provider.InvokeAuxMove();
            }
        }
    }
}
