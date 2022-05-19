using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaurieNamespace {
    public class LaurieCasting : MonoBehaviour {
        private GameStateManager gameStateManager;
        private PrimarySpellImage primarySpellImage;
        private SecondarySpellImage secondarySpellImage;
        private Laurie laurie;
        
        private GameObject cursor;

        public Spell primarySpell;
        public Spell secondarySpell;
        public Spell[] spells;

        public string primaryElement = Elements.Arcane;
        public string secondaryElement = Elements.Arcane;

        bool spellsFound;

        private void Start() {
            gameStateManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameStateManager>();
            primarySpellImage = GameObject.FindGameObjectWithTag("PrimaryIcon").GetComponent<PrimarySpellImage>();
            secondarySpellImage = GameObject.FindGameObjectWithTag("SecondaryIcon").GetComponent<SecondarySpellImage>();
            laurie = GetComponentInParent<Laurie>();
            cursor = GameObject.FindGameObjectWithTag("Cursor");

            spells = GetComponentsInChildren<Spell>();

            SetSpell(0, spells[1]);
            SetSpell(1, spells[0]);
        }

        public void SetSpell(int slot, Spell spell) {
            switch (slot) {
                case 0:
                    primarySpell = spell;

                    switch (primaryElement) {
                        case Elements.Arcane:
                            primarySpellImage.SetIcon(primarySpell.icons.arcane);
                            break;
                        case Elements.Pyro:
                            primarySpellImage.SetIcon(primarySpell.icons.pyro);
                            break;
                        case Elements.Cryo:
                            primarySpellImage.SetIcon(primarySpell.icons.cryo);
                            break;
                        case Elements.Toxi:
                            primarySpellImage.SetIcon(primarySpell.icons.toxi);
                            break;
                        case Elements.Volt:
                            primarySpellImage.SetIcon(primarySpell.icons.volt);
                            break;
                    }
                    break;
                case 1:
                    secondarySpell = spell;

                    switch (secondaryElement) {
                        case Elements.Arcane:
                            secondarySpellImage.SetIcon(secondarySpell.icons.arcane);
                            break;
                        case Elements.Pyro:
                            secondarySpellImage.SetIcon(secondarySpell.icons.pyro);
                            break;
                        case Elements.Cryo:
                            secondarySpellImage.SetIcon(secondarySpell.icons.cryo);
                            break;
                        case Elements.Toxi:
                            secondarySpellImage.SetIcon(secondarySpell.icons.toxi);
                            break;
                        case Elements.Volt:
                            secondarySpellImage.SetIcon(secondarySpell.icons.volt);
                            break;
                    }
                    break;
            }
        }

        void Update() {
            switch (laurie.primarySpellElement) {
                case PrimarySpellElement.Arcane:
                    primaryElement = Elements.Arcane;

                    switch (laurie.primarySpellType) {
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

                    switch (laurie.primarySpellType) {
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

                    switch (laurie.primarySpellType) {
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

                    switch (laurie.primarySpellType) {
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

                    switch (laurie.primarySpellType) {
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

            switch (laurie.secondarySpellElement) {
                case SecondarySpellElement.Arcane:
                    secondaryElement = Elements.Arcane;

                    switch (laurie.secondarySpellType) {
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

                    switch (laurie.secondarySpellType) {
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

                    switch (laurie.secondarySpellType) {
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

                    switch (laurie.secondarySpellType) {
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

                    switch (laurie.secondarySpellType) {
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

            if (laurie.ManaPointsAfterUse(primarySpell.cost) >= 0f) {
                primarySpellImage.UpdateSpellLock(true);
            }else {
                primarySpellImage.UpdateSpellLock(false);
            }
            if (laurie.ManaPointsAfterUse(secondarySpell.cost) >= 0f) {
                secondarySpellImage.UpdateSpellLock(true);
            }else {
                secondarySpellImage.UpdateSpellLock(false);
            }

            primarySpellImage.UpdateCooldown(primarySpell.cooldownTime, primarySpell.cooldown);
            secondarySpellImage.UpdateCooldown(secondarySpell.cooldownTime, secondarySpell.cooldown);
        }

        public void PrimaryCast() {
            if (primarySpell != null) {
                var direction = (cursor.transform.position - transform.position).normalized;
                if (laurie.ManaPointsAfterUse(primarySpell.cost) >= 0f) {
                    if (!primarySpell.coolingDown) {
                        laurie.UseMana(primarySpell.cost);
                        primarySpell.Cast(direction, primaryElement);
                    }
                }else {
                    // no more mana :(
                }
            }
        }

        public void SecondaryCast() {
            if (secondarySpell != null) {
                var direction = (cursor.transform.position - transform.position).normalized;
                if (laurie.ManaPointsAfterUse(secondarySpell.cost) >= 0f) {
                    if (!secondarySpell.coolingDown) {
                        laurie.UseMana(secondarySpell.cost);
                        secondarySpell.Cast(direction, secondaryElement);
                    }
                }else {
                    // no more mana :(
                }
            }
        }
    }
}
