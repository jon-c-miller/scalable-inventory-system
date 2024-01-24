using UnityEngine;

/// <summary> Provides a centralized, globally accessible API to handle common system requests. </summary>
public class GameCoordinator : MonoBehaviour
{
    [SerializeField] InventoryManager inventoryManager = new();
    [SerializeField] InventoryReader inventoryReader;
    [SerializeField] ItemReader itemReader;

    // Inventory API
    public void AddItemToInventory(InventoryItem itemToAdd) => inventoryManager.AddItem(itemToAdd);

    public void RemoveItemFromInventory(ItemTypes itemType) => inventoryManager.RemoveItemByType(itemType);

    public void CompactInventory() => inventoryManager.CompactItems();

    public void UpdateItemView(InventoryItem itemToView) => itemReader.UpdateTextBasedOnItem(itemToView);

    public void SetReaderInventory()
    {
        inventoryReader.SetCurrentInventory(inventoryManager.GetInventory());

        // Update the item view based on the inventory view's current selection
        int currentlySelectedIndex = inventoryReader.SelectedInventoryItemIndex;
        UpdateItemView(inventoryManager.GetInventory()[currentlySelectedIndex]);
    }

    public void NavigateInventoryNext()
    {
        inventoryReader.SelectNextEntry();

        int currentlySelectedIndex = inventoryReader.SelectedInventoryItemIndex;
        UpdateItemView(inventoryManager.GetInventory()[currentlySelectedIndex]);
    }

    public void NavigateInventoryPrevious()
    {
        inventoryReader.SelectPreviousEntry();

        int currentlySelectedIndex = inventoryReader.SelectedInventoryItemIndex;
        UpdateItemView(inventoryManager.GetInventory()[currentlySelectedIndex]);
    }





    public static GameCoordinator Instance { get; private set; }

    void Awake()
    {
        // Prevent reassigning an existing instance, as well as destruction on scene loading
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}