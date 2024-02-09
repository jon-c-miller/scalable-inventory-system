using System.Collections.Generic;

public static class InventoryAdd
{
    public static void AddItem(List<InventoryItem> currentInventory, InventoryItem itemToAdd, int itemStackMax, int inventoryAmountLimit, bool enableMultipleStacks, bool enableLogs)
    {
        // Loop to update stacking items
        if (itemToAdd.IsStackable)
        {
            if (enableLogs) UnityEngine.Debug.LogWarning($"Loop through the entire inventory to check for a current stack...");

            // Loop through the entire inventory to check for a current stack
            for (int i = 0; i < currentInventory.Count; i++)
            {
                InventoryItem currentItem = currentInventory[i];

                // Filter for items of the same type and quality
                if (currentItem.ItemID == itemToAdd.ItemID && currentItem.ItemQuality == itemToAdd.ItemQuality)
                {
                    int newQuantity = currentItem.ItemQuantity + itemToAdd.ItemQuantity;
                    InventoryItem inventoryAddition;

                    if (enableLogs) UnityEngine.Debug.LogWarning($"Try handle over max stacks...");
                    // Try handle over max stacks
                    if (newQuantity > itemStackMax)
                    {
                        // Try to add a new stack if possible
                        if (enableMultipleStacks)
                        {
                            // Skip entries with a max stack
                            if (currentItem.ItemQuantity == itemStackMax) continue;

                            if (enableLogs) UnityEngine.Debug.LogWarning($"Max the current stack and add a new stack to the first empty spot if available...");
                            // Max the current stack and update the entry
                            InventoryItem inventoryUpdate = currentItem.CopyItem(itemStackMax);
                            currentInventory[i] = inventoryUpdate;

                            // Add a new stack, first attempting to fill the first empty spot
                            int newStackQuantity = newQuantity - itemStackMax;
                            inventoryAddition = currentInventory[i].CopyItem(newStackQuantity);
                            TryAddToFirstEmptySpot(currentInventory, inventoryAddition, inventoryAmountLimit, enableLogs);
                            return;
                        }
                        else
                        {
                            // Only take action if the stack isn't at max yet
                            if (currentItem.ItemQuantity < itemStackMax)
                            {
                                if (enableLogs) UnityEngine.Debug.LogWarning($"Max the stack, and replace the current entry...");
                                inventoryAddition = currentInventory[i].CopyItem(itemStackMax);
                                currentInventory[i] = inventoryAddition;
                            }
                            else if (enableLogs)
                            {
                                UnityEngine.Debug.LogWarning($"Failed to add. Multiple stacks not enabled and current stack is at max.");
                            }
                            return;
                        }
                    }
                    else
                    {
                        // Update the stack count as usual for addition amounts that fall within the stack limit
                        inventoryAddition = currentItem.CopyItem(newQuantity);
                        currentInventory[i] = inventoryAddition;
                        return;
                    }
                }
            }
        }

        // Item is not stackable or no active stack found; add as usual
        TryAddToFirstEmptySpot(currentInventory, itemToAdd, inventoryAmountLimit, enableLogs);
    }

    static void TryAddToFirstEmptySpot(List<InventoryItem> currentInventory, InventoryItem newItem, int inventoryAmountLimit, bool enableLogs)
    {
        // Get an actual item count by skipping empty entries
        int actualItemCount = 0;
        for (int i = 0; i < currentInventory.Count; i++)
        {
            if (currentInventory[i].ItemID == ItemIDs.None) continue;

            actualItemCount++;
        }

        // Don't add an item if inventory is full
        if (actualItemCount >= inventoryAmountLimit)
        {
            if (enableLogs) UnityEngine.Debug.LogWarning($"Inventory is full ({actualItemCount}/{inventoryAmountLimit} items).");
            return;
        }

        if (enableLogs) UnityEngine.Debug.LogWarning($"Try add to the first empty spot if available...");
        // Replace an empty spot with item to be added if possible instead of appending it to the end
        for (int i = 0; i < currentInventory.Count; i++)
        {
            if (currentInventory[i].ItemID == ItemIDs.None)
            {
                currentInventory[i] = newItem;
                return;
            }
        }

        if (enableLogs) UnityEngine.Debug.LogWarning($"Add to the end otherwise...");
        // Add a new stack at the end otherwise
        currentInventory.Add(newItem);
    }
}