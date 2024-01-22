/// <summary> Provides a formatted text string based on a given item stat type. </summary>
public static class StatTextFormatter
{
    public static string FormatStatText(InventoryItemStat statType)
    {
        switch (statType.Type)
        {
            case ItemStatTypes.Size:
                return "";

            case ItemStatTypes.Quality:
                // Get the quality enum based on the stat's quality int value
                ItemQualityIDs quality = (ItemQualityIDs)statType.Value;

                // Set the item's quality text color based on its quality
                if (quality == ItemQualityIDs.Mundane)
                    return $"<color=white>{quality}</color>";
                else if (quality == ItemQualityIDs.Enchanted)
                    return $"<color=aqua>{quality}</color>";
                else return $"<color=purple>{quality}</color>";

            case ItemStatTypes.EffectRange:
                return $"{InventoryDatabase.StatDatabase[statType.Type].Name} {statType.Value}";

            case ItemStatTypes.ManaIncrease:
                return $"{InventoryDatabase.StatDatabase[statType.Type].Name} <color=blue>+{statType.Value}</color>";

            case ItemStatTypes.DamageIncrease:
                return $"{InventoryDatabase.StatDatabase[statType.Type].Name} <color=red>+{statType.Value}</color>";

            case ItemStatTypes.ComfortIncrease:
                return $"{InventoryDatabase.StatDatabase[statType.Type].Name} <color=teal>+{statType.Value}</color>";

            case ItemStatTypes.HealingAmount:
                return $"{InventoryDatabase.StatDatabase[statType.Type].Name} <color=green>+{statType.Value}</color>";
        }
        return "";
    }
}