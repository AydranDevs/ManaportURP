using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCasting : MonoBehaviour {
    private GameStateManager gameStateManager;
    
    private GameObject cursor;
    private GameObject spells;

    public Spell primarySpell;
    public Spell secondarySpell;

    private string primaryElement = Elements.Arcane;
    private string secondaryElement = Elements.Pyro;

    // public Image primarySpellImage;
    // public Image secondarySpellImage;

    private void Start() {
        gameStateManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameStateManager>();
        cursor = GameObject.FindGameObjectWithTag("Cursor");

        var spells = GetComponentsInChildren<Spell>();

        SetSpell(0, spells[1]);
        SetSpell(1, spells[2]);
    }

    void SetSpell(int slot, Spell spell)
    {
        switch (slot)
        {
            case 0:
                primarySpell = spell;

                switch (primaryElement)
                {
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

                switch (secondaryElement)
                {
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
        if (gameStateManager.state == GameState.Main) {
            if (Input.GetMouseButtonDown(0) && primarySpell != null) {
                var direction = (cursor.transform.position - transform.position).normalized;
                primarySpell.Cast(direction, primaryElement);
            }

            if (Input.GetMouseButtonDown(1) && secondarySpell != null) {
                var direction = (cursor.transform.position - transform.position).normalized;
                secondarySpell.Cast(direction, secondaryElement);
            }
        }
    }
}
