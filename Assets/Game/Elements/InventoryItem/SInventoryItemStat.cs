using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item Stat", menuName = "Inventory Item/Create New Item Stat")]
public class SInventoryItemStat : ScriptableObject
{
    public string Name;
    public string Description;
    public ItemStatIDs ID;
    [Space]
    [Range(1, 300)] public int Value = 1;
    [Range(0, 100)] public int Variance;
}

public enum ItemStatIDs
{
    // Core modifiers that fundamentally define the item
    Stackable,
    EffectRange,

    // Persistent increases while under effect of item (equipped, near, etc.)
    ManaIncrease,
    DamageIncrease,
    ComfortIncrease,

    // One shot updates upon consumption
    Healing,
    ManaRecovery,
}