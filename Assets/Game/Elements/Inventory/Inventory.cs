using System.Collections.Generic;
using UnityEngine;

/// <summary> Provides an API to update a unit's inventory at runtime. </summary>
public class Inventory : MonoBehaviour
{
    [SerializeField] int itemAmountLimit = 12;
    [SerializeField] int itemStackMax = 5;
    [SerializeField] bool enableMultipleStacks;
    
    List<InventoryItem> currentInventory = new();

    InventoryItem[] GetInventory() => currentInventory.ToArray();

    public void AddItem(InventoryItem itemToAdd)
    {

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