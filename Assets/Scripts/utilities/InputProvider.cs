using System;
using UnityEngine;

[CreateAssetMenuAttribute]
public class InputProvider : ScriptableObject, IInputProvider {
    public InputState inputState;
    
    public event Action OnPrimary;
    public event Action OnSecondary;
    public event Action OnAux;

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

    public void InvokeAux() {
        Aux();
    }
    protected virtual void Aux() {
        OnAux?.Invoke();
    }
}
