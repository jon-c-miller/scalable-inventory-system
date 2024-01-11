using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct InventoryItem
{
    [SerializeField] string itemName;
    [SerializeField] string itemDescription;
    [SerializeField] ItemIDs itemType;
    [Space]
    [SerializeField] ItemQualityIDs itemQuality;    // Determines stat count
    [SerializeField] int itemLevel;                 // Determines stats' values based on their range constraints
    [SerializeField] InventoryItemStat[] itemStats;

    public readonly string ItemName => itemName;
    public readonly string ItemDescription => itemDescription;
    public readonly ItemIDs ItemType => itemType;
    public readonly int ItemLevel => itemLevel;
    public readonly ItemQualityIDs ItemQuality => itemQuality;
    public readonly InventoryItemStat[] ItemStats => itemStats;

    public InventoryItem(string name, string description, ItemIDs type, ItemQualityIDs quality, int level, List<InventoryItemStat> stats)
    {
        itemName = name;
        itemDescription = description;
        itemType = type;
        itemQuality = quality;
        itemLevel = level;
        itemStats = stats.ToArray();
    }
}