using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {
    private GameStateManager gameStateManager; 

    public bool buttonSpellcastPrimary = false;
    public bool buttonSpellcastSecondary = false;
    public bool buttonAuxilaryMovement = false;
    public bool buttonInventory = false;
    public bool buttonEquipment = false;
    public bool buttonBestiary = false;
    public bool buttonSlot1 = false;
    public bool buttonSlot2 = false;
    public bool buttonPause = false;
    public bool buttonMap = false;

    private void Awake() {
        gameStateManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameStateManager>();
    }

    private void Update() {
        if (gameStateManager.state == GameState.Main) {
            HandleInputs();
        }
    }

    // Control Name      | PC           | Playstation | XBox 
    // ------------------+--------------+-------------+-------
    // primary default   : left click;   R1;           RB
    // secondary default : right click;   L1;           LB
    // auxilary default  : spacebar;     X;            A
    // inv default       : B;            triangle;     Y
    // equip default     : E;            square;       X
    // bestiary default  : R;            circle;       B
    // slot1 default     : 1;            R2;           RT
    // slot2 default     : 2;            L2;           LT
    // pause default     : esc;          options;      menu
    // map default       : tab;          touchpad;     view

    /* There is more than likely a better way to handle this... Oh well.
    I did my best, I plan to improve it later on when i know a lot more
    about what I'm doing. */

    public void HandleInputs() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            buttonAuxilaryMovement = true;
        }else if (Input.GetKeyUp(KeyCode.Space)) {
            buttonAuxilaryMovement = false;
        }

        if (Input.GetMouseButtonDown(0)) {
            buttonSpellcastPrimary = true;
        }else if (Input.GetMouseButtonUp(0)) {
            buttonSpellcastPrimary = false;
        }

        if (Input.GetMouseButtonDown(1)) {
            buttonSpellcastSecondary = true;
        }else if (Input.GetMouseButtonUp(1)) {
            buttonSpellcastSecondary = false;
        }
    }
}