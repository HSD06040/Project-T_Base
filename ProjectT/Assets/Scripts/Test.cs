using UnityEngine;
using UnityEngine.TextCore.Text;

public class Test : MonoBehaviour
{
    public ItemData itemData;
    public IGimmik gimmik;
    public CharacterStats myStats;
    public CharacterStats enemyStats;

    private void Awake()
    {
        myStats = GetComponent<CharacterStats>();    
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            InventoryManager.Instance.AddItem(itemData);

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gimmik = GetComponent<IGimmik>();
            gimmik.Activate();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            myStats.DoDamage(enemyStats);
        }
    }
}
