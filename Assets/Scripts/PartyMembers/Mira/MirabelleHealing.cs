using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PartyNamespace {
    namespace MirabelleNamespace {
        public class MirabelleHealing : MonoBehaviour {
            private GameStateManager gameStateManager;
            private HealingAbilityImage healingImage;
            private Mirabelle mirabelle;

            private GameObject cursor;

            public HealingAbility healingAbility;
            public HealingAbility[] heals;

            public string healingAbilityType = HealingTypes.Rejuvenating;

            bool abilitiesFound;

            public bool openingUmbrella = false;
            public bool umbrellaOpened = false;
            public bool closingUmbrella = false;
            public bool umbrellaClosed = true;

            private void Start() {
                gameStateManager = GameStateManager.Instance;
                healingImage = GameObject.FindGameObjectWithTag("PrimaryIcon").GetComponent<HealingAbilityImage>();
                mirabelle = GetComponentInParent<Mirabelle>();
                cursor = GameObject.FindGameObjectWithTag("Cursor");

                heals = GetComponentsInChildren<HealingAbility>();

                SetHeal(heals[(int)mirabelle.HealingEffect]);
            }

            private void SetHeal(HealingAbility heal) {
                healingAbility = heal;

                switch (healingAbilityType) {
                    case HealingTypes.Rejuvenating:
                        healingImage.SetIcon(healingAbility.icons.rejuvenating);
                        break;
                    case HealingTypes.Warming:
                        healingImage.SetIcon(healingAbility.icons.warming);
                        break;
                    case HealingTypes.Comforting:
                        healingImage.SetIcon(healingAbility.icons.comforting);
                        break;
                    case HealingTypes.Caring:
                        healingImage.SetIcon(healingAbility.icons.caring);
                        break;
                    case HealingTypes.Loving:
                        healingImage.SetIcon(healingAbility.icons.loving);
                        break;
                }
            }

            // Opens mirabelle's umbrella and prepares for healing party members
            public void Heal() {
                if (healingAbility == null) return;


                if (mirabelle.umbrellaState == UmbrellaState.UmbrellaClosed) {
                    if (!healingAbility.coolingDown) {
                        mirabelle.state = State.Umbrella;
                        mirabelle.umbrellaState = UmbrellaState.OpeningUmbrella;
                        healingAbility.Cast(healingAbilityType);
                    }
                }else if (mirabelle.umbrellaState == UmbrellaState.UmbrellaOpened) {
                    mirabelle.state = State.Umbrella;
                    mirabelle.umbrellaState = UmbrellaState.ClosingUmbrella;
                    healingAbility.Uncast();
                }
            }
        }
    }
}
