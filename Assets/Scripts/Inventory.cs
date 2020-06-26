using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> items;
    [SerializeField] private Transform itemsParent;
    [SerializeField] private ItemSlot[] itemsSlot;

    public event Action<Item> OnItemRightClickedEvent;

    private void Awake()
    {
        for (int i = 0; i < itemsSlot.Length; i++)
        {
            itemsSlot[i].OnRightClickEvent += OnItemRightClickedEvent;
        }
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
            itemsSlot[i].Item = items[i];
        }

        for (; i < itemsSlot.Length; i++)
        {
            itemsSlot[i].Item = null;
        }
    }

    public bool AddItem(Item item)
    {
        if (IsFull())
            return false;

        items.Add(item);
        RefreshItemsUI();
        return true;
    }

    public bool RemoveItem(Item item)
    {
        if(items.Remove(item))
        {
            RefreshItemsUI();
            return true;
        }

        return false;
    }

    public bool IsFull()
    {
        return items.Count >= itemsSlot.Length;
    }
}
