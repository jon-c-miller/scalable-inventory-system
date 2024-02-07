using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item Stat", menuName = "Inventory Item/Create New Item Stat")]
public class SInventoryItemStat : ScriptableObject
{
    public string Name;
    public string Description;
    public ItemStatIDs ID;
    [Space]
    [Range(0.01f, 300f)] public float Modifier = 1;
    [Range(0, 100f)] public float Variance;
}

public enum ItemStatIDs
{
    None,

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