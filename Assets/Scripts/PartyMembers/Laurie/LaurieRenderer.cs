using Aarthificial.Reanimation;
using UnityEngine;

namespace PartyNamespace {
    namespace LaurieNamespace {
        public class LaurieRenderer : MonoBehaviour {
            private static class Drivers {
                public const string STATE = "state";
                public const string MOVEMENT_STATE = "movementState";
                public const string FACING_STATE = "facingState";
                public const string AUXILARY_TYPE = "auxilaryType";
            }

            private Reanimator reanimator;
            private Laurie laurie;
            private LaurieController laurieController;

            private int facingState;

            void Start() {
                reanimator = GetComponent<Reanimator>();
                laurie = GetComponentInParent<Laurie>();
                laurieController = laurie.controller;

                facingState = 2;
            }

            void Update() {
                if (laurieController.movementDirection.Equals(new Vector2(0, 1))) { // north 
                    facingState = 0; // north
                }else if (laurieController.movementDirection.Equals(new Vector2(1, 1))) { // northeast
                    facingState = 0; 
                }else if (laurieController.movementDirection.Equals(new Vector2(1, 0))) { // east
                    facingState = 1; // east
                }else if (laurieController.movementDirection.Equals(new Vector2(1, -1))) { // southeast
                    facingState = 1;
                }else if (laurieController.movementDirection.Equals(new Vector2(0, -1))) { // south
                    facingState = 2; // south
                }else if (laurieController.movementDirection.Equals(new Vector2(-1, -1))) { // southwest
                    facingState = 2;
                }else if (laurieController.movementDirection.Equals(new Vector2(-1, 0))) { // west
                    facingState = 3; // west
                }else if (laurieController.movementDirection.Equals(new Vector2(-1, 1))) { // northwest
                    facingState = 3;
                }

                reanimator.Set(Drivers.STATE, (int)laurie.state);
                reanimator.Set(Drivers.FACING_STATE, facingState);
                reanimator.Set(Drivers.MOVEMENT_STATE, (int)laurie.movementState);
                reanimator.Set(Drivers.AUXILARY_TYPE, (int)laurie.auxilaryMovementType);
            }
        }
    }
}
