using System;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRecipeUI : MonoBehaviour
{
	[Header(" UI References")]
    [SerializeField] private RectTransform arrowParent;
    [SerializeField] private BaseItemSlot[] itemSlots;

    [Header("Public Variables")]
    public ItemContainer itemContainer;

    private CraftingRecipe craftingRecipe;
    public CraftingRecipe CraftingRecipe
    {
        get { return craftingRecipe; }
        set { SetCraftingRecipe(value); }
    }

    public event Action<BaseItemSlot> OnPointerEnterEvent;
    public event Action<BaseItemSlot> OnPointerExitEvent;

    private void OnValidate()
    {
        itemSlots = GetComponentsInChildren<BaseItemSlot>(includeInactive: true);
    }

    private void Start()
    {
        foreach (BaseItemSlot itemSlot in itemSlots)
        {
            itemSlot.OnPointerEnterEvent += slot => OnPointerEnterEvent(slot);
            itemSlot.OnPointerExitEvent += slot => OnPointerExitEvent(slot);
        }
    }

    public void OnCraftButtonClick()
    {
        if (craftingRecipe != null && itemContainer != null)
        {
            if (craftingRecipe.CanCraft(itemContainer)) 
            {
                if(!itemContainer.IsFull())
                {
                    craftingRecipe.CraftItem(itemContainer);
                }
                else 
                {
                    Debug.LogError("Inventory is Full...!!!");
                }
            }

            else
            {
                Debug.LogError("You don't have the required materials...!!!"); 
            }
        }
    }

    private void SetCraftingRecipe(CraftingRecipe newCraftingRecipe)
    {
        craftingRecipe = newCraftingRecipe;

        if (craftingRecipe != null)
        {
            int slotIndex = 0;
            slotIndex = SetSlots(craftingRecipe.materials, slotIndex);
            arrowParent.SetSiblingIndex(slotIndex);
            slotIndex = SetSlots(craftingRecipe.results, slotIndex);

            for (int i = slotIndex; i < itemSlots.Length; i++)
            {
                itemSlots[i].transform.parent.gameObject.SetActive(false);
            }

            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private int SetSlots(IList<ItemAmount> itemAmountList, int slotIndex)
    {
        for (int i = 0; i < itemAmountList.Count; i++, slotIndex++)
        {
            ItemAmount itemAmount = itemAmountList[i];
            BaseItemSlot itemSlot = itemSlots[slotIndex];

            itemSlot.Item = itemAmount.item;
            itemSlot.ItemAmount = itemAmount.amount;
            itemSlot.transform.parent.gameObject.SetActive(true);
        }
        return slotIndex;
    }
}
