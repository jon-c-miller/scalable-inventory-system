using System.Collections.Generic;

public static class InventoryRemove
{
    public static void RemoveItemByType(List<InventoryItem> currentInventory, ItemTypes itemTypeToRemove)
    {
        // Loop backwards to remove the entry farthest from the start (helps support a compact inventory)
        int inventorySize = currentInventory.Count - 1;
        UnityEngine.Debug.LogWarning($"Loop backwards through the entire inventory ({currentInventory.Count} items) to check for item...");
        for (int i = inventorySize; i >= 0; i--)
        {
            // Filter for items of the same type
            UnityEngine.Debug.LogWarning($"Filter for items of the same type...");
            if (currentInventory[i].ItemType == itemTypeToRemove)
            {
                // Handle cases where the item to be removed is stackable
                if (InventoryDatabase.ItemDatabase[itemTypeToRemove].IsStackable)
                {
                    int currentQuantity = currentInventory[i].ItemQuantity;

                    // Remove entry if there is only 1 quantity left
                    if (currentQuantity == 1)
                    {
                        UnityEngine.Debug.LogWarning($"Eliminate the entry due to remaining quantity of 1...");
                        currentInventory[i] = new();
                        break;
                    }

                    // Reduce the current stack and update the entry
                    UnityEngine.Debug.LogWarning($"Reduce the current stack and update the entry...");
                    InventoryItem inventoryUpdate = currentInventory[i].CopyItem(currentQuantity - 1);
                    currentInventory[i] = inventoryUpdate;
                    break;
                }
                else
                {
                    UnityEngine.Debug.LogWarning($"Eliminate the entry...");
                    currentInventory[i] = new();
                    break;
                }
            }
        }
    }
}