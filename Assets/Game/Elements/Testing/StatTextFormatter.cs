using System.Drawing;
/// <summary> Provides a formatted text string based on a given item stat type. </summary>
public static class StatTextFormatter
{
    public static string FormatQualityText(InventoryItem item)
    {
        // Set the item's quality text color based on its quality and level
        ItemQualityIDs quality = item.ItemQuality;
        if (quality == ItemQualityIDs.Mundane)
            return $"<color=white>{quality}</color>";
        else if (quality == ItemQualityIDs.Enchanted)
            return $"<color=aqua>{quality}</color>";
        else return $"<color=purple>{quality}</color>";
    }

    public static string FormatStatText(InventoryItemStat statType)
    {
        switch (statType.Type)
        {
            case ItemStatIDs.EffectRange:
                return $"{InventoryDatabase.StatDatabase[statType.Type].Name} {statType.Value}";

            case ItemStatIDs.ManaIncrease:
                return $"<color=lightblue>+{statType.Value}% {InventoryDatabase.StatDatabase[statType.Type].Name}</color>";

            case ItemStatIDs.DamageIncrease:
                return $"<color=red>+{statType.Value}% {InventoryDatabase.StatDatabase[statType.Type].Name}</color>";

            case ItemStatIDs.ComfortIncrease:
                return $"<color=teal>+{statType.Value}% {InventoryDatabase.StatDatabase[statType.Type].Name}</color>";

            case ItemStatIDs.Healing:
                return $"<color=pink>+{statType.Value}% {InventoryDatabase.StatDatabase[statType.Type].Name}</color>";

            case ItemStatIDs.ManaRecovery:
                return $"<color=lightblue>+{statType.Value}% {InventoryDatabase.StatDatabase[statType.Type].Name}</color>";
        }
        return "";
    }
}