[System.Serializable]
public struct InventoryItemStat
{
    [UnityEngine.SerializeField] ItemStatIDs statID;
    [UnityEngine.SerializeField] int statValue;
    [UnityEngine.SerializeField] bool isPercentage;

    public readonly ItemStatIDs Type => statID;
    public readonly int Value => statValue;
    public readonly bool IsPercentage => isPercentage;

    public InventoryItemStat(ItemStatIDs id, int value, bool isPercentage)
    {
        statID = id;
        statValue = value;
        this.isPercentage = isPercentage;
    }
}