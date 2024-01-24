using UnityEngine;

public class ItemReader : MonoBehaviour
{
    [Header("UI Fields")]
    [SerializeField] UnityEngine.UI.Text nameText;
    [SerializeField] UnityEngine.UI.Text descriptionText;
    [SerializeField] UnityEngine.UI.Text quantityText;
    [SerializeField] UnityEngine.UI.Text qualityText;
    [SerializeField] UnityEngine.UI.Text[] propertyTexts;

    public void UpdateTextBasedOnItem(InventoryItem item)
    {
        // Use the item's type to retrieve name and description from the item database
        nameText.text = InventoryDatabase.ItemDatabase[item.ItemType].Name;
        descriptionText.text = InventoryDatabase.ItemDatabase[item.ItemType].Description;

        // Keep track of the property text index to allow flexibility based on property type
        int propertyTextIndex = 0;

        // Display item quantity if over 1
        quantityText.text = item.ItemQuantity > 1 ? item.ItemQuantity.ToString() : "";

        qualityText.text = StatTextFormatter.FormatQualityText(item.ItemQuality);

        for (int i = 0; i < item.ItemStats.Length; i++)
        {
            propertyTexts[propertyTextIndex].text = StatTextFormatter.FormatStatText(item.ItemStats[i]);
            propertyTextIndex++;
        }
    }

    void Awake()
    {
        // Initialize text for properties
        for (int i = 0; i < propertyTexts.Length; i++)
        {
            propertyTexts[i].text = "";
        }
    }
}