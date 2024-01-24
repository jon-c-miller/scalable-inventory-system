using System.Collections.Generic;
using UnityEngine;

/// <summary> Provides an API to update a unit's inventory at runtime. </summary>
[System.Serializable]
public class Inventory
{
    [SerializeField] List<InventoryItem> currentInventory = new() { new InventoryItem() };
    [Space]
    [SerializeField] int itemAmountLimit = 12;
    [SerializeField] int itemStackMax = 5;
    [SerializeField] bool enableMultipleStacks;

    public InventoryItem[] GetInventory() => currentInventory.ToArray();

    public void AddItem(InventoryItem itemToAdd)
    {
        InventoryAdd.AddItem(currentInventory, itemToAdd, itemStackMax, itemAmountLimit, enableMultipleStacks);
    }

    public void RemoveItemByType(ItemTypes itemTypeToRemove)
    {
        InventoryRemove.RemoveItemByType(currentInventory, itemTypeToRemove);
    }

    public void CompactItems()
    {
        InventoryCompact.CompactItems(currentInventory, out currentInventory);
    }
}