using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemDrop : ItemDrop
{
    [Header("Player's drop")]
    [SerializeField] private float chanceToLooseitems;
    [SerializeField] private float chanceToLooseMaterials;

    public override void GenerateDrop()
    {
        Inventory inventory = Inventory.Instance;

        List<InventoryItem> itemsToUnequip = new List<InventoryItem>();
        List<InventoryItem> materialsToLoose = new List<InventoryItem>();

        foreach (InventoryItem item in inventory.GetEquipmentList())
        {
            if (Random.Range(0,100) <= chanceToLooseitems)
            {
                DropItem(item.data);
                itemsToUnequip.Add(item);
            }
        }
        for (int i = 0; i < itemsToUnequip.Count; i++)
        {
            inventory.UnEquipItem(itemsToUnequip[i].data as ItemData_Equipment);         
        }


        foreach (InventoryItem item in inventory.GetStashList())
        {
            if (Random.Range(0,100) <= chanceToLooseMaterials)
            {
                DropItem(item.data);
                materialsToLoose.Add(item);
            }
        }

        for (int i = 0; i < materialsToLoose.Count; i++)
        {
            inventory.RemoveItem(materialsToLoose[i].data);
        }
    }
}
