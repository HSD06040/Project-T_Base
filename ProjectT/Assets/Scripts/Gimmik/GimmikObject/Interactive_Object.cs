using UnityEngine;

public class Interactive_Object : MonoBehaviour, IGimmik
{
    [SerializeField] protected ItemData needItem;

    public void Activate()
    {
        if (needItem == null)
        {
            GimmikActivate();
            return;
        }

        bool itemFound = false;

        foreach (var item in InventoryManager.Instance.inventory.inventoryList)
        {
            if (item.data == needItem)
            {
                GimmikActivate();
                itemFound = true;
                break;
            }
        }

        if (!itemFound)
        {
            GimmikDeactivate();
        }
    }

    protected virtual void GimmikActivate()
    {
        
    }
    protected virtual void GimmikDeactivate()
    {
        
    }
}
