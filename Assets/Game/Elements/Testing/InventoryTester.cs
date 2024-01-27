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
    [SerializeField] KeyCode randomizeInventoryKey = KeyCode.Alpha5;
    [Space]
    [SerializeField] KeyCode selectPreviousEntryKey = KeyCode.W;
    [SerializeField] KeyCode selectNextEntryKey = KeyCode.S;

    [Header("Test Parameters")]
    [SerializeField] ItemTypes desiredItemType;
    [SerializeField] ItemQualityIDs desiredItemQuality;
    [SerializeField, Range(1, 20)] int desiredItemLevel = 1;
    [SerializeField] int desiredRandomizeAmount = 12;
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
        else if (Input.GetKeyDown(randomizeInventoryKey))
        {
            for (int i = 0; i < desiredRandomizeAmount; i++)
            {
                // Get a random item type based on the amount of item types, skipping None
                int itemTypesCount = System.Enum.GetValues(typeof(ItemTypes)).Length;
                ItemTypes randomItemType = (ItemTypes)Random.Range(1, itemTypesCount);

                // Get a random quality type based on the amount of quality types
                int itemQualityTypesCount = System.Enum.GetValues(typeof(ItemQualityIDs)).Length;
                ItemQualityIDs randomQuality = (ItemQualityIDs)Random.Range(1, itemQualityTypesCount);

                // Generate a new item and add it to the inventory
                InventoryItem newItem = InventoryItemGenerator.CreateInventoryItem(randomItemType, randomQuality, Random.Range(1, 100));
                Game.Instance.InventoryAddItem(newItem);
            }
        }
    }
}