using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class ItemObject : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private ItemData itemdata;
    private void SetupVisuals()
    {
        if (itemdata == null)
            return;

        GetComponent<SpriteRenderer>().sprite = itemdata.icon;
        gameObject.name = "ItemObject -" + itemdata.itemName;
    }

    public void SetupItem(ItemData _itemData,Vector2 _velocity)
    {
        itemdata = _itemData;
        rb.linearVelocity = _velocity;

        SetupVisuals();
    }

    public void PickupItem()
    {
        if (!Inventory.Instance.CanAddItem() && itemdata.itemType == ItemType.Equipment)
        {
            rb.linearVelocity = new Vector2(0, 7);
            PlayerManager.instance.player.fx.CreatePopUpText("Inventory is full");
            return;
        }

        AudioManager.instance.PlaySFX(18,transform);
        Inventory.Instance.AddItem(itemdata);
        Destroy(gameObject);
    }
}
