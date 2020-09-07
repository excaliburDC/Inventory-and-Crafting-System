using UnityEngine;
using TMPro;

public class ItemTooltip : MonoBehaviour
{
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text itemTypeText;
    [SerializeField] private TMP_Text itemDescriptionText;


    public void ShowTooltip(Item item)
    {
        itemNameText.text = item.itemName;
        itemTypeText.text = item.GetItemType();
        itemDescriptionText.text = item.GetItemDescription();

        gameObject.SetActive(true);
    }

    public void HideToolTip()
    {
        gameObject.SetActive(false);
    }

    
}
