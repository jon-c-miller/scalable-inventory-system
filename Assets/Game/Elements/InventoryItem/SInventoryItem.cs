using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Inventory Item/Create New Item")]
public class SInventoryItem : ScriptableObject
{
    public string Name;
    public string Description;
    public ItemIDs Type;
    [Space]
    public int CurrentAmount;
}

public enum ItemIDs
{
    JasmineFlower,
    CrystalFocus,
    SavoryPastry,
}