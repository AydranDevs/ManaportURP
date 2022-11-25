using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Manapotion.PartySystem.MirabelleCharacter
{
    // public class MirabellePlayerInput
    // {
    //     private Party _party;
    //     private Mirabelle _mirabelle;
    //     private GameStateManager _gameManager;
    //     private InputProvider _provider;

    //     private InputActionAsset _controls;
    //     private InputActionMap _inputActionMap;

    //     private InputAction _movement;
    //     private InputAction _mouseMovement;
    //     private InputAction _sprint;
    //     private InputAction _primary;
    //     private InputAction _secondary;
    //     private InputAction _auxMove;

    //     public MirabellePlayerInput(Mirabelle mirabelle)
    //     {
    //         _mirabelle = mirabelle;

    //         _party = _mirabelle.party;
    //         _gameManager = GameStateManager.Instance;
    //         _provider = mirabelle.inputProvider;

    //         _controls = _mirabelle.controls;
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
    
        
    //     public void OnMove(InputAction.CallbackContext context)
    //     {
    //         if (_party.partyLeader != PartyLeader.Mirabelle)
    //         {
    //             return;
    //         }

    //         _provider.inputState.movementDirection = context.ReadValue<Vector2>();
    //     }

    //     public void OnMouseMove(InputAction.CallbackContext context)
    //     {
    //         if (_party.partyLeader != PartyLeader.Mirabelle)
    //         {
    //             return;
    //         } 

    //         var simpleTargetPos = context.ReadValue<Vector2>();
    //         _provider.inputState.targetPos = Camera.main.WorldToScreenPoint(simpleTargetPos);
    //     }

    //     public void OnSprint(InputAction.CallbackContext context)
    //     {
    //         if (_party.partyLeader != PartyLeader.Mirabelle)
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
    //         if (_party.partyLeader != PartyLeader.Mirabelle)
    //         {
    //             return;
    //         } 

    //         if (!context.started)
    //         {
    //             return;
    //         }

    //         if (_gameManager.state == GameState.Main)
    //         {
    //             _provider.InvokePrimary();
    //         }
    //     }

    //     public void OnSecondary(InputAction.CallbackContext context) {
    //         if (_party.partyLeader != PartyLeader.Mirabelle)
    //         {
    //             return;
    //         } 

    //         if (!context.started)
    //         {
    //             return;
    //         }

    //         if (_gameManager.state == GameState.Main)
    //         {
    //             _provider.InvokeSecondary();
    //         }
    //     }

    //     public void OnAuxMove(InputAction.CallbackContext context)
    //     {
    //         if (_party.partyLeader != PartyLeader.Mirabelle)
    //         {
    //             return;
    //         } 
            
    //         if (!context.started)
    //         {
    //             return;
    //         }
            
    //         if (_gameManager.state == GameState.Main)
    //         {
    //             _provider.InvokeAux();
    //         }
    //     }
    // }
}
