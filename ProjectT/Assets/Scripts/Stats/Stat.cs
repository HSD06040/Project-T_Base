using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    [SerializeField] private int baseValue;
    public List<int> modifiers;
    
    public int GetValue()
    {
        int totalValue = baseValue;

        foreach (int modifier in modifiers)
        {
            totalValue += modifier;
        }

        return totalValue;
    }

    public void AddModifier(int value) => modifiers.Add(value);

    public void RemoveModifier(int value) => modifiers.Remove(value);

    public void SetBaseValue(int value)
    {
        baseValue = value;
    }
}
