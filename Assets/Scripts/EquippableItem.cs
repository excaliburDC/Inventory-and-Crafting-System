using UnityEngine;

public enum EquipmentType
{
    Helmet,
    ChestArmor,
    Gloves,
    Boots,
    PrimaryWeapon,
    SecondaryWeapon,
    Accessory1,
    Accessory2
}

[CreateAssetMenu(menuName = "Inventory/Equipments/Add Equipment")]
public class EquippableItem : Item
{
    [Header("Equipment Attributes")]
    public int strengthBonus;
    public int intelligenceBonus;
    public int charismaBonus;
    public int vitalityBonus;

    [Space]
    [Header("Equipment Attributes Percentage")]
    public float strengthPercentBonus;
    public float intelligencePercentBonus;
    public float charismaPercentBonus;
    public float vitaityPercentBonus;

    [Space]
    [Header("Type of Equipment")]
    public EquipmentType equipmentType;
}
