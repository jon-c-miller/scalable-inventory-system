using UnityEngine;

/// <summary> Provides a centralized, globally accessible API to handle common system requests. </summary>
public class GameCoordinator : MonoBehaviour
{
    [SerializeField] Inventory inventory = new();
    [SerializeField] InventoryReader inventoryReader;
    [SerializeField] ItemReader itemReader;

    // Inventory API
    public void AddItemToInventory(InventoryItem itemToAdd) => inventory.AddItem(itemToAdd);

    public void RemoveItemFromInventory(ItemTypes itemType) => inventory.RemoveItemByType(itemType);

    public void CompactInventory() => inventory.CompactItems();

    public void UpdateItemView(InventoryItem itemToView) => itemReader.UpdateTextBasedOnItem(itemToView);
            
    public void NavigateInventoryNext() => inventoryReader.SelectNextEntry();

    public void NavigateInventoryPrevious() => inventoryReader.SelectPreviousEntry();

    public void SetReaderInventory() => inventoryReader.SetCurrentInventory(inventory.GetInventory());




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