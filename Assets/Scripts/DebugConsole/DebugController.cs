using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugController : MonoBehaviour {
    bool showConsole;

    public void OnToggleDebug(InputValue value) {
        showConsole = !showConsole;
    }

    private void OnGUI() {
        if (!showConsole) return;
    }
}
