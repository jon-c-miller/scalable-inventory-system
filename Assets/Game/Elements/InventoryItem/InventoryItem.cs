using System.Collections.Generic;
using UnityEngine;

/// <summary> Represents an item instance; stores identifying info as well as randomly generate properties. </summary>
[System.Serializable]
public struct InventoryItem
{
    [Header("Base Properties")]
    [SerializeField] string itemName;           // Remove these properties? They aren't actually needed, and can be retrieved from
    [SerializeField] string itemDescription;    // the item database via itemType
    [SerializeField] ItemTypes itemType;

    [Header("Generated Properties")]
    [SerializeField] int itemID;                    // Unique identifier to match this generated item
    [SerializeField] ItemQualityIDs itemQuality;    // Determines stat count
    [SerializeField, Range(1, 20)] int itemLevel;   // Determines stats' values based on their range constraints
    [SerializeField] InventoryItemStat[] itemStats;

    public readonly string ItemName => itemName;
    public readonly string ItemDescription => itemDescription;
    public readonly ItemTypes ItemType => itemType;
    public readonly int ItemLevel => itemLevel;
    public readonly int ItemID => itemID;
    public readonly ItemQualityIDs ItemQuality => itemQuality;
    public readonly InventoryItemStat[] ItemStats => itemStats;

    public InventoryItem(string name, string description, ItemTypes type, ItemQualityIDs quality, int level, List<InventoryItemStat> stats)
    {
        itemName = name;
        itemDescription = description;
        itemType = type;
        itemID = Random.Range(10000000, 100000000);
        itemQuality = quality;
        itemLevel = level;
        itemStats = stats.ToArray();
    }
}