using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script, as it exsits now, might be better to just put in the PlayerMovement script instead.
public class PlayerInput : MonoBehaviour
{
    private GameStateManager gameStateManager;
    private PlayerMovement playerMovement;
    private Spellcaster spellcaster;
    private Player player;

    void Start()
    {
        gameStateManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameStateManager>();
        playerMovement = GetComponent<PlayerMovement>();
        player = GetComponent<Player>();
        spellcaster = GetComponent<Spellcaster>();
    }

    void Update()
    {
        if (gameStateManager.state == GameState.Main) {
            if (player.ability != AbilityState.AuxilaryMovement) {
                playerMovement.Move(Time.fixedDeltaTime);
            }
            // Spellcaster.RotateCursor();
        }
    }
}
