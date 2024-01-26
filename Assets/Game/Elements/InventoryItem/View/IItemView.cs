public interface IItemView
{
    void IUpdateEntryBasedOnItem(InventoryItem item);
    void IInitialize();
    IItemView Interface { get; }
}