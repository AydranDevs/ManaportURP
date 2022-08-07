using System;
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

            public bool openingUmbrella = false;
            public bool umbrellaOpened = false;
            public bool closingUmbrella = false;
            public bool umbrellaClosed = true;

            private void Start() {
                gameStateManager = GameStateManager.Instance;
                healingImage = GameObject.FindGameObjectWithTag("PrimaryIcon").GetComponent<HealingAbilityImage>();
                buffImage = GameObject.FindGameObjectWithTag("SecondaryIcon").GetComponent<BuffAbilityImage>();
                mirabelle = GetComponentInParent<Mirabelle>();
                cursor = GameObject.FindGameObjectWithTag("Cursor");

                heals = GetComponentsInChildren<HealingAbility>();
                buffs = GetComponentsInChildren<BuffAbility>();

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


            void Update() {
                if (mirabelle.party.partyLeader != PartyLeader.Mirabelle) return;

                switch (mirabelle.healingType) {
                    case HealingType.Rejuvenating:
                        healingAbilityType = HealingTypes.Rejuvenating;

                        switch (mirabelle.healingEffect) {
                            case HealingEffect.Showera:
                                SetHeal(heals[0]);
                                break;
                            case HealingEffect.Spray:
                                SetHeal(heals[1]);
                                break;
                        }
                        break;
                    case HealingType.Warming:
                        healingAbilityType = HealingTypes.Warming;

                        switch (mirabelle.healingEffect) {
                            case HealingEffect.Showera:
                                SetHeal(heals[0]);
                                break;
                            case HealingEffect.Spray:
                                SetHeal(heals[1]);
                                break;
                        }
                        break;
                    case HealingType.Comforting:
                        healingAbilityType = HealingTypes.Comforting;

                        switch (mirabelle.healingEffect) {
                            case HealingEffect.Showera:
                                SetHeal(heals[0]);
                                break;
                            case HealingEffect.Spray:
                                SetHeal(heals[1]);
                                break;
                        }
                        break;
                    case HealingType.Caring:
                        healingAbilityType = HealingTypes.Caring;

                        switch (mirabelle.healingEffect) {
                            case HealingEffect.Showera:
                                SetHeal(heals[0]);
                                break;
                            case HealingEffect.Spray:
                                SetHeal(heals[1]);
                                break;
                        }
                        break;
                    case HealingType.Loving:
                        healingAbilityType = HealingTypes.Loving;

                        switch (mirabelle.healingEffect) {
                            case HealingEffect.Showera:
                                SetHeal(heals[0]);
                                break;
                            case HealingEffect.Spray:
                                SetHeal(heals[1]);
                                break;
                        }
                        break; 
                }

                switch (mirabelle.buffType) {
                    case BuffType.Strengthening:
                        buffAbilityEffect = BuffTypes.Strengthening;

                        switch (mirabelle.buffEffect) {
                            case BuffEffect.Showera:
                                SetBuff(buffs[0]);
                                break;
                            case BuffEffect.Spray:
                                SetBuff(buffs[1]);
                                break;
                        }
                        break;
                    case BuffType.Healing:
                        buffAbilityEffect = BuffTypes.Healing;

                        switch (mirabelle.buffEffect) {
                            case BuffEffect.Showera:
                                SetBuff(buffs[0]);
                                break;
                            case BuffEffect.Spray:
                                SetBuff(buffs[1]);
                                break;
                        }
                        break;
                    case BuffType.Swiftening:
                        buffAbilityEffect = BuffTypes.Swiftening;

                        switch (mirabelle.buffEffect) {
                            case BuffEffect.Showera:
                                SetBuff(buffs[0]);
                                break;
                            case BuffEffect.Spray:
                                SetBuff(buffs[1]);
                                break;
                        }
                        break;
                    case BuffType.Defending:
                        buffAbilityEffect = BuffTypes.Defending;

                        switch (mirabelle.buffEffect) {
                            case BuffEffect.Showera:
                                SetBuff(buffs[0]);
                                break;
                            case BuffEffect.Spray:
                                SetBuff(buffs[1]);
                                break;
                        }
                        break;
                    
                    case BuffType.PyroWarming:
                        buffAbilityEffect = BuffTypes.PyroWarming;

                        switch (mirabelle.buffEffect) {
                            case BuffEffect.Showera:
                                SetBuff(buffs[0]);
                                break;
                            case BuffEffect.Spray:
                                SetBuff(buffs[1]);
                                break;
                        }
                        break;
                    case BuffType.CryoChilling:
                        buffAbilityEffect = BuffTypes.CryoChilling;

                        switch (mirabelle.buffEffect) {
                            case BuffEffect.Showera:
                                SetBuff(buffs[0]);
                                break;
                            case BuffEffect.Spray:
                                SetBuff(buffs[1]);
                                break;
                        }
                        break;
                    case BuffType.ToxiSickening:
                        buffAbilityEffect = BuffTypes.ToxiSickening;

                        switch (mirabelle.buffEffect) {
                            case BuffEffect.Showera:
                                SetBuff(buffs[0]);
                                break;
                            case BuffEffect.Spray:
                                SetBuff(buffs[1]);
                                break;
                        }
                        break;
                    case BuffType.VoltAmplifying:
                        buffAbilityEffect = BuffTypes.VoltAmplifying;

                        switch (mirabelle.buffEffect) {
                            case BuffEffect.Showera:
                                SetBuff(buffs[0]);
                                break;
                            case BuffEffect.Spray:
                                SetBuff(buffs[1]);
                                break;
                        }
                        break;
                }

            }

            public void CastHeal() {
                if (healingAbility == null) return;


                if (umbrellaClosed && !umbrellaOpened && !openingUmbrella) {
                    if (!healingAbility.coolingDown) {
                        openingUmbrella = true;
                        umbrellaClosed = false;
                        mirabelle.state = State.Umbrella;
                        mirabelle.umbrellaState = UmbrellaState.OpeningUmbrella;
                        healingAbility.Cast(healingAbilityType);
                    }
                }else if (umbrellaOpened && !umbrellaClosed && !closingUmbrella){
                    closingUmbrella = true;
                    umbrellaOpened = false;
                    mirabelle.state = State.Umbrella;
                    mirabelle.umbrellaState = UmbrellaState.ClosingUmbrella;
                    healingAbility.Uncast();
                }
            }

            public void CastBuff() {
                if (buffAbility == null) return;

                if (!umbrellaOpened) {
                    if (!buffAbility.coolingDown) {
                        buffAbility.Cast(buffAbilityEffect);
                    }
                }else {
                    // healingAbility.Uncast();
                }
            }
        }
    }
}
