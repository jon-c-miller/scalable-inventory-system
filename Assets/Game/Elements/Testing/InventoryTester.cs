using UnityEngine;

/// <summary> Acts as a proxy to test the project's API at runtime. </summary>
public class InventoryTesterAlternate : MonoBehaviour
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
            Game.Instance.InventoryAddItem(lastGeneratedItem);
        }
        else if (Input.GetKeyDown(removeItemFromInventoryKey))
        {
            Game.Instance.InventoryRemoveItemByType(desiredItemType);
        }
        else if (Input.GetKeyDown(removeSelectedItemFromInventoryKey))
        {
            Game.Instance.InventoryRemoveSelectedItem();
        }
        else if (Input.GetKeyDown(compactInventoryKey))
        {
            Game.Instance.InventoryCompact();
        }
        else if (Input.GetKeyDown(updateUIValuesFromItemKey))
        {
            Game.Instance.InventoryUpdateItemView(lastGeneratedItem);
        }
        else if (Input.GetKeyDown(selectPreviousEntryKey))
        {
            Game.Instance.InventoryNavigatePrevious();
        }
        else if (Input.GetKeyDown(selectNextEntryKey))
        {
            Game.Instance.InventoryNavigateNext();
        }
    }
}