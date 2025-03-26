using UnityEngine;



[CreateAssetMenu(fileName = "Thunder Strike Effect", menuName = "Data/Effect/ThunderStrike")]

public class ThunderStrikeEffect : ItemEffect
{
    [SerializeField] private GameObject thunderStrikePrefab;

    public override void ExecuteEffet(Transform _enemyPositon)
    {
        GameObject newThunderStrike = Instantiate(thunderStrikePrefab,_enemyPositon.position,Quaternion.identity);

        Destroy(newThunderStrike,1);
    }
}
