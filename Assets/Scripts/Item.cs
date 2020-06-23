using UnityEngine;

[CreateAssetMenu(menuName ="Inventory/Add Item")]
public class Item : ScriptableObject
{
    public string itemName = "";
    public Sprite itemIcon;
}
