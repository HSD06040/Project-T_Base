using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public Inventory inventory { get; private set; }
    [SerializeField] private Transform itemSlotParent;
    private UI_ItemSlot[] inventorySlot;

    private void Awake()
    {
        inventory = ScriptableObject.CreateInstance<Inventory>();
        inventorySlot = itemSlotParent.GetComponentsInChildren<UI_ItemSlot>();
    }

    private void UpdateSlotUI()
    {
        for(int  i = 0; i < inventorySlot.Length; i++)
        {
            inventorySlot[i].CleanUpSlot();
        }
        for (int i = 0; i < inventory.inventoryList.Count; i++)
        {
            inventorySlot[i].UpdateSlot(inventory.inventoryList[i]);
        }
    }

    public void AddItem(ItemData _data)
    {
        if (_data.itemType == ItemType.Equipment)
            inventory.AddToInventory(_data);

        UpdateSlotUI();
    }

    public void RemoveItem(ItemData _itemData)
    {
        if (inventory.inventoryDictionary.TryGetValue(_itemData, out InventoryItem item))
        {
            if (item.stack > 1)
            {
                item.RemoveStack();
            }
            else
            {
                inventory.inventoryList.Remove(item);
                inventory.inventoryDictionary.Remove(_itemData);
            }
        }
    }
}

