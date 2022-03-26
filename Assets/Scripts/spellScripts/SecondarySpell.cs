using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondarySpell : MonoBehaviour {
    public event EventHandler<OnSecondaryFireEventArgs> OnSecondaryFire;
    public class OnSecondaryFireEventArgs : EventArgs {
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

    // Checks Secondary Spell Type enum in Player.cs whenever this function is called
    
    private void CheckSpellType() {
        if (player.secondary == SecondarySpellType.Automa) {
            type = "Automa";
        }else if (player.secondary == SecondarySpellType.Blasteur) {
            type = "Blasteur";
        }else if (player.secondary == SecondarySpellType.Burston) {
            type = "Burston";
        }
    }

    // Check Secondary Spell Element enum in Player.cs whenever this function is called

    private void CheckElementType() {
        if (player.secondaryElement == SecondarySpellElement.Pyro) {
            element = "Pyro";
        }else if (player.secondaryElement == SecondarySpellElement.Cryo) {
            element = "Cryo";
        }else if (player.secondaryElement == SecondarySpellElement.Toxi) {
            element = "Toxi";
        }else if (player.secondaryElement == SecondarySpellElement.Bolt) {
            element = "Bolt";
        }else { // Assumes element type as "Arcane" if not overriden by the other elements. (This may be a little small brained)
            element = "Arcane";
        }
    }

    private void Update() {
        if (Input.GetMouseButtonDown(1)) {
            // Player used Secondary spell
            OnSecondaryFire?.Invoke(this, new OnSecondaryFireEventArgs { type = type, element = element });
        }
    }
}
