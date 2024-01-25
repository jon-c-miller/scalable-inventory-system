public interface IInventoryView
{
    int ISelectedInventoryItemIndex { get; }
    void ISetCurrentInventory(InventoryItem[] inventoryToDisplay);
    void IUpdateEntries();
    void ISelectNextEntry();
    void ISelectPreviousEntry();
    IInventoryView Interface { get; }
}