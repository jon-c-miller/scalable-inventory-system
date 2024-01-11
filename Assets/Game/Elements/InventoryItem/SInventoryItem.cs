using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Inventory Item/Create New Item")]
public class SInventoryItem : ScriptableObject
{
    public string Name;
    public string Description;
    public ItemIDs Type;
    public InventoryItemStat[] PossibleStats;
}

public enum ItemIDs
{
    // Aesthetica
    JasmineFlower,

    // Gear
    CrystalFocus,

    // Consumables
    SavoryPastry,
}