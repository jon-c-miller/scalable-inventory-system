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

        // Populate the entries collection with the current subset of inventory being displayed
        for (int i = displayFromInventoryIndex; i < concurrentEntriesToDisplay; i++)
        {
            // Don't try to add an entry outside of the actual inventory size
            if (i >= inventoryBeingDisplayed.Length)
            {
                Debug.LogWarning("Reached end of inventory.");
                return;
            }

            // Set name of entry based on the type of item at this index
            Debug.LogWarning($"Updating entry at index {i}...");
            string entryName = InventoryDatabase.ItemDatabase[inventoryBeingDisplayed[i].ItemType].Name;

            int entryQuantity = inventoryBeingDisplayed[i].ItemQuantity;
            entries[i].SetEntryText(entryName, entryQuantity);
        }
    }

    public void SelectNextEntry()
    {
        // Increment the entry selector and update text color if it isn't at the bottom already
        if (selectedEntryIndex < concurrentEntriesToDisplay && selectedEntryIndex < inventoryBeingDisplayed.Length - 1)
        {
            entries[selectedEntryIndex].UpdateTextColor(unselectedColor);
            selectedEntryIndex++;
            entries[selectedEntryIndex].UpdateTextColor(selectedColor);
        }

        // Update the content of the entries if necessary
        if (selectedEntryIndex > displayFromInventoryIndex + concurrentEntriesToDisplay)
        {
            displayFromInventoryIndex++;
            UpdateEntries();
        }
    }
}