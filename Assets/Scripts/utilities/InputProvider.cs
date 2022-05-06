using UnityEngine;

[CreateAssetMenuAttribute]
public class InputProvider : ScriptableObject, IInputProvider {
    public InputState inputState;

    public InputState GetState() {
        return inputState;
    }
}
