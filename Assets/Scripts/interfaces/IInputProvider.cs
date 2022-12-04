using System;
using UnityEngine;

[System.Serializable]
public struct InputState {
    public Vector2 movementDirection;
    public bool isSprinting;
    public bool isDashing;

    public Vector2 targetPos;
}

public interface IInputProvider {
    event Action OnPrimary;
    event Action OnSecondary;
    event Action OnAux;

    InputState GetState();
}
