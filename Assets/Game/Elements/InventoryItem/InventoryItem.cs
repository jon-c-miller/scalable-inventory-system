using System.Collections.Generic;
using UnityEngine;

/// <summary> Represents an item instance; stores identifying info as well as randomly generate properties. </summary>
[System.Serializable]
public struct InventoryItem
{
    [Header("Generated Properties")]
    [SerializeField] ItemTypes itemType;            // Links to scriptable database to retrieve basic properties (name, etc.)
    [SerializeField] int itemID;                    // Identifies this particular generated item
    [SerializeField] ItemQualityIDs itemQuality;    // Determines stat count
    [SerializeField, Range(1, 20)] int itemLevel;   // Determines stats' values based on their range constraints
    [SerializeField] InventoryItemStat[] itemStats;

    public readonly ItemTypes ItemType => itemType;
    public readonly int ItemLevel => itemLevel;
    public readonly int ItemID => itemID;
    public readonly ItemQualityIDs ItemQuality => itemQuality;
    public readonly InventoryItemStat[] ItemStats => itemStats;

    public InventoryItem(ItemTypes type, ItemQualityIDs quality, int level, List<InventoryItemStat> stats)
    {
        itemType = type;
        itemID = Random.Range(10000000, 100000000);
        itemQuality = quality;
        itemLevel = level;
        itemStats = stats.ToArray();
    }
}