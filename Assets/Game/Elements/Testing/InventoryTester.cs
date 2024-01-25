using UnityEngine;

/// <summary> Acts as a proxy to test the project's API at runtime. </summary>
public class InventoryTester : MonoBehaviour
{
    [Header("Keybindings")]
    [SerializeField] KeyCode createDesiredItemKey = KeyCode.Tab;
    [SerializeField] KeyCode updateUIValuesFromItemKey = KeyCode.Space;
    [Space]
    [SerializeField] KeyCode addItemToInventoryKey = KeyCode.Alpha1;
    [SerializeField] KeyCode removeItemFromInventoryKey = KeyCode.Alpha2;
    [SerializeField] KeyCode removeSelectedItemFromInventoryKey = KeyCode.Alpha3;
    [SerializeField] KeyCode compactInventoryKey = KeyCode.Alpha4;
    [Space]
    [SerializeField] KeyCode selectPreviousEntryKey = KeyCode.W;
    [SerializeField] KeyCode selectNextEntryKey = KeyCode.S;

    [Header("Test Parameters")]
    [SerializeField] ItemTypes desiredItemType;
    [SerializeField] ItemQualityIDs desiredItemQuality;
    [SerializeField, Range(1, 20)] int desiredItemLevel = 1;
    [Space]
    [SerializeField] InventoryItem lastGeneratedItem;

    void Update()
    {
        if (Input.GetKeyDown(createDesiredItemKey))
        {
            lastGeneratedItem = InventoryItemGenerator.CreateInventoryItem(desiredItemType, desiredItemQuality, desiredItemLevel);
        }
        else if (Input.GetKeyDown(addItemToInventoryKey))
        {
            GameCoordinator.Instance.AddItemToInventory(lastGeneratedItem);
        }
        else if (Input.GetKeyDown(removeItemFromInventoryKey))
        {
            GameCoordinator.Instance.RemoveItemFromInventory(desiredItemType);
        }
        else if (Input.GetKeyDown(removeSelectedItemFromInventoryKey))
        {
            GameCoordinator.Instance.RemoveSelectedItemFromInventory();
        }
        else if (Input.GetKeyDown(compactInventoryKey))
        {
            GameCoordinator.Instance.CompactInventory();
        }
        else if (Input.GetKeyDown(updateUIValuesFromItemKey))
        {
            GameCoordinator.Instance.UpdateItemView(lastGeneratedItem);
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