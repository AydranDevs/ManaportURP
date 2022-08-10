using System;
using Aarthificial.Reanimation;
using UnityEngine;

namespace PartyNamespace {
    namespace MirabelleNamespace {
        public class MirabelleRenderer : MonoBehaviour {
            public static MirabelleRenderer Instance;

            public static Action OnOpenUmbrella;
            public static Action OnCloseUmbrella;

            private static class Drivers {
                public const string STATE = "state";
                public const string MOVEMENT_STATE = "movementState";
                public const string FACING_STATE = "facingState";
                public const string ABILITY_TYPE = "abilityType";

                public const string HAS_UMBRELLA = "hasUmbrella";
            }

            private Reanimator reanimator;
            private Mirabelle mirabelle;
            private MirabelleController mirabelleController;
            private MirabelleHealing mirabelleHealing;
            private GameStateManager gameManager;

            private int facingState;
            private int abilityState = 1;
            private int state;
            private int hasUmbrella = 0;

            private bool abilityInit = false;

            void Awake() {
                Instance = this;
            }

            void Start() {
                reanimator = GetComponent<Reanimator>();
                mirabelle = GetComponentInParent<Mirabelle>();
                mirabelleController = mirabelle.controller;
                mirabelleHealing = mirabelle.healing;
                gameManager = GameStateManager.Instance;

                facingState = 2;

                reanimator.AddListener(
                    "umbrellaOpenedEvent",
                    () => { 
                        mirabelle.state = State.Movement;
                        mirabelle.umbrellaState = UmbrellaState.UmbrellaOpened;
                        hasUmbrella = 1;

                        if (OnOpenUmbrella != null) OnOpenUmbrella();
                    } 
                );

                reanimator.AddListener(
                    "umbrellaClosedEvent",
                    () => { 
                        mirabelle.state = State.Movement;
                        mirabelle.umbrellaState = UmbrellaState.UmbrellaClosed;

                        hasUmbrella = 0;

                        if (OnCloseUmbrella != null) OnCloseUmbrella();
                    } 
                );
            }

            private void Update() {
                RenderUpdate();
            }

            private void RenderUpdate() {
                if (gameManager.state != GameState.Main) return;

                if (mirabelle.state == State.Movement) {
                    state = 0;
                }else if (mirabelle.state == State.Umbrella) {
                    if (mirabelle.umbrellaState == UmbrellaState.OpeningUmbrella) {
                        abilityState = 1;
                    }else if (mirabelle.umbrellaState == UmbrellaState.ClosingUmbrella) {
                        abilityState = 0;
                    }
                    state = 1;
                }

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
        
                reanimator.Set(Drivers.STATE, state);
                reanimator.Set(Drivers.FACING_STATE, facingState);
                reanimator.Set(Drivers.MOVEMENT_STATE, (int)mirabelle.movementState);
                reanimator.Set(Drivers.ABILITY_TYPE, abilityState);
                reanimator.Set(Drivers.HAS_UMBRELLA, hasUmbrella);
            }
        }
    }
}
