using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item Stat", menuName = "Inventory Item/Create New Item Stat")]
public class SInventoryItemStat : ScriptableObject
{
    public string Name;
    public string Description;
    public ItemStatIDs Type;
    public int Value;
}

public enum ItemStatIDs
{
    Size,
    Quality,
    Quantity,
    EffectRange,

    ManaIncrease,
    DamageIncrease,

    HealingAmount,
    ComfortAmount,
}