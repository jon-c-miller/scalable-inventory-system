[System.Serializable]
public struct InventoryItemStat
{
    [UnityEngine.SerializeField] string statName;
    [UnityEngine.SerializeField] string statDescription;
    [UnityEngine.SerializeField] ItemStatTypes statType;
    [UnityEngine.SerializeField] int statValue;

    public readonly string Name => statName;
    public readonly string Description => statDescription;
    public readonly ItemStatTypes Type => statType;
    public readonly int Value => statValue;

    public InventoryItemStat(string name, string description, ItemStatTypes type, int value)
    {
        statName = name;
        statDescription = description;
        statType = type;
        statValue = value;
    }
}