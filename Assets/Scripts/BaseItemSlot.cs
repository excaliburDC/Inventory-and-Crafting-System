using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class BaseItemSlot : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] protected Image itemImage;
    [SerializeField] protected TMP_Text amountText;
    

    public event Action<BaseItemSlot> OnPointerEnterEvent;
    public event Action<BaseItemSlot> OnPointerExitEvent;
    public event Action<BaseItemSlot> OnRightClickEvent;
   

    protected Color normalColor = Color.white;
    protected Color disabledColor = new Color(1, 1, 1, 0);

    protected Item _item;
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
        return false;
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

    
}
