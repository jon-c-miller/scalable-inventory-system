using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Inventory Item/Create New Item")]
public class SInventoryItem : ScriptableObject
{
    public string Name;
    public string Description;
    public ItemIDs Type;
    public List<SInventoryItemStat> PossibleStats = new();
}

public enum ItemIDs
{
    None,

    // Aesthetica
    JasmineFlower,

    // Gear
    CrystalFocus,

    // Consumables
    SavoryPastry,
}

public enum ItemQualityIDs
{
    Mundane = 1,
    Enchanted = 2,
    Mystical = 2,
}