using UnityEngine;

/// <summary> Represents an item instance; stores identifying info as well as randomly generate properties. </summary>
[System.Serializable]
public struct InventoryItem
{
    [Header("Generated Properties")]
    [SerializeField] int itemHash;                  // Identifies this particular generated item
    [SerializeField] ItemIDs itemID;                // Links to scriptable database to retrieve basic properties (name, etc.)
    [SerializeField] ItemTypes itemType;            // Identifies this item's category
    [SerializeField] ItemQualityIDs itemQuality;    // Determines stat count based on ItemQualityIDs value
    [SerializeField] InventoryItemStat[] itemStats; // All generated stats, including core and optional
    [SerializeField] int itemQuantity;
    [SerializeField] bool isStackable;

    public readonly int ItemHash => itemHash;
    public readonly ItemIDs ItemID => itemID;
    public readonly ItemTypes ItemType => itemType;
    public readonly ItemQualityIDs ItemQuality => itemQuality;
    public readonly InventoryItemStat[] ItemStats => itemStats;
    public int ItemQuantity { readonly get => itemQuantity; set => itemQuantity = value; }
    public readonly bool IsStackable => isStackable;

    public readonly InventoryItem CopyItem()
    {
        // Copy the current stats array to the new copy
        InventoryItemStat[] copyOfStats = new InventoryItemStat[itemStats.Length];
        itemStats.CopyTo(copyOfStats, 0); 
        return new(itemID, itemType, itemQuality, copyOfStats, isStackable, itemQuantity);
    }

    public readonly InventoryItem CopyItem(int newQuantity)
    {
        InventoryItemStat[] copyOfStats = new InventoryItemStat[itemStats.Length];
        itemStats.CopyTo(copyOfStats, 0); 
        return new(itemID, itemType, itemQuality, copyOfStats, isStackable, newQuantity);
    }

    public InventoryItem(ItemIDs name, ItemTypes type, ItemQualityIDs quality, InventoryItemStat[] stats, bool stackable, int quantity)
    {
        itemID = name;
        itemType = type;
        itemHash = stackable ? 99 : Random.Range(10000000, 100000000);
        itemQuality = quality;
        itemStats = stats;
        isStackable = stackable;
        itemQuantity = quantity;
    }
}