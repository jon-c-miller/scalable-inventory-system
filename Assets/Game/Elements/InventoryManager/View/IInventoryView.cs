public interface IInventoryView
{
    int ISelectedInventoryItemIndex { get; }
    void ISetCurrentInventory(InventoryItem[] inventoryToDisplay, bool resetView);
    void IUpdateEntries();
    void ISelectNextEntry(IItemView itemView);
    void ISelectPreviousEntry(IItemView itemView);
    void IInitialize();
    IInventoryView Interface { get; }
}