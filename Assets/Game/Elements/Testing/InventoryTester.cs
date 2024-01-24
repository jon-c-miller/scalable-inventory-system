using UnityEngine;

/// <summary> Acts as a proxy to modify a unit's inventory at runtime. </summary>
public class InventoryTester : MonoBehaviour
{
    [Header("Keybindings")]
    [SerializeField] KeyCode createDesiredItemKey = KeyCode.Tab;
    [SerializeField] KeyCode updateUIValuesFromItemKey = KeyCode.Space;
    [Space]
    [SerializeField] KeyCode addItemToInventoryKey = KeyCode.Alpha1;
    [SerializeField] KeyCode removeItemFromInventoryKey = KeyCode.Alpha2;
    [SerializeField] KeyCode compactInventoryKey = KeyCode.Alpha3;
    [Space]
    [SerializeField] KeyCode setInventoryKey = KeyCode.Alpha4;
    [SerializeField] KeyCode selectPreviousEntryKey = KeyCode.W;
    [SerializeField] KeyCode selectNextEntryKey = KeyCode.S;

    [Header("Test Parameters")]
    [SerializeField] ItemTypes desiredItemType;
    [SerializeField] ItemQualityIDs desiredItemQuality;
    [SerializeField, Range(1, 20)] int desiredItemLevel = 1;
    [Space]
    [SerializeField] InventoryItem lastInstantiatedItem;

    void Update()
    {
        if (Input.GetKeyDown(createDesiredItemKey))
        {
            lastInstantiatedItem = InventoryItemGenerator.CreateInventoryItem(desiredItemType, desiredItemQuality, desiredItemLevel);
        }
        else if (Input.GetKeyDown(addItemToInventoryKey))
        {
            GameCoordinator.Instance.AddItemToInventory(lastInstantiatedItem);
        }
        else if (Input.GetKeyDown(removeItemFromInventoryKey))
        {
            GameCoordinator.Instance.RemoveItemFromInventory(lastInstantiatedItem.ItemType);
        }
        else if (Input.GetKeyDown(compactInventoryKey))
        {
            GameCoordinator.Instance.CompactInventory();
        }
        else if (Input.GetKeyDown(updateUIValuesFromItemKey))
        {
            GameCoordinator.Instance.UpdateItemView(lastInstantiatedItem);
        }
        else if (Input.GetKeyDown(setInventoryKey))
        {
            GameCoordinator.Instance.SetReaderInventory();
        }
        else if (Input.GetKeyDown(selectPreviousEntryKey))
        {
            GameCoordinator.Instance.NavigateInventoryPrevious();
        }
        else if (Input.GetKeyDown(selectNextEntryKey))
        {
            GameCoordinator.Instance.NavigateInventoryNext();
        }
    }
}