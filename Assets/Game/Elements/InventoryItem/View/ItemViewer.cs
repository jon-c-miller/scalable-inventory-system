using UnityEngine;

/// <summary> An arbitrary implementation of an item viewer in 'tooltip' form. Designed to test and illustrate use of the API. </summary>
[System.Serializable]
public class ItemViewer : MonoBehaviour, IItemView
{
    [Header("Configuration")]
    [SerializeField] bool showQuantity;
    [SerializeField] bool colorNameBasedOnQuality;

    [Header("UI Fields")]
    [SerializeField] UnityEngine.UI.Text nameText;
    [SerializeField] UnityEngine.UI.Text descriptionText;
    [SerializeField] UnityEngine.UI.Text primaryStatText;
    [SerializeField] UnityEngine.UI.Text[] secondaryStatTexts;
    [SerializeField] UnityEngine.UI.Text quantityText;
    [SerializeField] UnityEngine.UI.Text qualityText;

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
        descriptionText.text = Game.Instance.InventoryGetItemDescription(item.ItemID);

        // Keep track of the stat text index to allow flexibility based on stat type
        int statTextIndex = 0;

        // Display item quantity if over 1 and showQuantity is true
        quantityText.text = item.ItemQuantity > 1 && showQuantity ? item.ItemQuantity.ToString() : "";

        qualityText.text = StatTextFormatter.FormatQualityText(item);

        // Clear the stat texts
        primaryStatText.text = "";
        for (int i = 0; i < secondaryStatTexts.Length; i++)
        {
            secondaryStatTexts[i].text = "";
        }

        // Assign the primary stat name
        string newText = StatTextFormatter.FormatStatText(item.ItemPrimaryStat);
        primaryStatText.text = newText;

        // Assign the secondary stats' names, skipping empty entries
        for (int i = 0; i < secondaryStatTexts.Length; i++)
        {
            if (i < item.ItemSecondaryStats.Length)
            {
                newText = StatTextFormatter.FormatStatText(item.ItemSecondaryStats[i]);

                if (newText != "")
                {
                    secondaryStatTexts[statTextIndex].text = newText;
                    statTextIndex++;
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
        primaryStatText.text = "";

        for (int i = 0; i < secondaryStatTexts.Length; i++)
        {
            secondaryStatTexts[i].text = "";
        }
    }
}