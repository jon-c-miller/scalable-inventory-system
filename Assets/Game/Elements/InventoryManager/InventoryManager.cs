using System.Collections.Generic;
using UnityEngine;

/// <summary> Manages an inventory database, current inventory, and a view for displaying both inventory and item details. </summary>
[System.Serializable]
public class InventoryManager
{
    [SerializeField] InventoryDatabase database = new();
    [Space]
    [SerializeField] GameObject inventoryViewObject;
    [SerializeField] GameObject itemViewObject;
    [Space]
    [SerializeField] List<InventoryItem> currentInventory = new() { new InventoryItem() };
    [Space]
    [SerializeField] int itemAmountLimit = 12;
    [SerializeField] int itemStackMax = 5;
    [SerializeField] bool enableMultipleStacks;
    [Space]
    [SerializeField] bool enableLogs;

    IInventoryView inventoryView;
    IItemView itemView;

    InventoryAdd inventoryAdd = new();
    InventoryCompact inventoryCompact = new();


    public void Initialize()
    {
        // Get the interface component from inventory and item views (allows for any class to act as a view)
        inventoryViewObject.TryGetComponent(out inventoryView);
        itemViewObject.TryGetComponent(out itemView);

        database?.Initialize();
        
        inventoryView?.IInitialize();
        itemView?.IInitialize();
    }

    // Database Accessors

    public string GetItemName(ItemIDs id) => database.GetItemName(id);

    public string GetStatName(ItemStatIDs id) => database.GetStatName(id);

    public string GetItemDescription(ItemIDs id) => database.GetItemDescription(id);

    public string GetStatDescription(ItemStatIDs id) => database.GetStatDescription(id);

    public SInventoryItem GetItemTemplate(ItemIDs id) => database.GetItemTemplate(id);

    public SInventoryItemStat GetItemStatTemplate(ItemStatIDs id) => database.GetItemStatTemplate(id);


    // Inventory Accessors

    public InventoryItem[] GetInventory()
    {
        InventoryItem[] fullInventory = new InventoryItem[currentInventory.Count];
        for (int i = 0; i < currentInventory.Count; i++)
        {
            if (currentInventory[i].ItemID == ItemIDs.None)
            {
                fullInventory[i] = new();
            }
            else
            {
                fullInventory[i] = currentInventory[i].CopyItem();
            }
        }

        return fullInventory;
    }

    public InventoryItem GetItemAtIndex(int index) => currentInventory.Count >= index ? currentInventory[index] : new();

    public void AddItem(InventoryItem itemToAdd)
    {
        inventoryAdd.AddItem(currentInventory, itemToAdd, itemStackMax, itemAmountLimit, enableMultipleStacks, enableLogs);
        inventoryView.ISetCurrentInventory(GetInventory(), false);
    }

    public void RemoveItemAtIndex()
    {
        int index = inventoryView.ISelectedInventoryItemIndex;
        InventoryRemove.RemoveAtIndex(currentInventory, index, enableLogs);
        inventoryView.ISetCurrentInventory(GetInventory(), false);
    }

    public void RemoveItemByType(ItemIDs itemTypeToRemove)
    {
        InventoryRemove.RemoveItemByType(currentInventory, itemTypeToRemove, enableLogs);
        inventoryView.ISetCurrentInventory(GetInventory(), false);
    }

    public void CompactItems()
    {
        inventoryCompact.CompactItems(currentInventory, out currentInventory);
        inventoryView.ISetCurrentInventory(GetInventory(), true);
    }

    public void NavNext() => inventoryView.ISelectNextEntry(itemView.Interface);

    public void NavPrevious() => inventoryView.ISelectPreviousEntry(itemView.Interface);

    public void UpdateView(InventoryItem itemToView) => itemView.IUpdateEntryBasedOnItem(itemToView);
}