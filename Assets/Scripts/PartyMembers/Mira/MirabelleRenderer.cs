using Aarthificial.Reanimation;
using UnityEngine;

namespace PartyNamespace {
    namespace MirabelleNamespace {
        public class MirabelleRenderer : MonoBehaviour {
            private static class Drivers {
                public const string STATE = "state";
                public const string MOVEMENT_STATE = "movementState";
                public const string FACING_STATE = "facingState";
            }

            private Reanimator reanimator;
            private Mirabelle mirabelle;
            private MirabelleController mirabelleController;
            private GameStateManager gameManager;

            private int facingState;

            void Start() {
                reanimator = GetComponent<Reanimator>();
                mirabelle = GetComponentInParent<Mirabelle>();
                mirabelleController = mirabelle.controller;
                gameManager = GameStateManager.Instance;

                facingState = 2;
            }

            private void Update() {
                if (gameManager.state != GameState.Main) return;

                if (mirabelleController.movementDirection.Equals(new Vector2(0, 1))) { // north 
                    facingState = 0; // north
                }else if (mirabelleController.movementDirection.Equals(new Vector2(1, 1))) { // northeast
                    facingState = 0; 
                }else if (mirabelleController.movementDirection.Equals(new Vector2(1, 0))) { // east
                    facingState = 1; // east
                }else if (mirabelleController.movementDirection.Equals(new Vector2(1, -1))) { // southeast
                    facingState = 1;
                }else if (mirabelleController.movementDirection.Equals(new Vector2(0, -1))) { // south
                    facingState = 2; // south
                }else if (mirabelleController.movementDirection.Equals(new Vector2(-1, -1))) { // southwest
                    facingState = 2;
                }else if (mirabelleController.movementDirection.Equals(new Vector2(-1, 0))) { // west
                    facingState = 3; // west
                }else if (mirabelleController.movementDirection.Equals(new Vector2(-1, 1))) { // northwest
                    facingState = 3;
                }

                reanimator.Set(Drivers.STATE, (int)mirabelle.state);
                reanimator.Set(Drivers.FACING_STATE, facingState);
                reanimator.Set(Drivers.MOVEMENT_STATE, (int)mirabelle.movementState);
            }
        }
    }
}
