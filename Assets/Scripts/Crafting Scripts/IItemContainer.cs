
public interface IItemContainer 
{
    int ItemCount(string itemID);
    Item RemoveItem(string itemID);
   // bool ContainsItem(Item item);
    bool RemoveItem(Item item);
    bool AddItem(Item item);
    bool IsFull();

}
