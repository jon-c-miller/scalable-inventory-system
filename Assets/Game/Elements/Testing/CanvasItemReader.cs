using UnityEngine;

public class CanvasItemReader : MonoBehaviour
{
    [SerializeField] KeyCode updateUIValuesFromItem = KeyCode.Space;
    [Space]
    [SerializeField] SInventoryItem item;
    [Space]
    [SerializeField] UnityEngine.UI.Text nameText;
    [SerializeField] UnityEngine.UI.Text descriptionText;
    [SerializeField] UnityEngine.UI.Text[] propertyTexts;

    void UpdateTextBasedOnScriptableObject()
    {
        nameText.text = item.Name;
        descriptionText.text = item.Description;

        for (int i = 0; i < item.Stats.Length; i++)
        {
            propertyTexts[i].text = item.Stats[i].Name;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(updateUIValuesFromItem))
        {
            UpdateTextBasedOnScriptableObject();
        }
    }
}