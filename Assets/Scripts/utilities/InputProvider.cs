using System;
using UnityEngine;

[CreateAssetMenuAttribute]
public class InputProvider : ScriptableObject, IInputProvider {
    public InputState inputState;
    
    public event Action OnPrimary;
    public event Action OnSecondary;
    public event Action OnAuxMove;

    public InputState GetState() {
        return inputState;
    }

    public void InvokePrimary() {
        Primary();
    }
    protected virtual void Primary() {
        OnPrimary?.Invoke();
    }

    public void InvokeSecondary() {
        Secondary();
    }
    protected virtual void Secondary() {
        OnSecondary?.Invoke();
    }

    public void InvokeAuxMove() {
        AuxMove();
    }
    protected virtual void AuxMove() {
        OnAuxMove?.Invoke();
    }
}
