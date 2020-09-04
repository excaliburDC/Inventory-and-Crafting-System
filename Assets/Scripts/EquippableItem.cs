using UnityEngine;
using InventorySystem.StatsMod;

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
    public float vitalityPercentBonus;

    [Space]
    [Header("Type of Equipment")]
    public EquipmentType equipmentType;

    public override Item GetItemCopy()
    {
        return Instantiate(this);

    }

    public override void DestroyItem()
    {
        Destroy(this);
    }

    public void EquipStat(Character c)
    {
        if (strengthBonus != 0)
            c.strength.AddModifier(new StatsModifier(strengthBonus, StatModType.Flat, this));
        if (intelligenceBonus != 0)
            c.intelligence.AddModifier(new StatsModifier(intelligenceBonus, StatModType.Flat, this));
        if (charismaBonus != 0)
            c.charisma.AddModifier(new StatsModifier(charismaBonus, StatModType.Flat, this));
        if (vitalityBonus != 0)
            c.vitality.AddModifier(new StatsModifier(vitalityBonus, StatModType.Flat, this));

        if (strengthPercentBonus != 0)
            c.strength.AddModifier(new StatsModifier(strengthPercentBonus, StatModType.PercentMult, this));
        if (intelligencePercentBonus != 0)
            c.intelligence.AddModifier(new StatsModifier(intelligencePercentBonus, StatModType.PercentMult, this));
        if (charismaPercentBonus != 0)
            c.charisma.AddModifier(new StatsModifier(charismaPercentBonus, StatModType.PercentMult, this));
        if (vitalityPercentBonus != 0)
            c.vitality.AddModifier(new StatsModifier(vitalityPercentBonus, StatModType.PercentMult, this));

    }

    public void UnequipStat(Character c)
    {
        c.strength.RemoveAllModifiersFromSource(this);
        c.intelligence.RemoveAllModifiersFromSource(this);
        c.charisma.RemoveAllModifiersFromSource(this);
        c.vitality.RemoveAllModifiersFromSource(this);
    }
}
