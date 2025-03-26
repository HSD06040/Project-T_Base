using UnityEngine;


[CreateAssetMenu(fileName = "Heal Effect", menuName = "Data/Effect/Heal")]

public class Heal_Effect : ItemEffect
{
    [SerializeField] private GameObject healPrefab;

    [Range(0f, 1f)]
    [SerializeField] private float healPercent;
    public override void ExecuteEffet(Transform _enemyPositon)
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        int healAmount = Mathf.RoundToInt(playerStats.GetMaxHealthValue() * healPercent);

        playerStats.IncreaseHealthBy(healAmount);
    }
}
