[System.Serializable]
public struct InventoryItemStat
{
    [UnityEngine.SerializeField] string statName;
    [UnityEngine.SerializeField] string statDescription;
    [UnityEngine.SerializeField] ItemStatIDs statType;
    [UnityEngine.SerializeField] int statValue;

    public readonly string Name => statName;
    public readonly string Description => statDescription;
    public readonly ItemStatIDs Type => statType;
    public readonly int Value => statValue;

    public InventoryItemStat(string name, string description, ItemStatIDs type, int value)
    {
        statName = name;
        statDescription = description;
        statType = type;
        statValue = value;
    }
}