using UnityEngine;

/// <summary> Provides a centralized, globally accessible API to handle common system requests. </summary>
public class GameCoordinator : MonoBehaviour
{
    [SerializeField] InventoryManager inventoryManager = new();
    [SerializeField] InventoryViewer inventoryViewer = new();
    [SerializeField] ItemViewer itemViewer = new();

    // Inventory API
    public void AddItemToInventory(InventoryItem itemToAdd) => inventoryManager.AddItem(itemToAdd, inventoryViewer.Interface);

    public void RemoveItemFromInventory(ItemTypes itemType) => inventoryManager.RemoveItemByType(itemType, inventoryViewer.Interface);

    public void CompactInventory() => inventoryManager.CompactItems(inventoryViewer.Interface);

    public void UpdateItemView(InventoryItem itemToView) => itemViewer.IUpdateEntryBasedOnItem(itemToView);

    public void NavigateInventoryNext() => inventoryViewer.ISelectNextEntry(itemViewer.Interface);

    public void NavigateInventoryPrevious() => inventoryViewer.ISelectPreviousEntry(itemViewer.Interface);



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

        itemViewer.IInitializeView();
    }
}