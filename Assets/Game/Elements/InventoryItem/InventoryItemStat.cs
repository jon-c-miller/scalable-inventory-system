[System.Serializable]
public struct InventoryItemStat
{
    [UnityEngine.SerializeField] string statName;
    [UnityEngine.SerializeField] string statDescription;
    [UnityEngine.SerializeField] ItemStatIDs statType;
    public string Value;

    public string Name => statName;
    public string Description => statDescription;
    public ItemStatIDs Type => statType;

    public InventoryItemStat(string name, string description, ItemStatIDs type)
    {
        statName = name;
        statDescription = description;
        statType = type;
        Value = "";
    }
}