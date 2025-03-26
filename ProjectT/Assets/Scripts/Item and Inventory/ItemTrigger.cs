using UnityEngine;

public class ItemTrigger : MonoBehaviour
{
    private ItemObject myItemObject => GetComponentInParent<ItemObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if (collision.GetComponent<PlayerStats>().isDead)
                return;

            myItemObject.PickupItem();
        }
    }
}
