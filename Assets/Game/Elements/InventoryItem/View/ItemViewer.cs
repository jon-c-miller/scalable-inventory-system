using UnityEngine;

[System.Serializable]
public class ItemViewer : MonoBehaviour, IItemView
{
    [SerializeField] bool showQuantity;
    [SerializeField] bool colorNameBasedOnQuality;
    [Header("UI Fields")]
    [SerializeField] UnityEngine.UI.Text nameText;
    [SerializeField] UnityEngine.UI.Text descriptionText;
    [SerializeField] UnityEngine.UI.Text quantityText;
    [SerializeField] UnityEngine.UI.Text qualityText;
    [SerializeField] UnityEngine.UI.Text[] propertyTexts;

    public IItemView Interface => this;

    public void IUpdateEntryBasedOnItem(InventoryItem item)
    {
        if (item.ItemID == ItemIDs.None)
        {
            IInitialize();
            return;
        }

        // Use the item's type to retrieve name and description from the item database
        nameText.text = StatTextFormatter.FormatNameText(item, colorNameBasedOnQuality);
        descriptionText.text = InventoryDatabase.ItemDatabase[item.ItemID].Description;

        // Keep track of the property text index to allow flexibility based on property type
        int propertyTextIndex = 0;

        // Display item quantity if over 1 and showQuantity is true
        quantityText.text = item.ItemQuantity > 1 && showQuantity ? item.ItemQuantity.ToString() : "";

        qualityText.text = StatTextFormatter.FormatQualityText(item);

        // Clear the property texts
        for (int i = 0; i < propertyTexts.Length; i++)
        {
            propertyTexts[i].text = "";
        }

        // Assign the item stats' names to the property texts, skipping empty entries
        for (int i = 0; i < propertyTexts.Length; i++)
        {
            if (i < item.ItemSecondaryStats.Length)
            {
                string newText = StatTextFormatter.FormatStatText(item.ItemSecondaryStats[i]);

                if (newText != "")
                {
                    propertyTexts[propertyTextIndex].text = newText;
                    propertyTextIndex++;
                }
            }
        }
    }

    public void IInitialize()
    {
        nameText.text = "";
        descriptionText.text = "";
        quantityText.text = "";
        qualityText.text = "";

        for (int i = 0; i < propertyTexts.Length; i++)
        {
            propertyTexts[i].text = "";
        }
    }
}