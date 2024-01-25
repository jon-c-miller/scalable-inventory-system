using System.Collections.Generic;
using UnityEngine;

/// <summary> Provides an API to update a unit's inventory at runtime. </summary>
[System.Serializable]
public class InventoryManager
{
    [SerializeField] List<InventoryItem> currentInventory = new() { new InventoryItem() };
    [Space]
    [SerializeField] int itemAmountLimit = 12;
    [SerializeField] int itemStackMax = 5;
    [SerializeField] bool enableMultipleStacks;

    public InventoryItem[] GetInventoryCompacted() => currentInventory.ToArray();

    public InventoryItem[] GetInventory()
    {
        InventoryItem[] fullInventory = new InventoryItem[currentInventory.Count];
        for (int i = 0; i < currentInventory.Count; i++)
        {
            if (currentInventory[i].ItemType == ItemTypes.None)
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