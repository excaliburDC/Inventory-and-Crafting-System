﻿using UnityEngine;
using UnityEngine.UI;
using InventorySystem.CharacterStats;
using System;

public class Character : MonoBehaviour
{
    public int health = 50;

    public CharacterStats strength;
    public CharacterStats intelligence;
    public CharacterStats charisma;
    public CharacterStats vitality;

    [SerializeField] private Inventory inventory;
    [SerializeField] private EquipmentPanel equipmentPanel;
    [SerializeField] private CraftingWindow craftingWindow;
    [SerializeField] private StatsPanel statsPanel;
    [SerializeField] private ItemTooltip itemTooltip;
    [SerializeField] private Image draggableItem;
    [SerializeField] DropItemArea dropItemArea;
    [SerializeField] DropItemDialog dropItemDialog;


    private BaseItemSlot draggedSlot;

    private void OnValidate()
    {
        if(itemTooltip == null)
        {
            itemTooltip = FindObjectOfType<ItemTooltip>();
        }
    }

    private void Awake()
    {
        statsPanel.SetStats(strength, intelligence, charisma, vitality);
        statsPanel.UpdateStatsValue();

        //Setup events
        //Right click
        inventory.OnItemRightClickEvent += EquipFromInventory;
        equipmentPanel.OnItemRightClickEvent += UnequipFromPanel;

        //Pointer enter
        inventory.OnItemPointerEnterEvent += ShowToolTip;
        equipmentPanel.OnItemPointerEnterEvent += ShowToolTip;
        craftingWindow.OnItemPointerEnterEvent += ShowToolTip;

        //Pointer exit
        inventory.OnItemPointerExitEvent += HideToolTip;
        equipmentPanel.OnItemPointerExitEvent += HideToolTip;
        craftingWindow.OnItemPointerExitEvent += HideToolTip;

        //Begin Drag
        inventory.OnItemBeginDragEvent += BeginDrag;
        equipmentPanel.OnItemBeginDragEvent += BeginDrag;

        //End Drag
        inventory.OnItemEndDragEvent += EndDrag;
        equipmentPanel.OnItemEndDragEvent += EndDrag;

        //Drag
        inventory.OnItemDragEvent += Drag;
        equipmentPanel.OnItemDragEvent += Drag;

        //Drop
        inventory.OnItemDropEvent += Drop;
        equipmentPanel.OnItemDropEvent += Drop;
        dropItemArea.OnDropEvent += DropItemFromInventory;
    }

    


    #region For Click and Equip Method
    //private void EquipFromInventory(Item _item)
    //{
    //    Debug.Log("Equip Method called");
    //    if(_item is EquippableItem)
    //    {
    //        EquipItem((EquippableItem)_item);
    //    }
    //}

    //private void UnequipFromPanel(Item _item)
    //{
    //    Debug.Log("Unequip Method called");
    //    if (_item is EquippableItem)
    //    {
    //        Debug.Log("Unequipped");
    //        UnequipItem((EquippableItem)_item);
    //    }
    //} 
    #endregion

    private void EquipFromInventory(BaseItemSlot itemSlot)
    {
        Debug.Log("Equip Method called");
        if (itemSlot.Item is EquippableItem)
		{
			EquipItem((EquippableItem)itemSlot.Item);
		}
		else if (itemSlot.Item is UsableItem)
		{
			UsableItem usableItem = (UsableItem)itemSlot.Item;
			usableItem.UseItem(this);

			if (usableItem.isConsumable)
			{
                Debug.Log("Item Used...");
                inventory.RemoveItem(usableItem);
                usableItem.DestroyItem();
			}
		}
    }

    private void UnequipFromPanel(BaseItemSlot itemSlot)
    {
        Debug.Log("Unequip Method called");
        if (itemSlot.Item is EquippableItem)
        {
            UnequipItem((EquippableItem)itemSlot.Item);
        }
    }
    private void ShowToolTip(BaseItemSlot itemSlot)
    {
        if (itemSlot.Item != null)
        {
            itemTooltip.ShowTooltip(itemSlot.Item);
        }
    }

    private void HideToolTip(BaseItemSlot itemSlot)
    {
        if (itemTooltip.gameObject.activeSelf)
        {
            itemTooltip.HideToolTip();
        }
    }

    private void BeginDrag(BaseItemSlot itemSlot)
    {
        if(itemSlot.Item != null) 
        {
            draggedSlot = itemSlot;
            draggableItem.sprite = itemSlot.Item.itemIcon;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.enabled = true;
        }
    }
    private void EndDrag(BaseItemSlot itemSlot)
    {
        draggedSlot = null;
        draggableItem.enabled = false;
    }

    private void Drag(BaseItemSlot itemSlot)
    {
        if(draggableItem.enabled)
        {
            draggableItem.transform.position = Input.mousePosition;
        }
        
    }
    private void Drop(BaseItemSlot dropItemSlot)
    {
        if (dropItemSlot == null)
            return;

        if(dropItemSlot.CanAddStack(draggedSlot.Item))
        {
            AddItemStacks(dropItemSlot);
        }

        if (dropItemSlot.CanReceiveItem(draggedSlot.Item) && draggedSlot.CanReceiveItem(dropItemSlot.Item))
        {
            SwapItems(dropItemSlot);
        }

    }

    private void SwapItems(BaseItemSlot dropItemSlot)
    {
        EquippableItem dragItem = draggedSlot.Item as EquippableItem;
        EquippableItem dropItem = dropItemSlot.Item as EquippableItem;

        if (draggedSlot is EquipmentSlot)
        {
            if (dragItem != null)
                dragItem.UnequipStat(this);

            if (dropItem != null)
                dropItem.EquipStat(this);
        }

        if (dropItemSlot is EquipmentSlot)
        {
            if (dragItem != null)
                dragItem.EquipStat(this);

            if (dropItem != null)
                dropItem.UnequipStat(this);
        }

        statsPanel.UpdateStatsValue();

        Item draggedItem = draggedSlot.Item;
        int draggedItemAmt = draggedSlot.ItemAmount;

        draggedSlot.Item = dropItemSlot.Item;
        draggedSlot.ItemAmount = dropItemSlot.ItemAmount;

        dropItemSlot.Item = draggedItem;
        dropItemSlot.ItemAmount = draggedItemAmt;
    }

    private void AddItemStacks(BaseItemSlot dropItemSlot)
    {
        int numAddStacks = dropItemSlot.Item.maxStacks - dropItemSlot.ItemAmount;
        int stacksToAdd = Mathf.Min(numAddStacks, draggedSlot.ItemAmount);

        dropItemSlot.ItemAmount -= stacksToAdd;
        draggedSlot.ItemAmount += stacksToAdd;
    }

    public void EquipItem(EquippableItem item)
    {
        if(inventory.RemoveItem(item))
        {
            EquippableItem previousItem;
            if(equipmentPanel.AddEquipment(item,out previousItem))
            {
                if(previousItem!=null)
                {
                    inventory.AddItem(previousItem);
                    previousItem.UnequipStat(this);
                    statsPanel.UpdateStatsValue();
                }
                item.EquipStat(this);
                statsPanel.UpdateStatsValue();
            }

            else
            {
                inventory.AddItem(item);
            }
        }
    }

    public void UnequipItem(EquippableItem item)
    {
        if(!inventory.CanAddItem(item) && equipmentPanel.RemoveEquipment(item))
        {
            item.UnequipStat(this);
            statsPanel.UpdateStatsValue();
            inventory.AddItem(item);
        }
    }

    public void UpdateStatsValue()
    {
        statsPanel.UpdateStatsValue();
    }

    private void DropItemFromInventory()
    {
        if (draggedSlot == null) return;

        dropItemDialog.Show();
        BaseItemSlot slot = draggedSlot;
        dropItemDialog.OnYesEvent += () => DestroyItemInSlot(slot);
    }

    private void DestroyItemInSlot(BaseItemSlot itemSlot)
    {
        // If the item is equipped, unequip first
        if (itemSlot is EquipmentSlot)
        {
            EquippableItem equippableItem = (EquippableItem)itemSlot.Item;
            equippableItem.UnequipStat(this);
            UpdateStatsValue();
        }

        itemSlot.Item.DestroyItem();
        itemSlot.Item = null;
    }

}
