using System;
using UnityEngine;

[System.Serializable]
public struct InputState {
    public Vector2 movementDirection;
    public bool isSprinting;

    public Vector2 targetPos;
}

public interface IInputProvider {
    public event Action OnPrimary;
    public event Action OnSecondary;
    public event Action OnAux;

    public InputState GetState();
}
