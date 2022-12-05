using System;
using UnityEngine;

[CreateAssetMenuAttribute]
public class InputProvider : ScriptableObject, IInputProvider {
    public InputState inputState;
    
    public event Action OnPrimary;
    public event Action OnSecondary;
    public event Action OnAux;

    public event Action OnNext;
    public event Action OnPrevious;

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
    
    public void InvokeNext() {
        Next();
    }
    protected virtual void Next() {
        OnNext?.Invoke();
    }
    public void InvokePrevious() {
        Previous();
    }
    protected virtual void Previous() {
        OnPrevious?.Invoke();
    }
}
