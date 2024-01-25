using UnityEngine;

[System.Serializable]
public partial class InventoryViewer : IInventoryView
{
    [SerializeField] ItemEntryDisplay[] entries;
    [SerializeField] int concurrentEntriesToDisplay = 6;
    [SerializeField] bool skipEmptyEntries;
    [Space]
    [SerializeField] Color selectedColor;
    [SerializeField] Color unselectedColor;
    [Header("Debug")]
    [SerializeField] InventoryItem[] inventoryBeingDisplayed;
    [SerializeField] int displayFromInventoryIndex;
    [SerializeField] int selectedEntryIndex;

    public IInventoryView Interface => this;

    public int ISelectedInventoryItemIndex => selectedEntryIndex + displayFromInventoryIndex;

    public void ISetCurrentInventory(InventoryItem[] inventoryToDisplay)
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
        IUpdateEntries();
    }

    public void IUpdateEntries()
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

            // Handle empty entries
            if (inventoryBeingDisplayed[i].ItemType == ItemTypes.None)
            {
                // Clear canvas for the current entry
                entries[currentEntryIndex].SetEntryText("", "");
                currentEntryIndex++;
                continue;
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
}