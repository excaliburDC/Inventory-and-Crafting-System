using System.Text;
using UnityEngine;
using TMPro;
using InventorySystem.CharacterStats;
using InventorySystem.StatsMod;

public class StatToolTip : MonoBehaviour
{
    [SerializeField] private TMP_Text statNameText;
    [SerializeField] private TMP_Text statModifierLabelText;
    [SerializeField] private TMP_Text statModifiersText;

    private StringBuilder sb = new StringBuilder();



    public void ShowTooltip(CharacterStats stats,string statName)
    {
        statNameText.text = GetStatText(stats, statName);
        statModifiersText.text = GetStatModifiersText(stats);
       
        gameObject.SetActive(true);
    }

    public void HideToolTip()
    {
        gameObject.SetActive(false);
    }

    private string GetStatText(CharacterStats stats,string statName)
    {
        sb.Length = 0;
        sb.Append(statName);
        sb.Append(" ");
        sb.Append(stats.Value);

        if(stats.Value!=stats.BaseValue)
        {
            sb.Append(" (");
            sb.Append(stats.BaseValue);

            if (stats.Value > stats.BaseValue)
                sb.Append("+");

            sb.Append(Mathf.Round(stats.Value - stats.BaseValue));
            sb.Append(")");

        }

        return sb.ToString();
    }

    private string GetStatModifiersText(CharacterStats stats)
    {
        sb.Length = 0;

        foreach (StatsModifier mod in stats.StatModifiers)
        {
            if (sb.Length > 0)
                sb.AppendLine();

            if (mod.Value > 0)
                sb.Append("+");

            if(mod.Type==StatModType.Flat)
            {
                sb.Append(mod.Value);

            }

            else
            {
                sb.Append(mod.Value * 100);
                sb.Append("%");
            }


            Item item = mod.Source as Item;

            if(item!=null)
            {
                sb.Append(" ");
                sb.Append(item.itemName);

            }
            else
            {
                Debug.LogError("Modifier is not an Item");
            }
        }

        return sb.ToString();
    }
    
}
