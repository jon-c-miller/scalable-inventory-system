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
            
    public void NavigateInventoryNext() => inventoryReader.SelectNextEntry();

    public void NavigateInventoryPrevious() => inventoryReader.SelectPreviousEntry();

    public void SetReaderInventory() => inventoryReader.SetCurrentInventory(inventoryManager.GetInventory());




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