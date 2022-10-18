using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
    Author: Aydranna Walker

    This program handles the debug console of a larger game. using user input as a string and
    parses parts of that string into usable data that the program can use to change attributes
    of the player.

    This program uses the Unity API and some Unity Libraries.
*/

public class DebugController : MonoBehaviour {
    bool showConsole;
    bool showHelp;
    bool showPosition;

    string input; // holds what the user types
    string position;

    public static DebugCommand HELP;
    public static DebugCommand KILL_ALL;
    public static DebugCommand<float> SET_MAX_HP;
    public static DebugCommand MAX_HP;
    public static DebugCommand<float> SET_MAX_MP;
    public static DebugCommand MAX_MP;
    public static DebugCommand<int> SET_PRIMARY_SPELL;
    public static DebugCommand<int> SET_PRIMARY_ELEMENT;
    public static DebugCommand<int> SET_SECONDARY_SPELL;
    public static DebugCommand<int> SET_SECONDARY_ELEMENT;
    public static DebugCommand GET_POSITION;
    // public static DebugCommand<Vector2> TELEPORT;

    public List<object> commandList; // commandList holds every command that may be used

    // called when user presses enter on keyboard
    public void OnEnter() {
        if (showConsole) {
            HandleInput();
            input = "";
        }
    }

    // called when user presses grave (`) on keyboard
    public void OnToggleDebug() {
        showConsole = !showConsole;
    }

    private void Start() {
        HELP = new DebugCommand("help", "Shows a list of all available commands.", "help", () => {
            showHelp = true;
        });

        KILL_ALL = new DebugCommand("kill_all", "Removes all creatures from the scene.", "kill_all", () => {
            Creature.instance.KillAllCreatures();
        });

        SET_MAX_HP = new DebugCommand<float>("set_max_hp", "Sets player max HP.", "set_max_hp <hp_amount>", (x) => {
            // Player.instance.Debug_SetMaxHP(x);
        });

        MAX_HP = new DebugCommand("max_hp", "Gives player maximum HP.", "max_hp", () => {
            // Player.instance.Debug_MaxHP();
        });

        SET_MAX_MP = new DebugCommand<float>("set_max_mp", "Sets player max MP.", "set_max_mp <mp_amount>", (x) => {
            // Player.instance.Debug_SetMaxMP(x);
        });

        MAX_MP = new DebugCommand("max_mp", "Gives player maximum MP.", "max_mp", () => {
            // Player.instance.Debug_MaxMP();
        });

        SET_PRIMARY_SPELL = new DebugCommand<int>("set_primary_spell", "Sets primary spell to spell given.", "set_primary_spell <spell_number>", (x) => {
            // Player.instance.Debug_SetPrimarySpell(x);
        });

        SET_PRIMARY_ELEMENT = new DebugCommand<int>("set_primary_element", "Sets primary element to element given.", "set_primary_element <element_number>", (x) => {
            // Player.instance.Debug_SetPrimaryElement(x);
        });

        SET_SECONDARY_SPELL = new DebugCommand<int>("set_secondary_spell", "Sets secondary spell to spell given.", "set_secondary_spell <spell_number>", (x) => {
            // Player.instance.Debug_SetSecondarySpell(x);
        });

        SET_SECONDARY_ELEMENT = new DebugCommand<int>("set_secondary_element", "Sets secondary element to element given.", "set_secondary_element <element_number>", (x) => {
            // Player.instance.Debug_SetSecondaryElement(x);
        });

        GET_POSITION = new DebugCommand("get_position", "Shows player position.", "get_position", () => {
            // showPosition = true;

            // Vector3 playerPos = Player.instance.GetPosition();
            // position = playerPos.ToString("F3");
            // Debug.Log(playerPos);
        });

        // TELEPORT = new DebugCommand<Vector2>("teleport", "Teleports player to position given.", "teleport <position>", (x) => {
        //     Player.instance.gameObject.transform.position = (Vector3)x;
        // });

        // add all commands to commandList
        commandList = new List<object> {
            HELP,
            KILL_ALL,
            SET_MAX_HP,
            MAX_HP,
            SET_MAX_MP,
            MAX_MP,
            SET_PRIMARY_SPELL,
            SET_PRIMARY_ELEMENT,
            SET_SECONDARY_SPELL,
            SET_SECONDARY_ELEMENT,
            GET_POSITION,
            // TELEPORT
        };

        
    }

    Vector2 scroll;

    // Called by Unity API when GUI is to be drawn
    private void OnGUI() {
        if (!showConsole) {
            showPosition = false;
            return;
        }

        float y = 0f;

        if (showHelp) { // if user inputs "help" command, move console down by 100 and print commands from commandList
            GUI.Box(new Rect(0, y, Screen.width, 100), "");

            Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * commandList.Count);
            scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), scroll, viewport);

            for (int i=0; i<commandList.Count; i++) {
                DebugCommandBase command = commandList[i] as DebugCommandBase;

                string label = $"{command.commandFormat} - {command.commandDescription}";
                Rect labelRect = new Rect(5, 20 * i, viewport.width - 100, 20);
                GUI.Label(labelRect, label);
            }

            GUI.EndScrollView();

            y = y + 100;
        }

        if (showPosition) { // if user inputs "get_position" command, move console down by 20 and print Player position
            GUI.Box(new Rect(0, y, Screen.width, 20), position);
            GUI.backgroundColor = new Color(0, 0, 0, 0);

            y = y + 20;
        }

        // if showConsole is true, draw console at y = 0
        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);
    }
    
    private void HandleInput() {
        string[] properties = input.Split(' '); // string array holds every word or text broken by spaces

        // for every command in commandList, check command syntax and invoke proper function
        for (int i=0; i<commandList.Count; i++) {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase; // index commandBase according to commandList.Count 

            if (input.Contains(commandBase.commandId)) { // if input contains specified commandId (and additional arguments if necessary), invoke command
                if(commandList[i] is DebugCommand debugCommand){
                    debugCommand.Invoke();
                }else if (commandList[i] is DebugCommand<float> floatDebugCommand) {
                    floatDebugCommand.Invoke(float.Parse(properties[1]));
                }else if (commandList[i] is DebugCommand<int> intDebugCommand) {
                    intDebugCommand.Invoke(int.Parse(properties[1]));
                }// else if (commandList[i] is DebugCommand<Vector2> vector2DebugCommand) {
                //    vector2DebugCommand.Invoke(Vector2.Parse(properties[1]));
                // }
            }
        }
    }
}
