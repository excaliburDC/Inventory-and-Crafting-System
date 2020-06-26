using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private Image itemImage;

    public event Action<Item> OnRightClickEvent;

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

    public void OnPointerClick(PointerEventData eventData)
    {

        if(eventData!=null && eventData.button==PointerEventData.InputButton.Left)
        {
            if(Item!=null && OnRightClickEvent !=null)
            {
                Debug.Log("Clicked");
                OnRightClickEvent(Item);
            }
        }
    }

    protected virtual void OnValidate()
    {
        if (itemImage == null)
            itemImage = GetComponent<Image>();
    }
}
