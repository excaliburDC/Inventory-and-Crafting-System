using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName ="Inventory/Add Item")]
public class Item : ScriptableObject
{
    [SerializeField] private string id;

    public string ID { get => id; }

    public string itemName = "";
    public Sprite itemIcon;

    private void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);

        id = AssetDatabase.AssetPathToGUID(path);
    }
}
