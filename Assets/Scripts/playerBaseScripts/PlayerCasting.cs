using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCasting : MonoBehaviour {
    private GameStateManager gameStateManager;
    private Player player;
    
    private GameObject cursor;
    private GameObject spells;

    public Spell primarySpell;
    public Spell secondarySpell;

    public string primaryElement = Elements.Arcane;
    public string secondaryElement = Elements.Volt;

    bool spellsFound;

    // public Image primarySpellImage;
    // public Image secondarySpellImage;

    private void Start() {
        gameStateManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameStateManager>();
        player = GetComponent<Player>();
        cursor = GameObject.FindGameObjectWithTag("Cursor");

        var spells = GetComponentsInChildren<Spell>();

        SetSpell(0, spells[1]);
        SetSpell(1, spells[0]);
    }

    public void SetSpell(int slot, Spell spell) {
        switch (slot) {
            case 0:
                primarySpell = spell;

                switch (primaryElement) {
                    case Elements.Arcane:
                        // primarySpellImage.sprite = spell.icons.arcane;
                        break;
                    case Elements.Pyro:
                        // primarySpellImage.sprite = spell.icons.pyro;
                        break;
                }

                break;
            case 1:
                secondarySpell = spell;

                switch (secondaryElement) {
                    case Elements.Arcane:
                        // secondarySpellImage.sprite = Spell.icons.arcane;
                        break;
                    case Elements.Pyro:
                        // secondarySpellImage.sprite = spell.icons.pyro;
                        break;
                }
                break;
        }
    }

    void Update() {
        var spells = GetComponentsInChildren<Spell>();

        switch (player.primaryElement) {
            case PrimarySpellElement.Arcane:
                primaryElement = Elements.Arcane;

                switch (player.primary) {
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

                switch (player.primary) {
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

                switch (player.primary) {
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

                switch (player.primary) {
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

                switch (player.primary) {
                    case PrimarySpellType.Automa:
                        SetSpell(1, spells[2]);
                        break;
                    case PrimarySpellType.Blasteur:
                        SetSpell(1, spells[1]);
                        break;
                    case PrimarySpellType.Burston:
                        SetSpell(1, spells[0]);
                        break;
                }
                break;
            break;    
        }

        switch (player.secondaryElement) {
            case SecondarySpellElement.Arcane:
                secondaryElement = Elements.Arcane;

                switch (player.secondary) {
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

                switch (player.secondary) {
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

                switch (player.secondary) {
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

                switch (player.secondary) {
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

                switch (player.secondary) {
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
            break;    
        }

        if (gameStateManager.state == GameState.Main) {
            if (Input.GetMouseButtonDown(0) && primarySpell != null) {
                var direction = (cursor.transform.position - transform.position).normalized;
                if (player.ManaPointsAfterUse(primarySpell.cost) >= 0f) {
                    if (!primarySpell.coolingDown) {
                        player.UseMana(primarySpell.cost);
                        primarySpell.Cast(direction, primaryElement);
                    }
                }else {
                    // Debug.Log("no more mana :(");
                }
            }

            if (Input.GetMouseButtonDown(1) && secondarySpell != null) {
                var direction = (cursor.transform.position - transform.position).normalized;
                if (player.ManaPointsAfterUse(secondarySpell.cost) >= 0f) {
                    if (!secondarySpell.coolingDown) {
                        player.UseMana(secondarySpell.cost);
                        secondarySpell.Cast(direction, secondaryElement);
                    }
                }else {
                    // Debug.Log("no more mana :(");
                }
            }
        }
    }
}
