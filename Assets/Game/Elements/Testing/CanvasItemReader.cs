using UnityEngine;

public class CanvasItemReader : MonoBehaviour
{
    [SerializeField] KeyCode updateUIValuesFromItem = KeyCode.Space;
    [Space]
    [SerializeField] InventoryItem instantiatedItem;

    [Header("Generation Test Parameters")]
    [SerializeField] ItemTypes desiredItemType;
    [SerializeField] ItemQualityIDs desiredItemQuality;
    [SerializeField, Range(1, 20)] int desiredItemLevel;
    
    [Header("UI Fields")]
    [SerializeField] UnityEngine.UI.Text nameText;
    [SerializeField] UnityEngine.UI.Text descriptionText;
    [SerializeField] UnityEngine.UI.Text quantityText;
    [SerializeField] UnityEngine.UI.Text[] propertyTexts;

    public void UpdateTextBasedOnItem(InventoryItem newItem)
    {
        instantiatedItem = newItem;

        // Use the item's type to retrieve name and description from the item database
        nameText.text = InventoryItemDatabase.ItemDatabase[newItem.ItemType].Name;
        descriptionText.text = InventoryItemDatabase.ItemDatabase[newItem.ItemType].Description;

        for (int i = 0; i < instantiatedItem.ItemStats.Length; i++)
        {
            // Handle the quantity stat differently from other stats
            if (instantiatedItem.ItemStats[i].Type == ItemStatTypes.Quantity)
            {
                quantityText.text = instantiatedItem.ItemStats[i].Value.ToString();
            }
            else
            {
                propertyTexts[i].text = FormatBasedOnStatType(instantiatedItem.ItemStats[i]);
            }
        }
    }

    string FormatBasedOnStatType(InventoryItemStat stat)
    {
        return "";
    }

    void Awake()
    {
        // Initialize text for properties
        for (int i = 0; i < propertyTexts.Length; i++)
        {
            propertyTexts[i].text = "";
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(updateUIValuesFromItem))
        {
            InventoryItem itemBasedOnDesiredType = InventoryItemGenerator.CreateInventoryItem(desiredItemType, desiredItemQuality, desiredItemLevel);
            UpdateTextBasedOnItem(itemBasedOnDesiredType);
        }
    }
}