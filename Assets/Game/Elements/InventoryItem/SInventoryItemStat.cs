using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item Stat", menuName = "Inventory Item/Create New Item Stat")]
public class SInventoryItemStat : ScriptableObject
{
    public string Name;
    public string Description;
    public ItemStatIDs ID;
    [Space]
    [Range(1, 10)] public int ValueLow;
    [Range(1, 10)] public int ValueHigh;
    public int PerLevelIncrease;
}

public enum ItemStatIDs
{
    // Common modifiers
    Size,
    EffectRange,

    // Persistent increases while under effect of item (equipped, near, etc.)
    ManaIncrease,
    DamageIncrease,
    ComfortIncrease,

    // One shot updates upon consumption
    HealingAmount,
}