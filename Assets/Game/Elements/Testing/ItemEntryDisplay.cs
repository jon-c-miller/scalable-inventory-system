using UnityEngine;

public class ItemEntryDisplay : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Text nameText;
    [SerializeField] UnityEngine.UI.Text quantityText;

    public void SetEntryText(string itemName, int itemQuantity)
    {
        nameText.text = itemName;
        quantityText.text = itemQuantity.ToString();
    }

    public void ClearEntryText()
    {
        nameText.text = "";
        quantityText.text = "";
    }
}