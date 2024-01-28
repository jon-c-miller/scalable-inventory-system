[System.Serializable]
public struct InventoryItemStat
{
    [UnityEngine.SerializeField] ItemStatIDs statID;
    [UnityEngine.SerializeField] int statValue;

    public readonly ItemStatIDs Type => statID;
    public readonly int Value => statValue;

    public InventoryItemStat(ItemStatIDs id, int value)
    {
        statID = id;
        statValue = value;
    }
}