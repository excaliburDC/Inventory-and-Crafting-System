using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> items;
    [SerializeField] private Transform itemsParent;
    [SerializeField] private ItemSlot[] itemsSlot;

    public event Action<ItemSlot> OnItemPointerEnterEvent;
    public event Action<ItemSlot> OnItemPointerExitEvent;
    public event Action<ItemSlot> OnItemRightClickEvent;
    public event Action<ItemSlot> OnItemBeginDragEvent;
    public event Action<ItemSlot> OnItemEndDragEvent;
    public event Action<ItemSlot> OnItemDragEvent;
    public event Action<ItemSlot> OnItemDropEvent;

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
            itemsSlot[i].Item = items[i];
        }

        for (; i < itemsSlot.Length; i++)
        {
            itemsSlot[i].Item = null;
        }
    }

    public bool AddItem(Item item)
    {
        #region For Click and Equip Method
        //if (IsFull())
        //    return false;

        //items.Add(item);
        //RefreshItemsUI();
        //return true;
        #endregion

        #region For Drag and Drop Method
        for (int i = 0; i < itemsSlot.Length; i++)
        {
            if(itemsSlot[i].Item==null)
            {
                itemsSlot[i].Item = item;
                return true;
            }
        }

        return false;

        #endregion
    }

    public bool RemoveItem(Item item)
    {
        #region For Click and Equip Method
        //if(items.Remove(item))
        //{
        //    RefreshItemsUI();
        //    return true;
        //}

        //return false; 
        #endregion

        #region For Drag and Drop Method
        for (int i = 0; i < itemsSlot.Length; i++)
        {
            if (itemsSlot[i].Item == item)
            {
                itemsSlot[i].Item = null;
                return true;
            }
        }

        return false;

        #endregion
    }

    public bool IsFull()
    {
        #region For Click and Equip Method
        //return items.Count >= itemsSlot.Length; 
        #endregion

        #region For Drag and Drop Method
        for (int i = 0; i < itemsSlot.Length; i++)
        {
            if (itemsSlot[i].Item == null)
            {
                return false;
            }
        }

        return true;

        #endregion
    }
}
