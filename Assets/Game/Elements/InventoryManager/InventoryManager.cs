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

    public InventoryItem GetItemAtIndex(int index) => currentInventory.Count >= index ? currentInventory[index] : new();

    public void AddItem(InventoryItem itemToAdd, IInventoryView view)
    {
        InventoryAdd.AddItem(currentInventory, itemToAdd, itemStackMax, itemAmountLimit, enableMultipleStacks);
        view.ISetCurrentInventory(GetInventory(), false);
    }

    public void RemoveItemAtIndex(int index, IInventoryView view)
    {
        InventoryRemove.RemoveAtIndex(currentInventory, index);
        view.ISetCurrentInventory(GetInventory(), false);
    }

    public void RemoveItemByType(ItemTypes itemTypeToRemove, IInventoryView view)
    {
        InventoryRemove.RemoveItemByType(currentInventory, itemTypeToRemove);
        view.ISetCurrentInventory(GetInventory(), false);
    }

    public void CompactItems(IInventoryView view)
    {
        InventoryCompact.CompactItems(currentInventory, out currentInventory);
        view.ISetCurrentInventory(GetInventory(), true);
    }
}