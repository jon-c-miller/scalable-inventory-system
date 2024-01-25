using UnityEngine;

/// <summary> Holds navigation logic for the InventoryViewer. </summary>
public partial class InventoryViewer
{
    public void ISelectNextEntry()
    {
        // Increment the entry selector and update text color if it isn't at the bottom already
        if (selectedEntryIndex < concurrentEntriesToDisplay - 1 && selectedEntryIndex < inventoryBeingDisplayed.Length - 1)
        {
            entries[selectedEntryIndex].UpdateTextColor(unselectedColor);
            selectedEntryIndex++;

            // Skip entries that are empty
            if (inventoryBeingDisplayed[ISelectedInventoryItemIndex].ItemType == ItemTypes.None)
            {
                if (skipEmptyEntries)
                    ISelectNextEntry();

                return;
            }

            entries[selectedEntryIndex].UpdateTextColor(selectedColor);
            Debug.LogWarning($"Updating entry index to {selectedEntryIndex}...");
        }
        else if (displayFromInventoryIndex < inventoryBeingDisplayed.Length - concurrentEntriesToDisplay)
        {
            // Skip entries that are empty
            if (inventoryBeingDisplayed[ISelectedInventoryItemIndex].ItemType == ItemTypes.None)
            {
                if (skipEmptyEntries)
                    ISelectNextEntry();

                return;
            }

            // Otherwise increase the display from index if the lowest viewable inventory entry is less than inventory count
            displayFromInventoryIndex++;
            Debug.LogWarning($"Updating entries starting from index {displayFromInventoryIndex}...");
            IUpdateEntries();
        }
    }

    public void ISelectPreviousEntry()
    {
        // Decrement the entry selector and update text color if it isn't at the top already
        if (selectedEntryIndex > 0)
        {
            entries[selectedEntryIndex].UpdateTextColor(unselectedColor);
            selectedEntryIndex--;

            // Skip entries that are empty
            if (inventoryBeingDisplayed[ISelectedInventoryItemIndex].ItemType == ItemTypes.None)
            {
                if (skipEmptyEntries)
                    ISelectPreviousEntry();

                return;
            }

            entries[selectedEntryIndex].UpdateTextColor(selectedColor);
            Debug.LogWarning($"Updating entry index to {selectedEntryIndex}...");
        }
        else if (displayFromInventoryIndex > 0)
        {
            // Skip entries that are empty
            if (inventoryBeingDisplayed[ISelectedInventoryItemIndex].ItemType == ItemTypes.None)
            {
                if (skipEmptyEntries)
                    ISelectPreviousEntry();

                return;
            }

            // Otherwise decrease the display from index if still displaying a subset of inventory starting above index 0
            displayFromInventoryIndex--;
            Debug.LogWarning($"Updating entries starting from index {displayFromInventoryIndex}...");
            IUpdateEntries();
        }
    }
}