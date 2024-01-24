using System.Collections.Generic;

public static class InventoryCompact
{
    public static void CompactItems(List<InventoryItem> currentInventory, out List<InventoryItem> updatedInventory)
    {
        List<InventoryItem> newInventory = new();
        for (int i = 0; i < currentInventory.Count; i++)
        {
            // Skip empty entries
            if (currentInventory[i].ItemType == ItemTypes.None) continue;

            // Copy the current entry to the new inventory collection
            InventoryItem itemCopy = currentInventory[i].CopyItem();
            newInventory.Add(itemCopy);
        }

        updatedInventory = newInventory;
    }
}

/*

using System.Collections.Generic;

public static class InventoryCompact
{
    public static void CompactItems(List<InventoryItem> currentInventory)
    {
        int currentInventoryCount = currentInventory.Count;
        int currentIndex = 0;
        int iterationCount = currentInventoryCount * 3;
        bool emptySlotFound = false;
        
        int lastEmptiedIndex = 0;
        int lastFilledIndex = 0;

        while (iterationCount > 0)
        {
            // Limit loop count based on inventory size
            iterationCount--;

            // Loop from start to find index of next empty slot
            for (int i = 0; i < currentInventoryCount; i++)
            {
                // Skip slots previously filled (loop done in same frame, so no incremental updates)
                if (i == lastFilledIndex) continue;

                if (currentInventory[i].ItemType == ItemTypes.None)
                {
                    currentIndex = i;
                    emptySlotFound = true;
                    break;
                }
            }

            // Abort if no empty slot found
            if (!emptySlotFound || currentIndex > currentInventoryCount) break;

            // Loop from end to find next item keeping the inventory expanded and items spread out
            for (int i = currentInventoryCount - 1; i >=0; i--)
            {
                if (currentInventory[i].ItemType != ItemTypes.None)
                {
                    // Skip slots previously emptied (loop done in same frame, so no incremental updates)
                    if (i == lastEmptiedIndex) continue;

                    UnityEngine.Debug.Log($"Copying slot at index {i} to index {currentIndex}...");
                    InventoryItem itemCopy = currentInventory[i].CopyItem();
                    currentInventory[currentIndex] = itemCopy;

                    // Note the index as no longer empty
                    lastFilledIndex = currentIndex;

                    UnityEngine.Debug.Log($"Clearing slot at index {i}...");
                    currentInventory[i] = new();

                    // Note the index as now filled
                    lastEmptiedIndex = i;
                    break;
                }
            }
        }
    }
}

*/