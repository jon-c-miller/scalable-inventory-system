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

    [Header("Test Parameters")]
    [SerializeField] ItemTypes desiredItemType;
    [SerializeField] ItemQualityIDs desiredItemQuality;
    [SerializeField, Range(1, 20)] int desiredItemLevel;
    [Space]
    [SerializeField] InventoryItem lastInstantiatedItem;
    [SerializeField] int itemIDToRemove;

    void Update()
    {
        if (Input.GetKeyDown(updateUIValuesFromItemKey))
        {
            InventoryItem itemBasedOnDesiredType = InventoryItemGenerator.CreateInventoryItem(desiredItemType, desiredItemQuality, desiredItemLevel);
            reader.UpdateTextBasedOnItem(itemBasedOnDesiredType);
        }
    }
}