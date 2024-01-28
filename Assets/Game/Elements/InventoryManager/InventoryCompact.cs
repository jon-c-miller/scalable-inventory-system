using System.Collections.Generic;

public static class InventoryCompact
{
    public static void CompactItems(List<InventoryItem> currentInventory, out List<InventoryItem> updatedInventory)
    {
        List<InventoryItem> newInventory = new();
        for (int i = 0; i < currentInventory.Count; i++)
        {
            // Skip empty entries
            if (currentInventory[i].ItemID == ItemIDs.None) continue;

            // Copy the current entry to the new inventory collection
            InventoryItem itemCopy = currentInventory[i].CopyItem();
            newInventory.Add(itemCopy);
        }

        updatedInventory = newInventory;
    }
}