using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
    [SerializeField] private Transform equipmentParent;
    [SerializeField] private EquipmentSlot[] equipmentsSlot;

    public event Action<Item> OnEquipmentRightClickedEvent;

    private void Awake()
    {
        for (int i = 0; i < equipmentsSlot.Length; i++)
        {
            equipmentsSlot[i].OnRightClickEvent += OnEquipmentRightClickedEvent;
        }
    }

    private void OnValidate()
    {
        equipmentsSlot = equipmentParent.GetComponentsInChildren<EquipmentSlot>();
    }

   

    public bool AddEquipment(EquippableItem currentItem,out EquippableItem previousItem)
    {
        for (int i = 0; i < equipmentsSlot.Length; i++)
        {
            if(equipmentsSlot[i].equipmentType==currentItem.equipmentType)
            {
                previousItem = (EquippableItem)equipmentsSlot[i].Item;
                equipmentsSlot[i].Item = currentItem;
                return true;

            }
        }

        previousItem = null;
        return false;
    }

    public bool RemoveEquipment(EquippableItem item)
    {
        Debug.Log("Remove Equipment Method Called");
        for (int i = 0; i < equipmentsSlot.Length; i++)
        {
            if (equipmentsSlot[i].Item == item)
            {
                equipmentsSlot[i].Item = null;
                return true;

            }
        }

        return false;
    }
}
