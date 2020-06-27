using UnityEngine;
using TMPro;


public class StatsDisplay : MonoBehaviour
{
    public TextMeshProUGUI statName;
    public TextMeshProUGUI statValue;

    private void OnValidate()
    {
        TextMeshProUGUI[] textM = GetComponentsInChildren<TextMeshProUGUI>();
        statName = textM[0];
        statValue = textM[1];
    }

}
