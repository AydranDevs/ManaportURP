using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimarySpell : MonoBehaviour {
    public event EventHandler<OnPrimaryFireEventArgs> OnPrimaryFire;
    public class OnPrimaryFireEventArgs : EventArgs {
        public string type;
        public string element; 
    }

    private Player player;
    private GameStateManager gameStateManager;

    private Blasteur blasteur;
    private Burston burston;
    private Automa automa;

    private string type;
    private string element;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gameStateManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameStateManager>();

        automa = GetComponent<Automa>();
        burston = GetComponent<Burston>();
        blasteur = GetComponent<Blasteur>();
    }

    /* This doesnt have to happen in LateUpdate()! Planning 
    to call the functions here when a spellstone change is 
    detected. For now it stays in LateUpdate. */

    private void LateUpdate() {
        if (gameStateManager.state == GameState.Main) {
            CheckSpellType();
            CheckElementType();
        }
    }

    // Checks Primary Spell Type enum in Player.cs whenever this function is called
    
    private void CheckSpellType() {
        if (player.primary == PrimarySpellType.Automa) {
            type = "Automa";
        }else if (player.primary == PrimarySpellType.Blasteur) {
            type = "Blasteur";
        }else if (player.primary == PrimarySpellType.Burston) {
            type = "Burston";
        }
    }

    // Check Primary Spell Element enum in Player.cs whenever this function is called

    private void CheckElementType() {
        if (player.primaryElement == PrimarySpellElement.Pyro) {
            element = "Pyro";
        }else if (player.primaryElement == PrimarySpellElement.Cryo) {
            element = "Cryo";
        }else if (player.primaryElement == PrimarySpellElement.Toxi) {
            element = "Toxi";
        }else if (player.primaryElement == PrimarySpellElement.Bolt) {
            element = "Bolt";
        }else { // Assumes element type as "Arcane" if not overriden by the other elements. (This may be a little small brained)
            element = "Arcane";
        }
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            // Player used Primary spell
            OnPrimaryFire?.Invoke(this, new OnPrimaryFireEventArgs { type = type, element = element });
        }
    }
}
