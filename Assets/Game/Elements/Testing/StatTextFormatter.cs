/// <summary> Provides a formatted text string based on a given item stat type. </summary>
public static class StatTextFormatter
{
    public static string FormatNameText(InventoryItem item, bool colorBasedOnQuality)
    {
        string name = InventoryDatabase.GetItemName(item.ItemID);

        if (colorBasedOnQuality)
        {
            ItemQualityIDs quality = item.ItemQuality;
            if (quality == ItemQualityIDs.Mundane)
                return $"<color=white>{name}</color>";
            else if (quality == ItemQualityIDs.Enchanted)
                return $"<color=aqua>{name}</color>";
            else return $"<color=purple>{name}</color>";
        }

        return name;
    }

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

    public static string FormatStatText(InventoryItemStat itemStat)
    {
        string addendum = itemStat.IsPercentage ? "%" : "";
        switch (itemStat.Type)
        {
            case ItemStatIDs.EffectRange:
                return $"{Game.Instance.InventoryGetItemStatName(itemStat.Type)} {itemStat.Value}";

            case ItemStatIDs.ManaIncrease:
                return $"<color=lightblue>+{itemStat.Value}{addendum} {Game.Instance.InventoryGetItemStatName(itemStat.Type)}</color>";

            case ItemStatIDs.DamageIncrease:
                return $"<color=red>+{itemStat.Value}{addendum} {Game.Instance.InventoryGetItemStatName(itemStat.Type)}</color>";

            case ItemStatIDs.ComfortIncrease:
                return $"<color=teal>+{itemStat.Value}{addendum} {Game.Instance.InventoryGetItemStatName(itemStat.Type)}</color>";

            case ItemStatIDs.Healing:
                return $"<color=pink>+{itemStat.Value}{addendum} {Game.Instance.InventoryGetItemStatName(itemStat.Type)}</color>";

            case ItemStatIDs.ManaRecovery:
                return $"<color=lightblue>+{itemStat.Value}{addendum} {Game.Instance.InventoryGetItemStatName(itemStat.Type)}</color>";
        }
        return "";
    }
}