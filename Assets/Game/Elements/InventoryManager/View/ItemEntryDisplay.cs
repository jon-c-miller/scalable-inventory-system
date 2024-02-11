using UnityEngine;

/// <summary> An unmoving canvas element that acts as an item entry in a list-based inventory, displaying the name of the item. </summary>
public class ItemEntryDisplay : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Text nameText;
    [SerializeField] UnityEngine.UI.Text quantityText;

    public void UpdateTextColor(Color newColor) => nameText.color = newColor;

    public void SetEntryText(string itemName, string itemQuantity)
    {
        nameText.text = itemName;
        quantityText.text = itemQuantity;
    }

    public void ClearEntryText()
    {
        nameText.text = "";
        quantityText.text = "";
    }
}