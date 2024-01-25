using UnityEngine;

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