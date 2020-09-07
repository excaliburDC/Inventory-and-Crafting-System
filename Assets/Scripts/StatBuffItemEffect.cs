using System.Collections;
using UnityEngine;
using InventorySystem.StatsMod;

public enum StatType
{
    Strength,
    Intelligence,
    Charisma,
    Vitality
}


[CreateAssetMenu(menuName = "Inventory/Item Effects/Temporary Stats Buff")]
public class StatBuffItemEffect : UsableItemEffect
{
    //public int AgilityBuff;
    // public float Duration;

    public StatType statType;
    public int buffAmount;
    public float duration;

    public override void UseEffect(UsableItem parentItem, Character character)
    {
        switch (statType)
        {
            case StatType.Strength:
                AddTempBuff(parentItem, character);
                break;

            case StatType.Intelligence:
                AddTempBuff(parentItem, character);
                break;

            case StatType.Charisma:
                AddTempBuff(parentItem, character);
                break;

            case StatType.Vitality:
                AddTempBuff(parentItem, character);
                break;

            default:
                Debug.LogError("No Such Stat Exists...");
                break;
        }


    }

    public override string GetDescription()
    {
        return "Grants " + buffAmount + " " + statType.ToString() + " for " + duration + " seconds";
    }

    private void AddTempBuff(UsableItem parentItem, Character ch)
    {
        StatsModifier statModifier = new StatsModifier(buffAmount, StatModType.Flat, parentItem);
        ch.strength.AddModifier(statModifier);
        ch.UpdateStatsValue();
        ch.StartCoroutine(RemoveBuff(ch, statModifier, duration));
    }

    private static IEnumerator RemoveBuff(Character character, StatsModifier statModifier, float duration)
    {
        yield return new WaitForSeconds(duration);
        character.strength.RemoveModifier(statModifier);
        character.UpdateStatsValue();
    }




}
