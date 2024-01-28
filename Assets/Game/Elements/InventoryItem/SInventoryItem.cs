using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Inventory Item/Create New Item")]
public class SInventoryItem : ScriptableObject
{
    public string Name;
    public string Description;
    public ItemIDs ID;
    public ItemTypes Type;
    public List<SInventoryItemStat> CoreStats = new();      // Stats every instance of the item will have
    public List<SInventoryItemStat> OptionalStats = new();  // Stats added based on quality
    public ItemQualityIDs MaxQuality = ItemQualityIDs.Mundane;
    [Space]
    public int MaxDropAmount;
    public int UnlockLevel;

    public bool CheckForCoreStat(ItemStatIDs stat)
    {
        for (int i = 0; i < CoreStats.Count; i++)
        {
            if (CoreStats[i].ID == stat)
                return true;
        }
        return false;
    }
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