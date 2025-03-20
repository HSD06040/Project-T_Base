using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public enum ItemType
{
    Equipment,
    Material,
    Interactive
}
public enum ItemGrade
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Unique,
    Legendary
}

[CreateAssetMenu(fileName = "ItemData",menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public ItemGrade itemGrade;
    public string itemName;
    public Sprite icon;
    public string itemID;
    [TextArea]
    public string itemDescription;

    protected StringBuilder sr = new StringBuilder();

    private void OnValidate()
    {
#if UNITY_EDITOR
        string path = AssetDatabase.GetAssetPath(this);
        itemID = AssetDatabase.AssetPathToGUID(path);
#endif
    }

    public virtual string GetDescription()
    {
        return "";
    }
}
