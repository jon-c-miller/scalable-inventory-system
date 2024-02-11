using System.Collections.Generic;
using UnityEngine;

/// <summary> Manages an inventory and its view. </summary>
[System.Serializable]
public class InventoryManager
{
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

    public void Initialize()
    {
        // Get the interface component from inventory and item views (allows for any class to act as a view)
        inventoryViewObject.TryGetComponent(out inventoryView);
        itemViewObject.TryGetComponent(out itemView);
        
        inventoryView?.IInitialize();
        itemView?.IInitialize();
    }

    // Database Accessors

    public string GetItemName(ItemIDs id) => InventoryDatabase.GetItemName(id);

    public string GetStatName(ItemStatIDs id) => InventoryDatabase.GetStatName(id);

    public string GetItemDescription(ItemIDs id) => InventoryDatabase.GetItemDescription(id);

    public string GetStatDescription(ItemStatIDs id) => InventoryDatabase.GetStatDescription(id);

    public SInventoryItem GetItemTemplate(ItemIDs id) => InventoryDatabase.GetItemTemplate(id);

    public SInventoryItemStat GetItemStatTemplate(ItemStatIDs id) => InventoryDatabase.GetItemStatTemplate(id);


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
        InventoryAdd.AddItem(currentInventory, itemToAdd, itemStackMax, itemAmountLimit, enableMultipleStacks, enableLogs);
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
        InventoryCompact.CompactItems(currentInventory, out currentInventory);
        inventoryView.ISetCurrentInventory(GetInventory(), true);
    }

    public void NavNext() => inventoryView.ISelectNextEntry(itemView.Interface);

    public void NavPrevious() => inventoryView.ISelectPreviousEntry(itemView.Interface);

    public void UpdateView(InventoryItem itemToView) => itemView.IUpdateEntryBasedOnItem(itemToView);
}