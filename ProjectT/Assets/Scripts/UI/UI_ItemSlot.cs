using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemStack;

    public InventoryItem item;

    public void UpdateSlot(InventoryItem _newItem)
    {
        item = _newItem;

        itemIcon.color = Color.white;
        
        if (item != null)
        {
            itemIcon.sprite = item.data.icon;

            if (item.stack > 1)
            {
                itemStack.text = item.stack.ToString();
            }
            else itemStack.text = "";
        }
    }

    public void CleanUpSlot()
    {
        item = null;

        itemIcon.sprite = null;
        itemIcon.color = Color.clear;
        itemStack.text = "";
    }
}
