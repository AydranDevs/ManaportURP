using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PartyNamespace {
    namespace MirabelleNamespace {
        public class MirabelleHealing : MonoBehaviour {
            private GameStateManager gameStateManager;
            private HealingAbilityImage healingImage;
            private BuffAbilityImage buffImage;
            private Mirabelle mirabelle;

            private GameObject cursor;

            public HealingAbility healingAbility;
            public BuffAbility buffAbility;
            public HealingAbility[] heals;
            public BuffAbility[] buffs;

            public string healingAbilityType = HealingTypes.Rejuvenating;
            public string buffAbilityEffect = BuffTypes.Strengthening;

            bool abilitiesFound;

            private void Start() {
                gameStateManager = GameStateManager.Instance;
                healingImage = GameObject.FindGameObjectWithTag("PrimaryIcon").GetComponent<HealingAbilityImage>();
                buffImage = GameObject.FindGameObjectWithTag("SecondaryIcon").GetComponent<BuffAbilityImage>();
                mirabelle = GetComponentInParent<Mirabelle>();
                cursor = GameObject.FindGameObjectWithTag("Cursor");

                heals = GetComponentsInChildren<HealingAbility>();
                buffs = GetComponentsInChildren<BuffAbility>();
            }

            public void Refresh() {
                // retrieve saved heal/buff info
                SetHeal(heals[0]);
                // SetBuff(buffs[0]);
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

            private void SetBuff(BuffAbility buff) {
                buffAbility = buff;

                switch (buffAbilityEffect) {
                    case BuffTypes.Strengthening:
                        buffImage.SetIcon(buffAbility.icons.strengthening);
                        break;
                    case BuffTypes.Healing:
                        buffImage.SetIcon(buffAbility.icons.healing);
                        break;
                    case BuffTypes.Swiftening:
                        buffImage.SetIcon(buffAbility.icons.swiftening);
                        break;
                    case BuffTypes.Defending:
                        buffImage.SetIcon(buffAbility.icons.defending);
                        break;

                    case BuffTypes.PyroWarming:
                        buffImage.SetIcon(buffAbility.icons.pyroWarming);
                        break;
                    case BuffTypes.CryoChilling:
                        buffImage.SetIcon(buffAbility.icons.cryoChilling);
                        break;
                    case BuffTypes.ToxiSickening:
                        buffImage.SetIcon(buffAbility.icons.toxiSickening);
                        break;
                    case BuffTypes.VoltAmplifying:
                        buffImage.SetIcon(buffAbility.icons.voltAmplifying);
                        break;
                }
            }
        }
    }
}
