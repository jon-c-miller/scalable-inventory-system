public interface IItemView
{
    void IUpdateEntryBasedOnItem(InventoryItem item);
    void IInitializeView();
    IItemView Interface { get; }
}