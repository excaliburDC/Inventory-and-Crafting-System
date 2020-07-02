using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using InventorySystem.CharacterStats;


public class StatsDisplay : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI statNameText;
    [SerializeField] private TextMeshProUGUI statValue;

    private CharacterStats _charStat;
    public CharacterStats CharStat
    {
        get
        {
            return _charStat;
        }
        set
        {
            _charStat = value;
            UpdateStatsValue();
        }
    }

  

    private string _statName;
    public string StatName 
    {
        get
        {
            return _statName;
        }
        set
        {
            _statName = value;
            statNameText.text = _statName;
        }
    }

    [SerializeField] private StatToolTip statToolTip;

  
    private void OnValidate()
    {
        TextMeshProUGUI[] textM = GetComponentsInChildren<TextMeshProUGUI>();
        statNameText = textM[0];
        statValue = textM[1];

        if(statToolTip==null)
        {
            statToolTip = FindObjectOfType<StatToolTip>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        statToolTip.ShowTooltip(CharStat,StatName);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        statToolTip.HideToolTip();
    }

    public void UpdateStatsValue()
    {
        statValue.text = _charStat.Value.ToString();
    }


}
