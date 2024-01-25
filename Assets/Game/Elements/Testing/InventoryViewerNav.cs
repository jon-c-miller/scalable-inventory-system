using UnityEngine;

/// <summary> Holds navigation logic for the InventoryViewer. </summary>
public partial class InventoryViewer
{
    public void SelectNextEntry()
    {
        // Increment the entry selector and update text color if it isn't at the bottom already
        if (selectedEntryIndex < concurrentEntriesToDisplay - 1 && selectedEntryIndex < inventoryBeingDisplayed.Length - 1)
        {
            entries[selectedEntryIndex].UpdateTextColor(unselectedColor);
            selectedEntryIndex++;

            // Skip entries that are empty
            if (inventoryBeingDisplayed[SelectedInventoryItemIndex].ItemType == ItemTypes.None)
            {
                if (skipEmptyEntries)
                    SelectNextEntry();

                return;
            }

            entries[selectedEntryIndex].UpdateTextColor(selectedColor);
            Debug.LogWarning($"Updating entry index to {selectedEntryIndex}...");
        }
        else if (displayFromInventoryIndex < inventoryBeingDisplayed.Length - concurrentEntriesToDisplay)
        {
            // Skip entries that are empty
            if (inventoryBeingDisplayed[SelectedInventoryItemIndex].ItemType == ItemTypes.None)
            {
                if (skipEmptyEntries)
                    SelectNextEntry();

                return;
            }

            // Otherwise increase the display from index if the lowest viewable inventory entry is less than inventory count
            displayFromInventoryIndex++;
            Debug.LogWarning($"Updating entries starting from index {displayFromInventoryIndex}...");
            UpdateEntries();
        }
    }

    public void SelectPreviousEntry()
    {
        // Decrement the entry selector and update text color if it isn't at the top already
        if (selectedEntryIndex > 0)
        {
            entries[selectedEntryIndex].UpdateTextColor(unselectedColor);
            selectedEntryIndex--;

            // Skip entries that are empty
            if (inventoryBeingDisplayed[SelectedInventoryItemIndex].ItemType == ItemTypes.None)
            {
                if (skipEmptyEntries)
                    SelectPreviousEntry();

                return;
            }

            entries[selectedEntryIndex].UpdateTextColor(selectedColor);
            Debug.LogWarning($"Updating entry index to {selectedEntryIndex}...");
        }
        else if (displayFromInventoryIndex > 0)
        {
            // Skip entries that are empty
            if (inventoryBeingDisplayed[SelectedInventoryItemIndex].ItemType == ItemTypes.None)
            {
                if (skipEmptyEntries)
                    SelectPreviousEntry();

                return;
            }

            // Otherwise decrease the display from index if still displaying a subset of inventory starting above index 0
            displayFromInventoryIndex--;
            Debug.LogWarning($"Updating entries starting from index {displayFromInventoryIndex}...");
            UpdateEntries();
        }
    }
}