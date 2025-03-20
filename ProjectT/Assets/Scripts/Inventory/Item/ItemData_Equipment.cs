using UnityEngine;
public enum EquipmentType
{
    Weapon,
    Accessories,
    Armor,
    Boots
}

[CreateAssetMenu(fileName = "ItemData", menuName = "Data/Equipment")]
public class ItemData_Equipment : ItemData
{
    public EquipmentType equipmentType;

    [Header("Offensive stat")]
    public int damage;
    public int critChance;
    public int critDamage;

    [Header("Primary stat")]
    public int strength;
    public int agility;
    public int vitality;
    public int luck;

    [Header("Defensive stat")]
    public int maxHealth;
    public int defense;
}
