using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item Stat", menuName = "Inventory Item/Create New Item Stat")]
public class SInventoryItemStat : ScriptableObject
{
    public string Name;
    public string Description;
    public ItemStatIDs Type;
    [Range(1, 10)] public int ValueLow;
    [Range(1, 10)] public int ValueHigh;
}

public enum ItemStatIDs
{
    // Common modifiers
    Size,
    Quality,
    Quantity,
    EffectRange,

    // Persistent increases while under effect of item (equipped, near, etc.)
    ManaIncrease,
    DamageIncrease,
    ComfortIncrease,

    // One shot updates upon consumption
    HealingAmount,
}