using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemAmount
{
    public Item item;

    [Range(1,10)]
    public int amount;
}


[CreateAssetMenu(menuName = "Crafting")]
public class CraftingRecipe : ScriptableObject
{
    public List<ItemAmount> materials;

    public List<ItemAmount> results;

    

    public bool CanCraft(IItemContainer itemContainer)
    {
        return HasMaterials(itemContainer) && HasSpace(itemContainer); 

    }

    private bool HasMaterials(IItemContainer itemContainer)
    {
        foreach (ItemAmount itemAmt in materials)
        {
            if (itemContainer.ItemCount(itemAmt.item.ID) < itemAmt.amount)
            {
                Debug.LogWarning("You don't have the required materials.");
                return false;
            }

        }

        return true;
    }

    private bool HasSpace(IItemContainer itemContainer)
    {
        foreach (ItemAmount itemAmt in results)
        {
            if (!itemContainer.CanAddItem(itemAmt.item, itemAmt.amount))
            {
                Debug.LogWarning("Your inventory is full.");
                return false;
            }

        }

        return true;
    }

    

    public void CraftItem(IItemContainer itemContainer)
    {
        if(CanCraft(itemContainer))
        {
            foreach (ItemAmount itemAmt in materials)
            {
                for (int i = 0; i < itemAmt.amount; i++)
                {
                    Item oldItem = itemContainer.RemoveItem(itemAmt.item.ID);

                    oldItem.DestroyItem();
                }
            }

            foreach (ItemAmount itemAmt in results)
            {
                for (int i = 0; i < itemAmt.amount; i++)
                {
                    itemContainer.AddItem(Instantiate(itemAmt.item.GetItemCopy()));
                }
            }
        }
    }
}
