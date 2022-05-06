using UnityEngine;

[System.Serializable]
public struct InputState {
    public Vector2 movementDirection;
    public bool isSprinting;
}

public interface IInputProvider {
    public InputState GetState();
}
