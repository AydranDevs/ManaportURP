using UnityEngine;

namespace Manapotion.PartySystem.LaurieCharacter 
{
    public class LaurieCasting
    {
        private GameStateManager _gameStateManager;
        private PrimarySpellImage _primarySpellImage;
        private SecondarySpellImage _secondarySpellImage;
        private Laurie _laurie;
        
        private GameObject _cursor;

        public Spell primarySpell;
        public Spell secondarySpell;
        public Spell[] spells;

        public string primaryElement = Elements.Arcane;
        public string secondaryElement = Elements.Arcane;

        bool spellsFound;

        public LaurieCasting(Laurie laurie)
        {
            _laurie = laurie;

            _gameStateManager = GameStateManager.Instance;
            _primarySpellImage = GameObject.FindGameObjectWithTag("PrimaryIcon").GetComponent<PrimarySpellImage>();
            _secondarySpellImage = GameObject.FindGameObjectWithTag("SecondaryIcon").GetComponent<SecondarySpellImage>();
            _cursor = GameObject.FindGameObjectWithTag("Cursor");

            spells = _laurie.GetComponentsInChildren<Spell>();

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
                            _primarySpellImage.SetIcon(primarySpell.icons.arcane);
                            break;
                        case Elements.Pyro:
                            _primarySpellImage.SetIcon(primarySpell.icons.pyro);
                            break;
                        case Elements.Cryo:
                            _primarySpellImage.SetIcon(primarySpell.icons.cryo);
                            break;
                        case Elements.Toxi:
                            _primarySpellImage.SetIcon(primarySpell.icons.toxi);
                            break;
                        case Elements.Volt:
                            _primarySpellImage.SetIcon(primarySpell.icons.volt);
                            break;
                    }
                    break;
                case 1:
                    secondarySpell = spell;

                    switch (secondaryElement) 
                    {
                        case Elements.Arcane:
                            _secondarySpellImage.SetIcon(secondarySpell.icons.arcane);
                            break;
                        case Elements.Pyro:
                            _secondarySpellImage.SetIcon(secondarySpell.icons.pyro);
                            break;
                        case Elements.Cryo:
                            _secondarySpellImage.SetIcon(secondarySpell.icons.cryo);
                            break;
                        case Elements.Toxi:
                            _secondarySpellImage.SetIcon(secondarySpell.icons.toxi);
                            break;
                        case Elements.Volt:
                            _secondarySpellImage.SetIcon(secondarySpell.icons.volt);
                            break;
                    }
                    break;
            }
        }

        public void Update() 
        {
            if (_laurie.party.partyLeader != PartyLeader.Laurie) 
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
                _primarySpellImage.UpdateSpellLock(true);
            }
            else 
            {
                _primarySpellImage.UpdateSpellLock(false);
            }
            if (_laurie.ManaPointsAfterUse(secondarySpell.cost) >= 0f)
            {
                _secondarySpellImage.UpdateSpellLock(true);
            }
            else
            {
                _secondarySpellImage.UpdateSpellLock(false);
            }

            _primarySpellImage.UpdateCooldown(primarySpell.cooldownTime, primarySpell.cooldown);
            _secondarySpellImage.UpdateCooldown(secondarySpell.cooldownTime, secondarySpell.cooldown);
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
