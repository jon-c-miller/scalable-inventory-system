using UnityEngine;

public class InventoryItemStat : MonoBehaviour
{
    [SerializeField] SInventoryItemStat properties;

    public string Name => properties.Name;
    public string Description => properties.Description;
    public ItemStatIDs Type => properties.Type;
    public int Value => properties.Value;
}