using UnityEngine;

/// <summary> An arbitrary implementation of an inventory viewer in list form. Designed to test and illustrate use of the API. </summary>
[System.Serializable]
public class InventoryViewer : MonoBehaviour, IInventoryView
{
    [Header("Configuration")]
    [SerializeField] int concurrentEntriesToDisplay = 6;
    [SerializeField] bool skipEmptyEntries;
    [Space]
    [SerializeField] Color selectedColor;
    [SerializeField] Color unselectedColor;
    [Space]
    [SerializeField] RectTransform backgroundPanel;
    [SerializeField] ItemEntryDisplay[] entries;
    [Space]
    [SerializeField] InventoryItem[] inventoryBeingDisplayed;

    int displayFromInventoryIndex;
    int selectedEntryIndex;

    public IInventoryView Interface => this;

    public int ISelectedInventoryItemIndex => selectedEntryIndex + displayFromInventoryIndex;

    public void IInitialize()
    {
        displayFromInventoryIndex = 0;
        selectedEntryIndex = 0;

        // Update text selection
        for (int i = 0; i < entries.Length; i++)
        {
            if (entries[i] != null)
            {
                entries[i].ClearEntryText();
                entries[i].UpdateTextColor(unselectedColor);

                // Enable/disable the entry if it is within the desired amount to display
                entries[i].gameObject.SetActive(i < concurrentEntriesToDisplay);
            }
        }
        // Set background panel size based on active entries
        backgroundPanel.sizeDelta = new Vector2(backgroundPanel.sizeDelta.x, 40 * concurrentEntriesToDisplay);
    }

    public void ISelectNextEntry(IItemView itemView)
    {
        // Navigate to next if not at the bottom already and there are enough item entries to do so
        if (selectedEntryIndex < concurrentEntriesToDisplay - 1 && selectedEntryIndex < inventoryBeingDisplayed.Length - 1)
        {
            Navigate(false);
        }
        else if (displayFromInventoryIndex < inventoryBeingDisplayed.Length - concurrentEntriesToDisplay)
        {
            // Otherwise increase the display from index if the lowest viewable inventory entry is less than inventory count
            displayFromInventoryIndex++;
            IUpdateEntries();
        }
        else return;

        // Check for empty entries and nav again if so, and update the item view as long as the nav command is valid
        if (skipEmptyEntries && inventoryBeingDisplayed[ISelectedInventoryItemIndex].ItemID == ItemIDs.None)
            ISelectNextEntry(itemView);
        itemView.IUpdateEntryBasedOnItem(inventoryBeingDisplayed[ISelectedInventoryItemIndex]);
    }

    public void ISelectPreviousEntry(IItemView itemView)
    {
        if (selectedEntryIndex > 0)
        {
            // Navigate to previous if not at the top already
            Navigate(true);
        }
        else if (displayFromInventoryIndex > 0)
        {
            // Otherwise decrease the display from index if still displaying a subset of inventory starting above index 0
            displayFromInventoryIndex--;
            IUpdateEntries();
        }
        else return;

        if (skipEmptyEntries && inventoryBeingDisplayed[ISelectedInventoryItemIndex].ItemID == ItemIDs.None)
            ISelectPreviousEntry(itemView);
        itemView.IUpdateEntryBasedOnItem(inventoryBeingDisplayed[ISelectedInventoryItemIndex]);
    }

    public void ISetCurrentInventory(InventoryItem[] inventoryToDisplay, bool initializeView)
    {
        inventoryBeingDisplayed = inventoryToDisplay;

        if (initializeView)
        {
            IInitialize();
            entries[0].UpdateTextColor(selectedColor);
        }

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
            if (i >= inventoryBeingDisplayed.Length) return;

            // Handle empty entries
            if (inventoryBeingDisplayed[i].ItemID == ItemIDs.None)
            {
                // Clear canvas for the current entry
                entries[currentEntryIndex].SetEntryText("", "");
                currentEntryIndex++;
                continue;
            }

            // Gather data for name of entry based on the type of item at this index, as well as quantity
            string entryName = Game.Instance.InventoryGetItemName(inventoryBeingDisplayed[i].ItemID);
            
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

        // Automatically highlight the current entry and display the item view
        entries[selectedEntryIndex].UpdateTextColor(selectedColor);
        Game.Instance.InventoryUpdateItemView(inventoryBeingDisplayed[ISelectedInventoryItemIndex]);
    }

    void Navigate(bool isPrevious)
    {
        // Update text color of current index, update index based on moving to next or previous, and update view
        entries[selectedEntryIndex].UpdateTextColor(unselectedColor);
        selectedEntryIndex += isPrevious ? -1 : 1;
        entries[selectedEntryIndex].UpdateTextColor(selectedColor);
    }
}