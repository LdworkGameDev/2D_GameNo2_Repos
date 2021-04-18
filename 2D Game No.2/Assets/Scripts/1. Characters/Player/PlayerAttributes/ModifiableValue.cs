using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModifiableValue 
{

    public List<IModifier> modifiers = new List<IModifier>();

    [SerializeField] private int baseValue;
    public int BaseValue { set { baseValue = value; /*UpdateModifiedValue(); */} get { return baseValue; } }
    [SerializeField] private int modifiedValue;
    public int ModifiedValue { set { modifiedValue = value; } get { return modifiedValue; } }

    public ModifiableValue(int baseValue)
    {
        BaseValue = baseValue;
        modifiedValue = BaseValue;
    }

    public void UpdateModifiedValue()
    {
        var valueToAdd = 0;
        for (int i = 0; i < modifiers.Count; i++)
        {
            modifiers[i].AddValue(ref valueToAdd);
        }
        ModifiedValue = baseValue + valueToAdd;    }

    public void AddModifier(IModifier modifier_)
    {
        modifiers.Add(modifier_);
        UpdateModifiedValue();
    }

    public void RemoveModifier(IModifier modifier_)
    {
        modifiers.Remove(modifier_);
        UpdateModifiedValue();
    }
}
