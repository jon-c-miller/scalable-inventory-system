using UnityEngine;

/// <summary> Represents an item instance; stores identifying info as well as randomly generate properties. </summary>
[System.Serializable]
public struct InventoryItem
{
    [Header("Generated Properties")]
    [SerializeField] int hash;                              // Identifies this particular generated item
    [SerializeField] ItemIDs id;                            // Links to SO database to retrieve basic properties (name, etc.)
    [SerializeField] ItemTypes type;                        // Identifies this item's category
    [SerializeField] ItemQualityIDs quality;                // Determines stat count based on ItemQualityIDs value
    [SerializeField] InventoryItemStat primaryStat;         // Primary stat that defines this item's focus (damage, armor, etc.)
    [SerializeField] InventoryItemStat[] secondaryStats;    // All secondary generated stats
    [SerializeField] int level;
    [SerializeField] int quantity;
    [SerializeField] bool isStackable;

    public readonly int ItemHash => hash;
    public readonly ItemIDs ItemID => id;
    public readonly ItemTypes ItemType => type;
    public readonly ItemQualityIDs ItemQuality => quality;
    public readonly InventoryItemStat ItemPrimaryStat => primaryStat;
    public readonly InventoryItemStat[] ItemSecondaryStats => secondaryStats;
    public int ItemQuantity { readonly get => quantity; set => quantity = value; }
    public readonly int ItemLevel => level;
    public readonly bool IsStackable => isStackable;

    public readonly InventoryItem CopyItem()
    {
        // Copy the current stats array to the new copy
        InventoryItemStat[] copyOfSecondaryStats = new InventoryItemStat[secondaryStats.Length];
        secondaryStats.CopyTo(copyOfSecondaryStats, 0); 
        return new(id, type, quality, primaryStat, copyOfSecondaryStats, isStackable, level, quantity);
    }

    public readonly InventoryItem CopyItem(int newQuantity)
    {
        InventoryItemStat[] copyOfSecondaryStats = new InventoryItemStat[secondaryStats.Length];
        secondaryStats.CopyTo(copyOfSecondaryStats, 0);
        return new(id, type, quality, primaryStat, copyOfSecondaryStats, isStackable, level, newQuantity);
    }

    public InventoryItem(ItemIDs id, ItemTypes type, ItemQualityIDs quality, InventoryItemStat primaryStat, InventoryItemStat[] secondaryStats, bool isStackable, int level, int quantity)
    {
        this.id = id;
        this.type = type;
        this.quality = quality;
        this.primaryStat = primaryStat;
        this.secondaryStats = secondaryStats;
        this.level = level;
        this.quantity = quantity;
        this.isStackable = isStackable;

        // Generate a unique hash for non-stackable items, and an arbitrary hash of 99 for stackable items
        hash = isStackable ? 99 : Random.Range(10000000, 100000000);
    }
}