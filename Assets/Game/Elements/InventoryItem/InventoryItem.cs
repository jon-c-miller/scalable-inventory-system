using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] SInventoryItem properties;

    public string Name => properties.Name;
    public string Description => properties.Description;
    public ItemIDs Type => properties.Type;
}