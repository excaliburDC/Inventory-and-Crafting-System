using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemSlot : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler,IDragHandler,IBeginDragHandler,IEndDragHandler,IDropHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text amountText;
    
    private Vector2 originalPos;

    public event Action<ItemSlot> OnPointerEnterEvent;
    public event Action<ItemSlot> OnPointerExitEvent;
    public event Action<ItemSlot> OnRightClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;

    private Color normalColor = Color.white;
    private Color disabledColor = new Color(1, 1, 1, 0);

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
                itemImage.color=disabledColor;

            }
            else
            {
                itemImage.sprite = _item.itemIcon;
                itemImage.color = normalColor;
            }
        }
    }

    private int _itemAmount;

    public int ItemAmount
    {
        get
        {
            return _itemAmount;
        }

        set
        {
            _itemAmount = value;

            if (_itemAmount < 0)
                _itemAmount = 0;

            if (_itemAmount == 0)
                Item = null;

            if(amountText!=null)
            {
                amountText.enabled = _item != null  && _itemAmount > 1;

                if (amountText.enabled)
                {
                    amountText.text = _itemAmount.ToString();
                }
            }
            
        }
    }


    protected virtual void OnValidate()
    {
        if (itemImage == null)
            itemImage = GetComponent<Image>();

        if (amountText == null)
            amountText = GetComponentInChildren<TMP_Text>();

    }

    public virtual bool CanAddStack(Item item, int amount = 1)
    {
        return Item != null && Item.ID == item.ID && ItemAmount + amount <= item.maxStacks;
    }

    public virtual bool CanReceiveItem(Item item)
    {
        return true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if(eventData!=null && eventData.button==PointerEventData.InputButton.Right)
        {
            if(OnRightClickEvent !=null)
            {
                Debug.Log("Clicked");
                OnRightClickEvent(this);
            }
        }
    }

   

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (OnPointerEnterEvent != null)
            OnPointerEnterEvent(this);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (OnPointerExitEvent != null)
            OnPointerExitEvent(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragEvent != null)
            OnDragEvent(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (OnBeginDragEvent != null)
            OnBeginDragEvent(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (OnEndDragEvent != null)
            OnEndDragEvent(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (OnDropEvent != null)
            OnDropEvent(this);
    }
}
