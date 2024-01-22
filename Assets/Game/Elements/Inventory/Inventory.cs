using System.Collections.Generic;
using UnityEngine;

/// <summary> Provides an API to update a unit's inventory at runtime. </summary>
[System.Serializable]
public class Inventory
{
    [SerializeField] List<InventoryItem> currentInventory = new();
    [Space]
    [SerializeField] int itemAmountLimit = 12;
    [SerializeField] int itemStackMax = 5;
    [SerializeField] bool enableMultipleStacks;

    public InventoryItem[] GetInventory() => currentInventory.ToArray();

    public void AddItem(InventoryItem itemToAdd)
    {
        InventoryAdd.AddItem(currentInventory, itemToAdd, itemStackMax, itemAmountLimit, enableMultipleStacks);
    }

    public void RemoveItemByType(ItemTypes itemToRemove)
    {

    }

    public void RemoveItemByID(int itemID)
    {

    }

    public void CompactItems()
    {

    }
}