using UnityEngine;


[CreateAssetMenu(fileName = "Buff Effect", menuName = "Data/Effect/Buff")]

public class Buff_Effect : ItemEffect
{
    private PlayerStats stats;
    [SerializeField] StatType buffType;
    [SerializeField] int buffAmount;
    [SerializeField] private float buffDuration;

    public override void ExecuteEffet(Transform _enemyPositon)
    {
        stats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        stats.IncreaseStatBy(buffAmount, buffDuration, stats.GetStat(buffType));
    }

}
