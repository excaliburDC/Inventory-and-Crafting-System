using UnityEngine;
using InventorySystem.CharacterStats;

public class Character : MonoBehaviour
{
    public CharacterStats strength;
    public CharacterStats intelligence;
    public CharacterStats charisma;
    public CharacterStats vitality;

    [SerializeField] private Inventory inventory;
    [SerializeField] private EquipmentPanel equipmentPanel;
    [SerializeField] private StatsPanel statsPanel;

    private void Awake()
    {
        statsPanel.SetStats(strength, intelligence, charisma, vitality);
        statsPanel.UpdateStatsValue();

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
        if(!inventory.IsFull() && equipmentPanel.RemoveEquipment(item))
        {
            item.UnequipStat(this);
            statsPanel.UpdateStatsValue();
            inventory.AddItem(item);
        }
    }
}
