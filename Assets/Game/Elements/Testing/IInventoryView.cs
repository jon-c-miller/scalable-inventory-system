public interface IInventoryView
{
    int ISelectedInventoryItemIndex { get; }
    void ISetCurrentInventory(InventoryItem[] inventoryToDisplay);
    void IUpdateEntries();
    void ISelectNextEntry(IItemView itemView);
    void ISelectPreviousEntry(IItemView itemView);
    IInventoryView Interface { get; }
}