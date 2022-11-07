using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModifier
{
    int AddValue();
}

public delegate void ModifiedEvent();

[System.Serializable]
public class ModifiableInt
{
    [SerializeField]
    private int _baseValue;
    public int baseValue
    {
        get
        { 
            return _baseValue;
        } 
        set 
        {
            _baseValue = value; 
            UpdateModifiedValue();
        }
    }

    [SerializeField]
    private int _modifiedValue;
    public int modifiedValue
    {
        get 
        {
            return _modifiedValue;
        } 
        private set
        {
            _modifiedValue = value;
        }
    }

    public List<IModifier> modifiers = new List<IModifier>();

    public event ModifiedEvent ValueModified;
    public ModifiableInt(ModifiedEvent method = null)
    {
        _modifiedValue = baseValue;
        if (method != null)
        {
            ValueModified += method;
        }
    }

    public void Init()
    {
        modifiers = new List<IModifier>();
    }

    public void RegsiterModEvent(ModifiedEvent method)
    {
        ValueModified += method;
    }
    public void UnregsiterModEvent(ModifiedEvent method)
    {
        ValueModified -= method;
    }

    public void UpdateModifiedValue()
    {
        var valueToAdd = 0;
        for (int i = 0; i < modifiers.Count; i++)
        {
            valueToAdd += modifiers[i].AddValue();
        }

        modifiedValue = _baseValue + valueToAdd;
        if (ValueModified != null)
        {
            ValueModified.Invoke();
        }
    }

    public void AddModifier(IModifier _modifier)
    {
        modifiers.Add(_modifier);
        UpdateModifiedValue();
    }
    public void RemoveModifier(IModifier _modifier)
    {
        modifiers.Remove(_modifier);
        UpdateModifiedValue();
    }
}