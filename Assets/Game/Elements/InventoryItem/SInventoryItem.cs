using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Inventory Item/Create New Item")]
public class SInventoryItem : ScriptableObject
{
    public string Name;
    public string Description;
    public ItemIDs ID;
    public ItemTypes Type;
    public List<ItemStatIDs> CoreStats = new();         // Stats every instance of the item will have
    public List<ItemStatIDs> OptionalStats = new();     // Stats generated based on quality
    public ItemQualityIDs MaxQuality = ItemQualityIDs.Mundane;
    [Space]
    public int MaxDropAmount;
    public int UnlockLevel;

    public bool CheckForCoreStat(ItemStatIDs stat) => CoreStats.Contains(stat);
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