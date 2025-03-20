using System.Collections.Generic;
using UnityEngine;

public class Inventory : ScriptableObject
{
    public List<InventoryItem> inventoryList = new List<InventoryItem>();
    public Dictionary<ItemData, InventoryItem> inventoryDictionary = new Dictionary<ItemData, InventoryItem>();

    public void AddToInventory(ItemData _itemData)
    {
        if(inventoryDictionary.TryGetValue(_itemData,out InventoryItem item))
        {
            item.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_itemData);
            inventoryList.Add(newItem);
            inventoryDictionary.Add(_itemData,newItem);
        }
    }
}

