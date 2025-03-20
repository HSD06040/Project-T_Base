using System;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public ItemData data;
    public int stack;

    public InventoryItem(ItemData _newItemData)
    {
        data = _newItemData;
        AddStack();
    }

    public void AddStack() => stack++;

    public void RemoveStack() => stack--;
}
