using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class has been created separately so that we can 
//utilize it in other scenarios such as bank cash system or other item containing scenarios
//It is an abstract class and has all it's methods as virtual for added functionality
public abstract class ItemContainer : MonoBehaviour, IItemContainer
{

    [SerializeField] protected ItemSlot[] itemsSlot;

    public event Action<BaseItemSlot> OnItemPointerEnterEvent;
    public event Action<BaseItemSlot> OnItemPointerExitEvent;
    public event Action<BaseItemSlot> OnItemRightClickEvent;
    public event Action<BaseItemSlot> OnItemBeginDragEvent;
    public event Action<BaseItemSlot> OnItemEndDragEvent;
    public event Action<BaseItemSlot> OnItemDragEvent;
    public event Action<BaseItemSlot> OnItemDropEvent;

    protected virtual void OnValidate()
    {
        itemsSlot = GetComponentsInChildren<ItemSlot>(includeInactive:true);
    }

    protected virtual void Start()
    {
        for (int i = 0; i < itemsSlot.Length; i++)
        {
            itemsSlot[i].OnPointerEnterEvent += slot => OnItemPointerEnterEvent(slot);
            itemsSlot[i].OnPointerExitEvent += slot => OnItemPointerExitEvent(slot);
            itemsSlot[i].OnRightClickEvent += slot => OnItemRightClickEvent(slot);
            itemsSlot[i].OnBeginDragEvent += slot => OnItemBeginDragEvent(slot);
            itemsSlot[i].OnEndDragEvent += slot => OnItemEndDragEvent(slot);
            itemsSlot[i].OnDragEvent += slot => OnItemDragEvent(slot);
            itemsSlot[i].OnDropEvent += slot => OnItemDropEvent(slot);
        }
    }


    public virtual bool CanAddItem(Item item, int amount = 1)
    {
        int freeSpaces = 0;

        foreach (ItemSlot iSlot in itemsSlot)
        {
            if (iSlot.Item == null || iSlot.Item.ID == item.ID)
            {
                freeSpaces += item.maxStacks - iSlot.ItemAmount;
            }
        }
        return freeSpaces >= amount;
    }

    public virtual bool AddItem(Item item)
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
            if (itemsSlot[i].CanAddStack(item))
            {
                itemsSlot[i].Item = item;
                itemsSlot[i].ItemAmount++;
                return true;
            }
        }

        for (int i = 0; i < itemsSlot.Length; i++)
        {
            if (itemsSlot[i].Item == null)
            {
                itemsSlot[i].Item = item;
                itemsSlot[i].ItemAmount++;
                return true;
            }
        }

        return false;

        #endregion
    }

    public virtual bool RemoveItem(Item item)
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
                itemsSlot[i].ItemAmount--;

                return true;
            }
        }

        return false;

        #endregion
    }

    public virtual Item RemoveItem(string itemID)
    {
        for (int i = 0; i < itemsSlot.Length; i++)
        {
            Item item = itemsSlot[i].Item;
            if (item != null && item.ID == itemID)
            {
                itemsSlot[i].ItemAmount--;

                return item;
            }
        }

        return null;

    }

    public virtual int ItemCount(string itemID)
    {
        int numItems = 0;

        for (int i = 0; i < itemsSlot.Length; i++)
        {
            Item item = itemsSlot[i].Item;
            if (item != null && item.ID == itemID)
            {
                numItems += itemsSlot[i].ItemAmount;
            }
        }

        return numItems;
    }

    public void ClearItem()
    {
        for (int i = 0; i < itemsSlot.Length; i++)
        {
            if (itemsSlot[i].Item != null && Application.isPlaying)
            {
                itemsSlot[i].Item.DestroyItem();
            }
            itemsSlot[i].Item = null;
            itemsSlot[i].ItemAmount = 0;
        }
    }

    //public virtual bool IsFull()
    //{
    //    #region For Click and Equip Method
    //    //return items.Count >= itemsSlot.Length; 
    //    #endregion

    //    #region For Drag and Drop Method
    //    for (int i = 0; i < itemsSlot.Length; i++)
    //    {
    //        if (itemsSlot[i].Item == null)
    //        {
    //            return false;
    //        }
    //    }

    //    return true;

    //    #endregion
    //}

    //public virtual bool ContainsItem(Item item)
    //{
    //    for (int i = 0; i < itemsSlot.Length; i++)
    //    {
    //        if (itemsSlot[i].Item == item)
    //        {
    //            return true;
    //        }
    //    }

    //    return false;
    //}

}
