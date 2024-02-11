using System.Collections.Generic;

public static class InventoryRemove
{
    public static void RemoveAtIndex(List<InventoryItem> currentInventory, int index, bool enableLogs)
    {
        if (currentInventory.Count < index) return;

        // Handle cases where the item to be removed is stackable
        if (currentInventory[index].IsStackable)
        {
            int currentQuantity = currentInventory[index].ItemQuantity;

            // Remove entry if there is only 1 quantity left
            if (currentQuantity == 1)
            {
                if (enableLogs) UnityEngine.Debug.LogWarning($"Eliminate the entry due to remaining quantity of 1...");
                currentInventory[index] = new();
                return;
            }

            if (enableLogs) UnityEngine.Debug.LogWarning($"Reduce the current stack and update the entry...");
            // Reduce the current stack and update the entry
            InventoryItem inventoryUpdate = currentInventory[index].CopyItem(currentQuantity - 1);
            currentInventory[index] = inventoryUpdate;
        }
        else
        {
            if (enableLogs) UnityEngine.Debug.LogWarning($"Eliminate the entry...");
            currentInventory[index] = new();
        }
    }

    public static void RemoveItemByType(List<InventoryItem> currentInventory, ItemIDs itemTypeToRemove, bool enableLogs)
    {
        if (enableLogs) UnityEngine.Debug.LogWarning($"Loop backwards through the entire inventory ({currentInventory.Count} items) to check for item...");
        // Loop backwards to remove the entry farthest from the start (helps support a compact inventory)
        int inventorySize = currentInventory.Count - 1;
        for (int i = inventorySize; i >= 0; i--)
        {
            if (enableLogs) UnityEngine.Debug.LogWarning($"Filter for items of the same type...");
            // Filter for items of the same type
            if (currentInventory[i].ItemID == itemTypeToRemove)
            {
                // Handle cases where the item to be removed is stackable
                if (InventoryDatabase.GetItemTemplate(itemTypeToRemove).CheckForDefiningStat(ItemStatIDs.Stackable))
                {
                    int currentQuantity = currentInventory[i].ItemQuantity;

                    // Remove entry if there is only 1 quantity left
                    if (currentQuantity == 1)
                    {
                        if (enableLogs) UnityEngine.Debug.LogWarning($"Eliminate the entry due to remaining quantity of 1...");
                        currentInventory[i] = new();
                        break;
                    }

                    if (enableLogs) UnityEngine.Debug.LogWarning($"Reduce the current stack and update the entry...");
                    // Reduce the current stack and update the entry
                    InventoryItem inventoryUpdate = currentInventory[i].CopyItem(currentQuantity - 1);
                    currentInventory[i] = inventoryUpdate;
                    break;
                }
                else
                {
                    if (enableLogs) UnityEngine.Debug.LogWarning($"Eliminate the entry...");
                    currentInventory[i] = new();
                    break;
                }
            }
        }
    }
}