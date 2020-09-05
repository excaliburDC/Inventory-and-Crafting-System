using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class has been created separately so that we can 
//utilize it in other scenarios such as bank cash system or other item containing scenarios
//It is an abstract class and has all it's methods as virtual for added functionality
public abstract class ItemContainer : MonoBehaviour, IItemContainer
{

    [SerializeField] protected ItemSlot[] itemsSlot;

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
            if (itemsSlot[i].Item == null || itemsSlot[i].CanAddStack(item))
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

    public virtual bool IsFull()
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

    public virtual int ItemCount(string itemID)
    {
        int numItems = 0;

        for (int i = 0; i < itemsSlot.Length; i++)
        {
            if (itemsSlot[i].Item.ID == itemID)
            {
                numItems++;

            }
        }

        return numItems;
    }
}
