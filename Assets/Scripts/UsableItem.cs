using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Items/Usable Item")]
public class UsableItem : Item
{
    public bool isConsumable;

    public List<UsableItemEffect> itemEffects;

    public virtual void UseItem(Character character)
    {
        foreach (UsableItemEffect effect in itemEffects)
        {
            effect.UseEffect(this, character);
        }
    }

    public override string GetItemType()
    {
        return isConsumable ? "Consumable" : "Usable";
    }

    public override string GetItemDescription()
    {
        sb.Length = 0;
        foreach (UsableItemEffect effect in itemEffects)
        {
            sb.AppendLine(effect.GetDescription());
        }
        return sb.ToString();
    }
}
