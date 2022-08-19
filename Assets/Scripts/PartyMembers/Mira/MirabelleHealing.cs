using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manapotion.PartySystem.MirabelleCharacter
{
    public class MirabelleHealing : MonoBehaviour
    {
        private GameStateManager _gameManager;
        private HealingAbilityImage _healingImage;
        private Mirabelle _mirabelle;

        private GameObject _cursor;

        public HealingAbility healingAbility;
        public HealingAbility[] heals;

        public string healingAbilityType = HealingTypes.Rejuvenating;

        bool abilitiesFound;

        public bool openingUmbrella = false;
        public bool umbrellaOpened = false;
        public bool closingUmbrella = false;
        public bool umbrellaClosed = true;

        public MirabelleHealing(Mirabelle mirabelle)
        {
            _mirabelle = mirabelle;

            _gameManager = GameStateManager.Instance;
            _healingImage = GameObject.FindGameObjectWithTag("PrimaryIcon").GetComponent<HealingAbilityImage>();
            _cursor = GameObject.FindGameObjectWithTag("Cursor");

            heals = _mirabelle.GetComponentsInChildren<HealingAbility>();

            SetHeal(heals[(int)_mirabelle.HealingEffect]);
        }

        private void SetHeal(HealingAbility heal)
        {
            healingAbility = heal;

            switch (healingAbilityType)
            {
                case HealingTypes.Rejuvenating:
                    _healingImage.SetIcon(healingAbility.icons.rejuvenating);
                    break;
                case HealingTypes.Warming:
                    _healingImage.SetIcon(healingAbility.icons.warming);
                    break;
                case HealingTypes.Comforting:
                    _healingImage.SetIcon(healingAbility.icons.comforting);
                    break;
                case HealingTypes.Caring:
                    _healingImage.SetIcon(healingAbility.icons.caring);
                    break;
                case HealingTypes.Loving:
                    _healingImage.SetIcon(healingAbility.icons.loving);
                    break;
            }
        }

        // Opens mirabelle's umbrella and prepares for healing party members
        public void Heal()
        {
            if (healingAbility == null) 
            {
                return;
            }


            if (_mirabelle.umbrellaState == UmbrellaState.UmbrellaClosed)
            {
                if (!healingAbility.coolingDown)
                {
                    _mirabelle.state = State.Umbrella;
                    _mirabelle.umbrellaState = UmbrellaState.OpeningUmbrella;
                    healingAbility.Cast(healingAbilityType);
                }
            }
            else if (_mirabelle.umbrellaState == UmbrellaState.UmbrellaOpened)
            {
                _mirabelle.state = State.Umbrella;
                _mirabelle.umbrellaState = UmbrellaState.ClosingUmbrella;
                healingAbility.Uncast();
            }
        }
    }
}
