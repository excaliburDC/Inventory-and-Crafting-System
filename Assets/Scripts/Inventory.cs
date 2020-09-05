using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : ItemContainer
{
    [SerializeField] private List<Item> items;
    [SerializeField] private Transform itemsParent;
    

    public event Action<BaseItemSlot> OnItemPointerEnterEvent;
    public event Action<BaseItemSlot> OnItemPointerExitEvent;
    public event Action<BaseItemSlot> OnItemRightClickEvent;
    public event Action<BaseItemSlot> OnItemBeginDragEvent;
    public event Action<BaseItemSlot> OnItemEndDragEvent;
    public event Action<BaseItemSlot> OnItemDragEvent;
    public event Action<BaseItemSlot> OnItemDropEvent;

    private void Start()
    {
        for (int i = 0; i < itemsSlot.Length; i++)
        {
            itemsSlot[i].OnPointerEnterEvent += OnItemPointerEnterEvent;
            itemsSlot[i].OnPointerExitEvent += OnItemPointerExitEvent;
            itemsSlot[i].OnRightClickEvent += OnItemRightClickEvent;
            itemsSlot[i].OnBeginDragEvent += OnItemBeginDragEvent;
            itemsSlot[i].OnEndDragEvent += OnItemEndDragEvent;
            itemsSlot[i].OnDragEvent += OnItemDragEvent;
            itemsSlot[i].OnDropEvent += OnItemDropEvent;
        }

        RefreshItemsUI();
    }

    private void OnValidate()
    {
        if (itemsParent != null) 
        {
            itemsSlot = itemsParent.GetComponentsInChildren<ItemSlot>();
        }

        RefreshItemsUI();
    }
    private void RefreshItemsUI()
    {
        int i = 0;

        for (; i < items.Count && i<itemsSlot.Length; i++)
        {
            itemsSlot[i].Item = items[i].GetItemCopy();
            itemsSlot[i].ItemAmount = 1;
        }

        for (; i < itemsSlot.Length; i++)
        {
            itemsSlot[i].Item = null;
            itemsSlot[i].ItemAmount = 0;
        }
    }

   
}
