using UnityEngine;
using Manapotion.UI;

namespace Manapotion.PartySystem.MirabelleCharacter
{
    public class MirabelleHealing
    {
        private GameStateManager _gameManager;
        private HealingAbilityImage _healingImage;
        private Mirabelle _mirabelle;

        private GameObject _cursor;

        public HealingAbility healingAbility;
        public HealingAbility[] heals;

        public string healingAbilityType = HealingTypes.Rejuvenating;

        bool abilitiesFound;

        AbilityIconSprites sprites;

        public MirabelleHealing(Mirabelle mirabelle)
        {
            _mirabelle = mirabelle;

            _gameManager = GameStateManager.Instance;
            _cursor = GameObject.FindGameObjectWithTag("Cursor");

            heals = _mirabelle.GetComponentsInChildren<HealingAbility>();

            sprites = MainUIManager.Instance.abilityIconSprites;

            SetHeal(heals[(int)_mirabelle.healingEffect]);
        }

        private void SetHeal(HealingAbility heal)
        {
            healingAbility = heal;

            switch (_mirabelle.healingEffect)
            {
                case PartyBuffs.Rejuvenated:
                    _mirabelle.UpdateAbilityIcons(0, sprites.RejuvenatingShower);
                    break;
                case PartyBuffs.Comfortable:
                    _mirabelle.UpdateAbilityIcons(0, sprites.ComfortingShower);
                    break;
                case PartyBuffs.Cared_For:
                    _mirabelle.UpdateAbilityIcons(0, sprites.CaringShower);
                    break;
                case PartyBuffs.Loved:
                    _mirabelle.UpdateAbilityIcons(0, sprites.LovingShower);
                    break;
                case PartyBuffs.Empowered:
                    _mirabelle.UpdateAbilityIcons(0, sprites.EmpoweringShower);
                    break;
                case PartyBuffs.Sociable:
                    _mirabelle.UpdateAbilityIcons(0, sprites.SociableShower);
                    break;
                case PartyBuffs.Nimble:
                    _mirabelle.UpdateAbilityIcons(0, sprites.SwifteningShower);
                    break;
                case PartyBuffs.Toughened:
                    _mirabelle.UpdateAbilityIcons(0, sprites.TougheningShower);
                    break;
                case PartyBuffs.Pyro_UP:
                    _mirabelle.UpdateAbilityIcons(0, sprites.PyroUP);
                    break;
                case PartyBuffs.Cryo_UP:
                    _mirabelle.UpdateAbilityIcons(0, sprites.CryoUP);
                    break;
                case PartyBuffs.Toxi_UP:
                    _mirabelle.UpdateAbilityIcons(0, sprites.ToxiUP);
                    break;
                case PartyBuffs.Volt_UP:
                    _mirabelle.UpdateAbilityIcons(0, sprites.VoltUP);
                    break;
            }
        }

        public void Update() {
            if (_mirabelle.partyMemberState != PartyMemberState.CurrentLeader) 
            {
                return;
            }

            SetHeal(heals[(int)_mirabelle.healingType]);
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
