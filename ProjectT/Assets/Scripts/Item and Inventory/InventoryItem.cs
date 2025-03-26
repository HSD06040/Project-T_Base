using System;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public ItemData data;
    public int stackSize;

    public InventoryItem (ItemData _newitemdata)
    {
        data = _newitemdata;
        AddStack();
    }

    public void AddStack() => stackSize++;

    public void RemoveStack() => stackSize--;
}
