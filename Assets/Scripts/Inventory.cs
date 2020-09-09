
using System.Collections.Generic;
using UnityEngine;

public class Inventory : ItemContainer
{
    [SerializeField] private List<Item> items;
    [SerializeField] private Transform itemsParent;


    protected override void OnValidate()
    {
        if (itemsParent != null)
        {
            itemsSlot = itemsParent.GetComponentsInChildren<ItemSlot>(includeInactive:true);
        }

        RefreshItemsUI();
    }


    protected override void Start()
    {
        base.Start();
        RefreshItemsUI();
    }

   
    private void RefreshItemsUI()
    {
        ClearItem();

        foreach (Item it in items)
        {
            AddItem(it.GetItemCopy());
        }

        //int i = 0;

        //for (; i < items.Count && i<itemsSlot.Length; i++)
        //{
        //    itemsSlot[i].Item = items[i].GetItemCopy();
        //    itemsSlot[i].ItemAmount = 1;
        //}

        //for (; i < itemsSlot.Length; i++)
        //{
        //    itemsSlot[i].Item = null;
        //    itemsSlot[i].ItemAmount = 0;
        //}
    }

   
}
