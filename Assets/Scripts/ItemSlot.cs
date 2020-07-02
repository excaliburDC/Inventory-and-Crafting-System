using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private ItemTooltip itemToolTip;

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

        if (itemToolTip == null)
            itemToolTip = FindObjectOfType<ItemTooltip>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(Item is EquippableItem)
        {
            itemToolTip.ShowTooltip((EquippableItem)Item);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemToolTip.HideToolTip();
    }
}
