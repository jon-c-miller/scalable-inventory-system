using UnityEngine;

[System.Serializable]
public struct InventoryItem
{
    [SerializeField] string itemName;
    [SerializeField] string itemDescription;
    [SerializeField] ItemIDs itemType;
    [SerializeField] InventoryItemStat[] itemStats;

    public readonly string ItemName => itemName;
    public readonly string ItemDescription => itemDescription;
    public readonly ItemIDs ItemType => itemType;
    public readonly InventoryItemStat[] ItemStats => itemStats;

    public InventoryItem(string name, string description, ItemIDs type, InventoryItemStat[] stats)
    {
        itemName = name;
        itemDescription = description;
        itemType = type;
        itemStats = stats;
    }
}