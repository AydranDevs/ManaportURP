using UnityEngine;
using UnityEngine.InputSystem;

namespace PartyNamespace {
    namespace WinsleyNamespace {
        public class WinsleyPlayerInput : MonoBehaviour {
            private Party party;
            private Winsley winsley;
            private GameStateManager gameStateManager;
            [SerializeField] private InputProvider provider;

            private void Start() {
                winsley = GetComponentInParent<Winsley>();
                party = winsley.party;
                gameStateManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameStateManager>();
            }
        
            
            public void OnMove(InputAction.CallbackContext context) {
                if (party.partyLeader != PartyLeader.Winsley) { return; }

                provider.inputState.movementDirection = context.ReadValue<Vector2>();
            }

            public void OnMouseMove(InputAction.CallbackContext context) {
                if (party.partyLeader != PartyLeader.Winsley) { return; } 

                var simpleTargetPos = context.ReadValue<Vector2>();
                provider.inputState.targetPos = Camera.main.WorldToScreenPoint(simpleTargetPos);
            }

            public void OnSprint(InputAction.CallbackContext context) {
                if (party.partyLeader != PartyLeader.Winsley) { return; } 

                if (!context.canceled) {
                    provider.inputState.isSprinting = true;
                }else {
                    provider.inputState.isSprinting = false;
                }
            }

            public void OnPrimary(InputAction.CallbackContext context) {
                if (party.partyLeader != PartyLeader.Winsley) { return; } 

                if (!context.started) return;

                if (gameStateManager.state == GameState.Main) {
                    provider.InvokePrimary();
                }
            }

            public void OnSecondary(InputAction.CallbackContext context) {
                if (party.partyLeader != PartyLeader.Winsley) { return; } 

                if (!context.started) return;

                if (gameStateManager.state == GameState.Main) {
                    provider.InvokeSecondary();
                }
            }

            public void OnAuxMove(InputAction.CallbackContext context) {
                if (party.partyLeader != PartyLeader.Winsley) { return; } 
                
                if (!context.started) return;
                
                if (gameStateManager.state == GameState.Main) {
                    provider.InvokeAuxMove();
                }
            }
        }
    }
}
