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
    [SerializeField] int itemQuantity;
    [SerializeField] bool isStackable;

    public readonly ItemTypes ItemType => itemType;
    public readonly int ItemLevel => itemLevel;
    public readonly int ItemID => itemID;
    public readonly ItemQualityIDs ItemQuality => itemQuality;
    public readonly InventoryItemStat[] ItemStats => itemStats;
    public int ItemQuantity { readonly get => itemQuantity; set => itemQuantity = value; }
    public readonly bool IsStackable => isStackable;

    public readonly InventoryItem CopyItem(int newQuantity)
    {
        // Copy the current stats array to the new copy
        InventoryItemStat[] copyOfStats = new InventoryItemStat[itemStats.Length];
        itemStats.CopyTo(copyOfStats, 0); 
        return new(itemType, itemQuality, itemLevel, copyOfStats, isStackable, newQuantity);
    }

    public InventoryItem(ItemTypes type, ItemQualityIDs quality, int level, InventoryItemStat[] stats, bool stackable, int quantity)
    {
        itemType = type;
        itemID = stackable ? 99 : Random.Range(10000000, 100000000);
        itemQuality = quality;
        itemLevel = level;
        itemStats = stats;
        isStackable = stackable;
        itemQuantity = quantity;
    }
}