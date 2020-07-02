using System.Text;
using UnityEngine;
using TMPro;

public class ItemTooltip : MonoBehaviour
{
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text itemSlotText;
    [SerializeField] private TMP_Text itemStatsText;

    private StringBuilder sb = new StringBuilder();



    public void ShowTooltip(EquippableItem item)
    {
        itemNameText.text = item.itemName;
        itemSlotText.text = item.equipmentType.ToString();

        sb.Length = 0;

        //add stats for items
        AddStats(item.strengthBonus, "Strength");
        AddStats(item.intelligenceBonus, "Intelligence");
        AddStats(item.charismaBonus, "Charisma");
        AddStats(item.vitalityBonus, "Vitality");

        //add bonus stats for item if it has any
        AddStats(item.strengthPercentBonus, "Strength",true);
        AddStats(item.intelligencePercentBonus, "Intelligence",true);
        AddStats(item.charismaPercentBonus, "Charisma",true);
        AddStats(item.vitalityPercentBonus, "Vitality",true);

        itemStatsText.text = sb.ToString();

        gameObject.SetActive(true);
    }

    public void HideToolTip()
    {
        gameObject.SetActive(false);
    }

    private void AddStats(float value,string statName,bool isPercent = false)
    {
        if (value != 0) 
        {
            if (sb.Length > 0)
                sb.AppendLine();

            if (value > 0)
                sb.Append("+");

            if(isPercent)
            {
                sb.Append(value * 100);
                sb.Append("% ");
            }

            else
            {
                sb.Append(value);
                sb.Append(" ");
            }

           
            sb.Append(statName);
        }
  
    }
}
