using UnityEngine;

// Provides an API to update a unit's inventory at runtime
public partial class GameAPI
{
    [SerializeField] InventoryManager inventoryManager = new();

    // Inventory API
    public void InventoryAddItem(InventoryItem itemToAdd) => inventoryManager.AddItem(itemToAdd);

    public void InventoryRemoveItemByType(ItemIDs itemType) => inventoryManager.RemoveItemByType(itemType);

    public void InventoryRemoveSelectedItem() => inventoryManager.RemoveItemAtIndex();

    public void InventoryCompact() => inventoryManager.CompactItems();

    public void InventoryUpdateItemView(InventoryItem itemToView) => inventoryManager.UpdateView(itemToView);

    public void InventoryNavigateNext() => inventoryManager.NavNext();

    public void InventoryNavigatePrevious() => inventoryManager.NavPrevious();
}