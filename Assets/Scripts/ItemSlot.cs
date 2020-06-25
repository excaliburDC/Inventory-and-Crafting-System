using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image itemImage;

    private Item _item;
    public Item Item
    {
        get
        {
            return _item;
        }

        set
        {
            _item = value;

            if(_item==null)
            {
                itemImage.enabled = false;

            }
            else
            {
                itemImage.sprite = _item.itemIcon;
                itemImage.enabled = true;
            }
        }
    }



    protected virtual void OnValidate()
    {
        if (itemImage == null)
            itemImage = GetComponent<Image>();
    }
}
