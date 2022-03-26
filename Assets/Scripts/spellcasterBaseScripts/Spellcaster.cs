using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellcaster : MonoBehaviour {
    private Player player;
    private Controls controls;
    private GameStateManager gameStateManager;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        controls = GameObject.FindGameObjectWithTag("Player").GetComponent<Controls>();
        gameStateManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameStateManager>();
    }

    private void Update() {
        if (gameStateManager.state == GameState.Main) {
            HandleSpellInputs();
        }
    }

    public void HandleSpellInputs() {
        if (controls.buttonSpellcastPrimary) {
            player.ability = AbilityState.SpellcastPrimary;
        }

        if (controls.buttonSpellcastSecondary) {
            player.ability = AbilityState.SpellcastSecondary;
        }
    }

    
}
