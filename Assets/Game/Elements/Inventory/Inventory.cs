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
        // Loop to update stacking items
        if (itemToAdd.IsStackable)
        {
            // Loop through the entire inventory to check for a current stack
            Debug.LogWarning($"Loop through the entire inventory to check for a current stack...");
            for (int i = 0; i < currentInventory.Count; i++)
            {
                // Filter for items of the same type
                Debug.LogWarning($"Filter for items of the same type...");
                if (currentInventory[i].ItemType == itemToAdd.ItemType)
                {
                    int newQuantity = currentInventory[i].ItemQuantity + itemToAdd.ItemQuantity;
                    InventoryItem inventoryAddition;

                    // Try handle over max stacks
                    Debug.LogWarning($"Try handle over max stacks...");
                    if (newQuantity > itemStackMax)
                    {
                        // Try to add a new stack if possible
                        if (enableMultipleStacks)
                        {
                            // Skip entries with a max stack
                            if (currentInventory[i].ItemQuantity == itemStackMax) continue;

                            // Max the current stack
                            Debug.LogWarning($"Max the current stack and add a new stack to the first empty spot if available...");
                            InventoryItem inventoryUpdate = currentInventory[i].CopyItem(itemStackMax);
                            currentInventory[i] = inventoryUpdate;

                            // Add a new stack, first attempting to fill the first empty spot
                            int newStackQuantity = newQuantity - itemStackMax;
                            inventoryAddition = currentInventory[i].CopyItem(newStackQuantity);
                            TryAddToFirstEmptySpot(inventoryAddition);
                            return;
                        }
                        else
                        {
                            // Only take action if the stack isn't at max yet
                            if (currentInventory[i].ItemQuantity < itemStackMax)
                            {
                                Debug.LogWarning($"Max the stack, and replace the current entry...");
                                inventoryAddition = currentInventory[i].CopyItem(itemStackMax);
                                currentInventory[i] = inventoryAddition;
                            }
                            else
                            {
                                Debug.LogWarning($"Failed to add. Multiple stacks not enabled and current stack is at max.");
                            }
                            return;
                        }
                    }
                    else
                    {
                        // Update the stack count as usual for addition amounts that fall within the stack limit
                        Debug.LogWarning($"Updating quantity to {newQuantity} ({currentInventory[i].ItemQuantity} + {itemToAdd.ItemQuantity})...");
                        inventoryAddition = currentInventory[i].CopyItem(newQuantity);
                        currentInventory[i] = inventoryAddition;
                        return;
                    }
                }
            }
        }

        // Item is not stackable or no active stack found; add as usual
        TryAddToFirstEmptySpot(itemToAdd);
    }

    void TryAddToFirstEmptySpot(InventoryItem newItem)
    {
        // Get an actual item count by skipping empty entries
        Debug.LogWarning($"Get an actual item count by skipping empty entries.");
        int actualItemCount = 0;
        for (int i = 0; i < currentInventory.Count; i++)
        {
            if (currentInventory[i].ItemType == ItemTypes.None) continue;

            actualItemCount++;
        }

        // Don't add an item if inventory is full
        if (actualItemCount >= itemAmountLimit)
        {
            Debug.LogWarning($"Inventory is full ({actualItemCount}/{itemAmountLimit} items).");
            return;
        }

        Debug.LogWarning($"Try add to the first empty spot if available...");
        for (int i = 0; i < currentInventory.Count; i++)
        {
            if (currentInventory[i].ItemType == ItemTypes.None)
            {
                currentInventory[i] = newItem;
                return;
            }
        }

        // Add a new stack at the end otherwise
        Debug.LogWarning($"Add to the end otherwise...");
        currentInventory.Add(newItem);
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