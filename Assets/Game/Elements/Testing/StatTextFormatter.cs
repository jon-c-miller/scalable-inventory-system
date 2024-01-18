using System.Drawing;

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
                if (quality == ItemQualityIDs.Mundane)
                    return $"<color=white>{quality.ToString()}</color>";
                else if (quality == ItemQualityIDs.Enchanted)
                    return $"<color=aqua>{quality.ToString()}</color>";
                else return $"<color=gold>{quality.ToString()}</color>";

            case ItemStatTypes.Quantity:
                return $"{statType.Value}";

            case ItemStatTypes.EffectRange:
                return $"Range: {statType.Value}";

            case ItemStatTypes.ManaIncrease:
                return $"<color=blue>+ {statType.Value}</color> Mana";

            case ItemStatTypes.DamageIncrease:
                return $"<color=red>+ {statType.Value}</color> Damage";

            case ItemStatTypes.ComfortIncrease:
                return $"<color=teal>+ {statType.Value}</color> Comfort";

            case ItemStatTypes.HealingAmount:
                return $"<color=green>{statType.Value}</color> Health Recovered";
        }
        return "";
    }
}