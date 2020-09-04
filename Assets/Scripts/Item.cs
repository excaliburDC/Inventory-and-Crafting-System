using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName ="Inventory/Add Item")]
public class Item : ScriptableObject
{
    [SerializeField] private string id;

    public string ID { get => id; }

    public string itemName = "";

    public Sprite itemIcon;

    [Range(0, 100)]
    public int maxStacks = 1;


    private void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);

        id = AssetDatabase.AssetPathToGUID(path);
    }

    public virtual Item GetItemCopy()
    {
        return this;
    }

    public virtual void DestroyItem()
    {

    }
}
