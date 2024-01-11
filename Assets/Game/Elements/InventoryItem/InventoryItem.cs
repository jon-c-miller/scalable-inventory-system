using System.Collections.Generic;
using UnityEngine;

/// <summary> Represents an item instance; stores identifying info as well as randomly generate properties. </summary>
[System.Serializable]
public struct InventoryItem
{
    [Header("Base Properties")]
    [SerializeField] string itemName;
    [SerializeField] string itemDescription;
    [SerializeField] ItemIDs itemType;

    [Header("Generated Properties")]
    [SerializeField] ItemQualityIDs itemQuality;    // Determines stat count
    [SerializeField, Range(1, 20)] int itemLevel;   // Determines stats' values based on their range constraints
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