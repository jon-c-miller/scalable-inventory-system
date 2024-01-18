[System.Serializable]
public struct InventoryItemStat
{
    [UnityEngine.SerializeField] ItemStatTypes statType;
    [UnityEngine.SerializeField] int statValue;

    public readonly ItemStatTypes Type => statType;
    public readonly int Value => statValue;

    public InventoryItemStat(ItemStatTypes type, int value)
    {
        statType = type;
        statValue = value;
    }
}