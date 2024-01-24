using UnityEngine;

public class InventoryReader : MonoBehaviour
{
    [SerializeField] ItemEntryDisplay[] entries;
    [SerializeField] int concurrentEntriesToDisplay = 6;
    [Space]
    [SerializeField] Color selectedColor;
    [SerializeField] Color unselectedColor;
    [Header("Debug")]
    [SerializeField] InventoryItem[] inventoryBeingDisplayed;
    [SerializeField] int displayFromInventoryIndex;
    [SerializeField] int selectedEntryIndex;

    public void SetCurrentInventory(InventoryItem[] inventoryToDisplay)
    {
        inventoryBeingDisplayed = inventoryToDisplay;
        displayFromInventoryIndex = 0;
        selectedEntryIndex = 0;

        // Update text selection
        for (int i = 0; i < entries.Length; i++)
        {
            if (entries[i] != null)
            {
                entries[i].ClearEntryText();
                entries[i].UpdateTextColor(unselectedColor);
            }
        }
        entries[0].UpdateTextColor(selectedColor);
        UpdateEntries();
    }

    public void UpdateEntries()
    {
        if (inventoryBeingDisplayed.Length < 1)
        {
            Debug.LogWarning("Inventory is empty. Please set current inventory first.");
            return;
        }

        // Populate canvas entries with current subset of inventory being displayed (based on concurrentEntriesToDisplay)
        int currentEntryIndex = 0;
        for (int i = displayFromInventoryIndex; i < concurrentEntriesToDisplay + displayFromInventoryIndex; i++)
        {
            // Don't try to display an entry outside of the actual inventory size
            if (i >= inventoryBeingDisplayed.Length)
            {
                Debug.LogWarning("Reached end of inventory.");
                return;
            }

            // Gather data for name of entry based on the type of item at this index, as well as quantity
            Debug.LogWarning($"Updating entry at index {currentEntryIndex} based on inventory index {i}...");
            string entryName = InventoryDatabase.ItemDatabase[inventoryBeingDisplayed[i].ItemType].Name;
            
            // Only show quantity if the item is stackable
            string entryQuantity = "";
            if (inventoryBeingDisplayed[i].IsStackable)
            {
                entryQuantity = inventoryBeingDisplayed[i].ItemQuantity.ToString();
            }

            // Set the canvas entry based on starting from index 0
            entries[currentEntryIndex].SetEntryText(entryName, entryQuantity);
            currentEntryIndex++;
        }
    }

    public void SelectNextEntry()
    {
        // Increment the entry selector and update text color if it isn't at the bottom already
        if (selectedEntryIndex < concurrentEntriesToDisplay - 1 && selectedEntryIndex < inventoryBeingDisplayed.Length - 1)
        {
            entries[selectedEntryIndex].UpdateTextColor(unselectedColor);
            selectedEntryIndex++;
            entries[selectedEntryIndex].UpdateTextColor(selectedColor);
            Debug.LogWarning($"Updating entry index to {selectedEntryIndex}...");
        }
        else if (displayFromInventoryIndex < inventoryBeingDisplayed.Length - concurrentEntriesToDisplay)
        {
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
            entries[selectedEntryIndex].UpdateTextColor(selectedColor);
            Debug.LogWarning($"Updating entry index to {selectedEntryIndex}...");
        }
        else if (displayFromInventoryIndex > 0)
        {
            // Otherwise decrease the display from index if still displaying a subset of inventory starting above index 0
            displayFromInventoryIndex--;
            Debug.LogWarning($"Updating entries starting from index {displayFromInventoryIndex}...");
            UpdateEntries();
        }
    }
}