using UnityEngine;


[CreateAssetMenu(fileName = "Freeze enemise effect", menuName = "Data/Effect/Freeze enemise")]
public class FreezeEnemise_Effect : ItemEffect
{
    [SerializeField] private float duration;

    public override void ExecuteEffet(Transform _transform)
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        if (playerStats.currentHealth > playerStats.GetMaxHealthValue() * .1f)
            return;

        if (!Inventory.Instance.CanUseArmor())
            return;

        Collider2D[] collider = Physics2D.OverlapCircleAll(_transform.position, 2);

        foreach (var hit in collider)
        {
            hit.GetComponent<Enemy>()?.FreezeTimerFor(duration);
        }
    }


}
