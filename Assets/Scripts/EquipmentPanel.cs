using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
    [SerializeField] private Transform equipmentParent;
    [SerializeField] private EquipmentSlot[] equipmentsSlot;

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
