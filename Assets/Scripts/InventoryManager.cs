using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private EquipmentPanel equipmentPanel;

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
