using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private EquipmentPanel equipmentPanel;

    private void Awake()
    {
        inventory.OnItemRightClickedEvent += EquipFromInventory;
        equipmentPanel.OnEquipmentRightClickedEvent += UnequipFromPanel;
    }

    private void EquipFromInventory(Item _item)
    {
        Debug.Log("Equip Method called");
        if(_item is EquippableItem)
        {
            EquipItem((EquippableItem)_item);
        }
    }

    private void UnequipFromPanel(Item _item)
    {
        Debug.Log("Unequip Method called");
        if (_item is EquippableItem)
        {
            Debug.Log("Unequipped");
            UnequipItem((EquippableItem)_item);
        }
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
                }
            }

            else
            {
                inventory.AddItem(item);
            }
        }
    }

    public void UnequipItem(EquippableItem item)
    {
        if(!inventory.IsFull() && equipmentPanel.RemoveEquipment(item))
        {
            inventory.AddItem(item);
        }
    }
}
