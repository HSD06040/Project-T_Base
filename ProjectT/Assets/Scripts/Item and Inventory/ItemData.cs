using System.Text;
using UnityEditor;


#if UNITY_EDITOR
using UnityEngine;
#endif

public enum ItemType
{
    Material,
    Equipment
}


[CreateAssetMenu(fileName ="New Item Data",menuName ="Data/Item")]

public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public Sprite icon;
    public string itemId;

    [Range(0f, 100f)]
    public float dropChance;

    protected StringBuilder sb = new StringBuilder();

    private void OnValidate()
    {
#if UNITY_EDITOR
        string path = AssetDatabase.GetAssetPath(this);
        itemId = AssetDatabase.AssetPathToGUID(path);
#endif
    }

    public virtual string GetDescription()
    {
        return "";
    }
}
