using UnityEngine;

// Provides an API to update a unit's inventory at runtime
[System.Serializable] public partial class GameAPI
{
    [SerializeField] InventoryManager inventoryManagerAlt = new();

    // Inventory API
    public void InventoryAddItem(InventoryItem itemToAdd) => inventoryManagerAlt.AddItem(itemToAdd);

    public void InventoryRemoveItemByType(ItemTypes itemType) => inventoryManagerAlt.RemoveItemByType(itemType);

    public void InventoryRemoveSelectedItem() => inventoryManagerAlt.RemoveItemAtIndex();

    public void InventoryCompact() => inventoryManagerAlt.CompactItems();

    public void InventoryUpdateItemView(InventoryItem itemToView) => inventoryManagerAlt.UpdateView(itemToView);

    public void InventoryNavigateNext() => inventoryManagerAlt.NavNext();

    public void InventoryNavigatePrevious() => inventoryManagerAlt.NavPrevious();
}