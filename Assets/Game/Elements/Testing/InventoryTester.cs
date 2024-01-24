using UnityEngine;

/// <summary> Acts as a proxy to modify a unit's inventory at runtime. </summary>
public class InventoryTester : MonoBehaviour
{
    [SerializeField] CanvasItemReader reader;
    [Space]
    [SerializeField] Inventory inventory = new();

    [Header("Keybindings")]
    [SerializeField] KeyCode createDesiredItemKey = KeyCode.Tab;
    [SerializeField] KeyCode updateUIValuesFromItemKey = KeyCode.Space;
    [Space]
    [SerializeField] KeyCode addItemToInventoryKey = KeyCode.Alpha1;
    [SerializeField] KeyCode removeItemFromInventoryKey = KeyCode.Alpha2;
    [SerializeField] KeyCode removeUniqueItemFromInventoryKey = KeyCode.Alpha3;
    [SerializeField] KeyCode compactInventoryKey = KeyCode.Alpha4;

    [Header("Test Parameters")]
    [SerializeField] ItemTypes desiredItemType;
    [SerializeField] ItemQualityIDs desiredItemQuality;
    [SerializeField, Range(1, 20)] int desiredItemLevel = 1;
    [Space]
    [SerializeField] InventoryItem lastInstantiatedItem;
    [SerializeField] int itemIDToRemove;

    void Update()
    {
        if (Input.GetKeyDown(createDesiredItemKey))
        {
            lastInstantiatedItem = InventoryItemGenerator.CreateInventoryItem(desiredItemType, desiredItemQuality, desiredItemLevel);
        }
        else if (Input.GetKeyDown(addItemToInventoryKey))
        {
            inventory.AddItem(lastInstantiatedItem);
        }
        else if (Input.GetKeyDown(removeItemFromInventoryKey))
        {
            inventory.RemoveItemByType(lastInstantiatedItem.ItemType);
        }
        else if (Input.GetKeyDown(removeUniqueItemFromInventoryKey))
        {
            inventory.RemoveItemByID(lastInstantiatedItem.ItemID);
        }
        else if (Input.GetKeyDown(compactInventoryKey))
        {
            inventory.CompactItems();
        }
        else if (Input.GetKeyDown(updateUIValuesFromItemKey))
        {
            reader.UpdateTextBasedOnItem(lastInstantiatedItem);
        }
    }
}