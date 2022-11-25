using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Manapotion.PartySystem.LaurieCharacter
{
    // public class LauriePlayerInput
    // {
    //     private Party _party;
    //     private Laurie _laurie;
    //     private GameStateManager _gameStateManager;
    //     private InputProvider _provider;
        
    //     private InputActionAsset _controls;
    //     private InputActionMap _inputActionMap;

    //     private InputAction _movement;
    //     private InputAction _mouseMovement;
    //     private InputAction _sprint;
    //     private InputAction _primary;
    //     private InputAction _secondary;
    //     private InputAction _auxMove;

    //     public LauriePlayerInput(Laurie laurie)
    //     {
    //         _laurie = laurie;

    //         _party = _laurie.party;
    //         _gameStateManager = GameStateManager.Instance;
    //         _provider = _laurie.inputProvider;
            
    //         _controls = _laurie.controls;
    //         _inputActionMap = _controls.FindActionMap("Player");

    //         CreateInputAction(OnMove, _movement, "Movement");
    //         CreateInputAction(OnMouseMove, _mouseMovement, "MousePosition");
    //         CreateInputAction(OnSprint, _sprint, "Sprint");
    //         CreateInputAction(OnPrimary, _primary, "PrimaryCast");
    //         CreateInputAction(OnSecondary, _secondary, "SecondaryCast");
    //         CreateInputAction(OnAuxMove, _auxMove, "AuxilaryMovement");
    //     }

    //     private void CreateInputAction(Action<InputAction.CallbackContext> subscriber, InputAction action, string actionName)
    //     {
    //         action = _inputActionMap.FindAction(actionName);
    //         action.Enable();
    //         action.started += subscriber;
    //         action.performed += subscriber;
    //         action.canceled += subscriber;
    //     }
        
    //     private void OnMove(InputAction.CallbackContext context)
    //     {
    //         if (_party.partyLeader != PartyLeader.Laurie)
    //         {
    //             return;
    //         }
            
    //         _provider.inputState.movementDirection = context.ReadValue<Vector2>();
    //     }

    //     public void OnMouseMove(InputAction.CallbackContext context)
    //     {
    //         if (_party.partyLeader != PartyLeader.Laurie)
    //         {
    //             return;
    //         } 

    //         var simpleTargetPos = context.ReadValue<Vector2>();
    //         _provider.inputState.targetPos = Camera.main.ScreenToWorldPoint(simpleTargetPos);
    //     }

    //     public void OnSprint(InputAction.CallbackContext context) 
    //     {
    //         if (_party.partyLeader != PartyLeader.Laurie)
    //         {
    //             return;
    //         } 

    //         if (!context.canceled)
    //         {
    //             _provider.inputState.isSprinting = true;
    //         }
    //         else
    //         {
    //             _provider.inputState.isSprinting = false;
    //         }
    //     }

    //     public void OnPrimary(InputAction.CallbackContext context)
    //     {
    //         if (_party.partyLeader != PartyLeader.Laurie) 
    //         {
    //             return;
    //         } 

    //         if (!context.started)
    //         {
    //             return;
    //         }

    //         if (_gameStateManager.state == GameState.Main)
    //         {
    //             _provider.InvokePrimary();
    //         }
    //     }

    //     public void OnSecondary(InputAction.CallbackContext context)
    //     {
    //         if (_party.partyLeader != PartyLeader.Laurie)
    //         {
    //             return;
    //         } 

    //         if (!context.started)
    //         {
    //             return;
    //         }

    //         if (_gameStateManager.state == GameState.Main)
    //         {
    //             _provider.InvokeSecondary();
    //         }
    //     }

    //     public void OnAuxMove(InputAction.CallbackContext context)
    //     {
    //         if (_party.partyLeader != PartyLeader.Laurie)
    //         {
    //             return;
    //         } 

    //         if (!context.started)
    //         {
    //             return;
    //         }

    //         if (_gameStateManager.state == GameState.Main)
    //         {
    //             // _provider.InvokeAuxMove();
    //         }
    //     }
    // }
}
