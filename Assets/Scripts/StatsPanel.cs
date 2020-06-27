using UnityEngine;
using InventorySystem.CharacterStats;

public class StatsPanel : MonoBehaviour
{
    [SerializeField] private StatsDisplay[] statsDisplay;
    [SerializeField] private string[] statsNames;

    private CharacterStats[] characterStats;

    private void OnValidate()
    {
        statsDisplay = GetComponentsInChildren<StatsDisplay>();
        UpdateStatsNames();
    }

    public void SetStats(params CharacterStats[] charStats)
    {
        characterStats = charStats;

        if(characterStats.Length>statsDisplay.Length)
        {
            Debug.LogError("Not Enough Stat Displays");
            return;
        }

        for (int i = 0; i < characterStats.Length; i++)
        {
            statsDisplay[i].gameObject.SetActive(i < statsDisplay.Length);
        }
    }

    public void UpdateStatsValue()
    {
        for (int i = 0; i < characterStats.Length; i++)
        {
            statsDisplay[i].statValue.text = characterStats[i].Value.ToString();
        }
    }

    public void UpdateStatsNames()
    {
        for (int i = 0; i < statsNames.Length; i++)
        {
            statsDisplay[i].statName.text = statsNames[i];
        }
    }
}
