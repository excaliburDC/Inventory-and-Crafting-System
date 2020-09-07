
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item Effects/Heal")]
public class HealEffect : UsableItemEffect
{
    public int healAmount;
    public override void UseEffect(UsableItem parentItem, Character character)
    {
        character.health += healAmount;
    }

    public override string GetDescription()
    {
        return "Restores " + healAmount + " health.";
    }

    
}
