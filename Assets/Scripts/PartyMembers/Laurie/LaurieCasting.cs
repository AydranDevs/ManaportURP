using UnityEngine;
using Manapotion.UI;

namespace Manapotion.PartySystem.LaurieCharacter 
{
    public class LaurieCasting
    {
        private GameStateManager _gameStateManager;
        private Laurie _laurie;
        
        private GameObject _cursor;

        public Spell primarySpell;
        public Spell secondarySpell;
        public Spell[] spells;

        public string primaryElement = Elements.Arcane;
        public string secondaryElement = Elements.Arcane;

        bool spellsFound;

        AbilityIconSprites sprites;

        public LaurieCasting(Laurie laurie)
        {
            _laurie = laurie;

            _gameStateManager = GameStateManager.Instance;
            _cursor = GameObject.FindGameObjectWithTag("Cursor");

            spells = _laurie.GetComponentsInChildren<Spell>();

            sprites = MainUIManager.Instance.abilityIconSprites;

            SetSpell(0, spells[0]);
            SetSpell(1, spells[0]);
        }

        public void SetSpell(int slot, Spell spell) 
        {   
            switch (slot) 
            {
                case 0:
                    primarySpell = spell;

                    switch (primaryElement) 
                    {
                        case Elements.Arcane:
                            if (primarySpell.spellId == "burston")
                            {
                                _laurie.UpdateAbilityIcons(slot, sprites.ArcaneBurston);
                            }
                            else if (primarySpell.spellId == "blasteur")
                            {
                                _laurie.UpdateAbilityIcons(slot, sprites.ArcaneBlasteur);
                            }
                            else if (primarySpell.spellId == "automa")
                            {
                                _laurie.UpdateAbilityIcons(slot, sprites.ArcaneAutoma);
                            }

                            break;
                        case Elements.Pyro:
                            if (primarySpell.spellId == "burston")
                            {
                                _laurie.UpdateAbilityIcons(slot, sprites.PyroBurston);
                            }
                            else if (primarySpell.spellId == "blasteur")
                            {
                                _laurie.UpdateAbilityIcons(slot, sprites.PyroBlasteur);
                            }
                            else if (primarySpell.spellId == "automa")
                            {
                                _laurie.UpdateAbilityIcons(slot, sprites.PyroAutoma);
                            }

                            break;
                        case Elements.Cryo:
                            if (primarySpell.spellId == "burston")
                            {
                                _laurie.UpdateAbilityIcons(slot, sprites.CryoBurston);
                            }
                            else if (primarySpell.spellId == "blasteur")
                            {
                                _laurie.UpdateAbilityIcons(slot, sprites.CryoBlasteur);
                            }
                            else if (primarySpell.spellId == "automa")
                            {
                                _laurie.UpdateAbilityIcons(slot, sprites.CryoAutoma);
                            }

                            break;
                        case Elements.Toxi:
                            if (primarySpell.spellId == "burston")
                            {
                                _laurie.UpdateAbilityIcons(slot, sprites.ToxiBurston);
                            }
                            else if (primarySpell.spellId == "blasteur")
                            {
                                _laurie.UpdateAbilityIcons(slot, sprites.ToxiBlasteur);
                            }
                            else if (primarySpell.spellId == "automa")
                            {
                                _laurie.UpdateAbilityIcons(slot, sprites.ToxiAutoma);
                            }

                            break;
                        case Elements.Volt:
                            if (primarySpell.spellId == "burston")
                            {
                                _laurie.UpdateAbilityIcons(slot, sprites.VoltBurston);
                            }
                            else if (primarySpell.spellId == "blasteur")
                            {
                                _laurie.UpdateAbilityIcons(slot, sprites.VoltBlasteur);
                            }
                            else if (primarySpell.spellId == "automa")
                            {
                                _laurie.UpdateAbilityIcons(slot, sprites.VoltAutoma);
                            }

                            break;
                    }
                    break;
                case 1:
                    secondarySpell = spell;

                    switch (secondaryElement) 
                    {
                        case Elements.Arcane:
                            if (secondarySpell.spellId == "burston")
                            {
                                _laurie.UpdateAbilityIcons(slot, sprites.ArcaneBurston);
                            }
                            else if (secondarySpell.spellId == "blasteur")
                            {
                                _laurie.UpdateAbilityIcons(slot, sprites.ArcaneBlasteur);
                            }
                            else if (secondarySpell.spellId == "automa")
                            {
                                _laurie.UpdateAbilityIcons(slot, sprites.ArcaneAutoma);
                            }

                            break;
                        case Elements.Pyro:
                            if (secondarySpell.spellId == "burston")
                            {
                                _laurie.UpdateAbilityIcons(slot, sprites.PyroBurston);
                            }
                            else if (secondarySpell.spellId == "blasteur")
                            {
                                _laurie.UpdateAbilityIcons(slot, sprites.PyroBlasteur);
                            }
                            else if (secondarySpell.spellId == "automa")
                            {
                                _laurie.UpdateAbilityIcons(slot, sprites.PyroAutoma);
                            }

                            break;
                        case Elements.Cryo:
                            if (secondarySpell.spellId == "burston")
                            {
                                _laurie.UpdateAbilityIcons(slot, sprites.CryoBurston);
                            }
                            else if (secondarySpell.spellId == "blasteur")
                            {
                                _laurie.UpdateAbilityIcons(slot, sprites.CryoBlasteur);
                            }
                            else if (secondarySpell.spellId == "automa")
                            {
                                _laurie.UpdateAbilityIcons(slot, sprites.CryoAutoma);
                            }

                            break;
                        case Elements.Toxi:
                            if (secondarySpell.spellId == "burston")
                            {
                                _laurie.UpdateAbilityIcons(slot, sprites.ToxiBurston);
                            }
                            else if (secondarySpell.spellId == "blasteur")
                            {
                                _laurie.UpdateAbilityIcons(slot, sprites.ToxiBlasteur);
                            }
                            else if (secondarySpell.spellId == "automa")
                            {
                                _laurie.UpdateAbilityIcons(slot, sprites.ToxiAutoma);
                            }

                            break;
                        case Elements.Volt:
                            if (secondarySpell.spellId == "burston")
                            {
                                _laurie.UpdateAbilityIcons(slot, sprites.VoltBurston);
                            }
                            else if (secondarySpell.spellId == "blasteur")
                            {
                                _laurie.UpdateAbilityIcons(slot, sprites.VoltBlasteur);
                            }
                            else if (secondarySpell.spellId == "automa")
                            {
                                _laurie.UpdateAbilityIcons(slot, sprites.VoltAutoma);
                            }

                            break;
                    }
                    break;
            }
        }

        public void Update() 
        {
            if (_laurie.partyMemberState != PartyMemberState.CurrentLeader) 
            {
                return;
            }
            
            switch (_laurie.primarySpellElement) 
            {
                case PrimarySpellElement.Arcane:
                    primaryElement = Elements.Arcane;

                    switch (_laurie.primarySpellType) 
                    {
                        case PrimarySpellType.Automa:
                            SetSpell(0, spells[2]);
                            break;
                        case PrimarySpellType.Blasteur:
                            SetSpell(0, spells[1]);
                            break;
                        case PrimarySpellType.Burston:
                            SetSpell(0, spells[0]);
                            break;
                    }
                break;
                case PrimarySpellElement.Pyro:
                    primaryElement = Elements.Pyro;

                    switch (_laurie.primarySpellType) 
                    {
                        case PrimarySpellType.Automa:
                            SetSpell(0, spells[2]);
                            break;
                        case PrimarySpellType.Blasteur:
                            SetSpell(0, spells[1]);
                            break;
                        case PrimarySpellType.Burston:
                            SetSpell(0, spells[0]);
                            break;
                    }
                break;
                case PrimarySpellElement.Cryo:
                    primaryElement = Elements.Cryo;

                    switch (_laurie.primarySpellType) 
                    {
                        case PrimarySpellType.Automa:
                            SetSpell(0, spells[2]);
                            break;
                        case PrimarySpellType.Blasteur:
                            SetSpell(0, spells[1]);
                            break;
                        case PrimarySpellType.Burston:
                            SetSpell(0, spells[0]);
                            break;
                    }
                break;
                case PrimarySpellElement.Toxi:
                    primaryElement = Elements.Toxi;

                    switch (_laurie.primarySpellType) 
                    {
                        case PrimarySpellType.Automa:
                            SetSpell(0, spells[2]);
                            break;
                        case PrimarySpellType.Blasteur:
                            SetSpell(0, spells[1]);
                            break;
                        case PrimarySpellType.Burston:
                            SetSpell(0, spells[0]);
                            break;
                    }
                break;
                case PrimarySpellElement.Volt:
                    primaryElement = Elements.Volt;

                    switch (_laurie.primarySpellType) 
                    {
                        case PrimarySpellType.Automa:
                            SetSpell(0, spells[2]);
                            break;
                        case PrimarySpellType.Blasteur:
                            SetSpell(0, spells[1]);
                            break;
                        case PrimarySpellType.Burston:
                            SetSpell(0, spells[0]);
                            break;
                    }
                break;   
            }

            switch (_laurie.secondarySpellElement) 
            {
                case SecondarySpellElement.Arcane:
                    secondaryElement = Elements.Arcane;

                    switch (_laurie.secondarySpellType) 
                    {
                        case SecondarySpellType.Automa:
                            SetSpell(1, spells[2]);
                            break;
                        case SecondarySpellType.Blasteur:
                            SetSpell(1, spells[1]);
                            break;
                        case SecondarySpellType.Burston:
                            SetSpell(1, spells[0]);
                            break;
                    }
                break;
                case SecondarySpellElement.Pyro:
                    secondaryElement = Elements.Pyro;

                    switch (_laurie.secondarySpellType) 
                    {
                        case SecondarySpellType.Automa:
                            SetSpell(1, spells[2]);
                            break;
                        case SecondarySpellType.Blasteur:
                            SetSpell(1, spells[1]);
                            break;
                        case SecondarySpellType.Burston:
                            SetSpell(1, spells[0]);
                            break;
                    }
                break;
                case SecondarySpellElement.Cryo:
                    secondaryElement = Elements.Cryo;

                    switch (_laurie.secondarySpellType) 
                    {
                        case SecondarySpellType.Automa:
                            SetSpell(1, spells[2]);
                            break;
                        case SecondarySpellType.Blasteur:
                            SetSpell(1, spells[1]);
                            break;
                        case SecondarySpellType.Burston:
                            SetSpell(1, spells[0]);
                            break;
                    }
                break;
                case SecondarySpellElement.Toxi:
                    secondaryElement = Elements.Toxi;

                    switch (_laurie.secondarySpellType) 
                    {
                        case SecondarySpellType.Automa:
                            SetSpell(1, spells[2]);
                            break;
                        case SecondarySpellType.Blasteur:
                            SetSpell(1, spells[1]);
                            break;
                        case SecondarySpellType.Burston:
                            SetSpell(1, spells[0]);
                            break;
                    }
                break;
                case SecondarySpellElement.Volt:
                    secondaryElement = Elements.Volt;

                    switch (_laurie.secondarySpellType) 
                    {
                        case SecondarySpellType.Automa:
                            SetSpell(1, spells[2]);
                            break;
                        case SecondarySpellType.Blasteur:
                            SetSpell(1, spells[1]);
                            break;
                        case SecondarySpellType.Burston:
                            SetSpell(1, spells[0]);
                            break;
                    }
                break;    
            }

            if (_laurie.ManaPointsAfterUse(primarySpell.cost) >= 0f) 
            {
                _laurie.UpdateAbilityIconLock(0, true);
            }
            else 
            {
                _laurie.UpdateAbilityIconLock(0, false);
            }
            if (_laurie.ManaPointsAfterUse(secondarySpell.cost) >= 0f)
            {
                _laurie.UpdateAbilityIconLock(1, true);
            }
            else
            {
                _laurie.UpdateAbilityIconLock(1, false);
            }

            _laurie.UpdateAbilityIconCooldown(0, primarySpell.cooldownTime, primarySpell.cooldown);
            _laurie.UpdateAbilityIconCooldown(1, secondarySpell.cooldownTime, secondarySpell.cooldown);
        }

        public void PrimaryCast() 
        {
            if (primarySpell != null) 
            {
                var direction = (_cursor.transform.position - _laurie.transform.position).normalized;
                if (_laurie.ManaPointsAfterUse(primarySpell.cost) >= 0f) 
                {
                    if (!primarySpell.coolingDown) 
                    {
                        _laurie.UseMana(primarySpell.cost);
                        primarySpell.Cast(direction, primaryElement);
                    }
                }
                else
                {
                    // no more mana :(
                }
            }
        }

        public void SecondaryCast() 
        {
            if (secondarySpell != null) 
            {
                var direction = (_cursor.transform.position - _laurie.transform.position).normalized;
                if (_laurie.ManaPointsAfterUse(secondarySpell.cost) >= 0f)
                {
                    if (!secondarySpell.coolingDown) 
                    {
                        _laurie.UseMana(secondarySpell.cost);
                        secondarySpell.Cast(direction, secondaryElement);
                    }
                }
                else
                {
                    // no more mana :(
                }
            }
        }
    }
}
