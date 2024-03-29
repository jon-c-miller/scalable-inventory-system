using System.Collections.Generic;
using UnityEngine;

/// <summary> Represents an item template, replacing what would otherwise be part of a data model database. </summary>
[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Inventory Item/Create New Item")]
public class SInventoryItem : ScriptableObject
{
    public string Name;
    public string Description;
    public ItemIDs ID;
    public ItemTypes Type;
    public ItemStatIDs PrimaryStat;                     // The primary stat value for this item
    public List<ItemStatIDs> DefiningStats = new();     // Stats that define the way the item works (stackable, etc.)
    public List<ItemStatIDs> AssignableStats = new();   // All stats that can be assigned based on quality and chance
    public ItemQualityIDs MaxQuality = ItemQualityIDs.Mundane;
    [Space]
    public int MaxDropAmount;
    public int UnlockLevel;

    public bool CheckForDefiningStat(ItemStatIDs stat) => DefiningStats.Contains(stat);
}

public enum ItemTypes
{
    Aesthetica,
    Gear,
    Consumables,
}

public enum ItemIDs
{
    None,

    // Aesthetica
    JasmineFlower,

    // Gear
    CrystalFocus,

    // Consumables
    BurntCookie,
    SavoryPastry,
}

public enum ItemQualityIDs
{
    Mundane = 1,
    Enchanted = 2,
    Mystical = 3,
}