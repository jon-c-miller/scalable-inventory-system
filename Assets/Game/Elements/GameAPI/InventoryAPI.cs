using UnityEngine;

// Provides an API to update a unit's inventory at runtime
public partial class GameAPI
{
    [SerializeField] InventoryManager inventoryManager = new();

    // Database API

    public string InventoryGetItemName(ItemIDs id) => inventoryManager.GetItemName(id);

    public string InventoryGetItemStatName(ItemStatIDs id) => inventoryManager.GetStatName(id);

    public string InventoryGetItemDescription(ItemIDs id) => inventoryManager.GetItemDescription(id);

    public string InventoryGetStatDescription(ItemStatIDs id) => inventoryManager.GetStatDescription(id);

    public SInventoryItem InventoryGetItemTemplate(ItemIDs id) => inventoryManager.GetItemTemplate(id);

    public SInventoryItemStat InventoryGetItemStatTemplate(ItemStatIDs id) => inventoryManager.GetItemStatTemplate(id);


    // Inventory API

    public InventoryItem InventoryGenerateSpecificItem(ItemIDs type, ItemQualityIDs quality, int level) => InventoryItemGenerator.CreateSpecificItem(type, quality, level);

    public InventoryItem InventoryGenerateItemAvailableAtLevel(int level) => InventoryItemGenerator.CreateRandomItemAvailableAtLevel(level);

    public void InventoryAddItem(InventoryItem itemToAdd) => inventoryManager.AddItem(itemToAdd);

    public void InventoryRemoveItemByType(ItemIDs itemType) => inventoryManager.RemoveItemByType(itemType);

    public void InventoryRemoveSelectedItem() => inventoryManager.RemoveItemAtIndex();

    public void InventoryCompact() => inventoryManager.CompactItems();

    public void InventoryUpdateItemView(InventoryItem itemToView) => inventoryManager.UpdateView(itemToView);

    public void InventoryNavigateNext() => inventoryManager.NavNext();

    public void InventoryNavigatePrevious() => inventoryManager.NavPrevious();
}