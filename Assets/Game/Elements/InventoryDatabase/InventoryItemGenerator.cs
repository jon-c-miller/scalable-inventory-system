using System.Collections.Generic;
using UnityEngine;

/// <summary> Generates an item with randomized stats based on a given type, quality, and level. </summary>
public static class InventoryItemGenerator
{
    public static InventoryItem CreateRandomItemAvailableAtLevel(int level)
    {
        // Get a random item type based on the amount of item types and their unlock level, skipping None
        SInventoryItem itemTemplate = null;
        int itemTypesCount = System.Enum.GetValues(typeof(ItemIDs)).Length;
        int tries = 999;
        while (tries > 0)
        {
            tries--;
            ItemIDs randomItemType = (ItemIDs)Random.Range(1, itemTypesCount);
            SInventoryItem template = InventoryDatabase.ItemDatabase[randomItemType];
            if (template.UnlockLevel <= level)
            {
                itemTemplate = template;
                break;
            }
        }

        // Generate quality based on item's max quality limiter
        int optionalStatCount = 0;
        int itemQuality = 1;
        int itemMaxQuality = (int)itemTemplate.MaxQuality;
        if (itemMaxQuality > 1)
        {
            // Set stat count based on item quality
            itemQuality = Random.Range(1, itemMaxQuality + 1);
            optionalStatCount = itemQuality - 1;
        }

        // Generate core stats
        List<InventoryItemStat> generatedStats = new();
        for (int i = 0; i < itemTemplate.CoreStats.Count; i++)
        {
            // Get the stat template from the inventory database based on the ItemStatIDs key
            ItemStatIDs statID = itemTemplate.CoreStats[i];
            SInventoryItemStat statTemplate = InventoryDatabase.StatDatabase[statID];

            int statValue = GenerateStatValue(statTemplate.Value, statTemplate.Variance, itemQuality);
            generatedStats.Add(new InventoryItemStat(statTemplate.ID, statValue));
        }

        // Generate optional stats based on quality (uses the ItemQualityIDs enum value as the amount of stats)
        for (int i = 0; i < optionalStatCount; i++)
        {
            // Prevent errors with not enough possible stats to match item quality
            if (i >= itemTemplate.OptionalStats.Count) break;

            // Get the stat template from the inventory database based on the ItemStatIDs key
            ItemStatIDs statID = itemTemplate.OptionalStats[i];
            SInventoryItemStat statTemplate = InventoryDatabase.StatDatabase[statID];
            int statValue = GenerateStatValue(statTemplate.Value, statTemplate.Variance, itemQuality);

            // Add to stats collection
            generatedStats.Add(new InventoryItemStat(statTemplate.ID, statValue));
        }

        // Handle quantity
        int quantity = 1;
        bool isStackable = itemTemplate.CheckForCoreStat(ItemStatIDs.Stackable);
        if (isStackable)
            quantity = Random.Range(1, itemTemplate.MaxDropAmount + 1);

        return new InventoryItem(itemTemplate.ID, itemTemplate.Type, (ItemQualityIDs)itemQuality, generatedStats.ToArray(), isStackable, quantity);
    }

    static int GenerateStatValue(int baseValue, int variance, int quality)
    {
        // Factor in quality, increasing the stat's effectiveness every quality level above 1
        int qualityAdjustedValue = baseValue;
        qualityAdjustedValue *= quality;

        // Add variance based on quality
        int statVariance = Random.Range(0, variance + 1);
        int varianceAdjustedValue = qualityAdjustedValue + (statVariance * quality);

        return varianceAdjustedValue;
    }

    public static InventoryItem CreateItemOfSpecificTypeAndQuality(ItemIDs type, ItemQualityIDs quality)
    {
        SInventoryItem itemTemplate = InventoryDatabase.ItemDatabase[type];

        // Generate stat count based on quality (uses the ItemQualityIDs enum value as the amount of optional stats)
        int itemQuality = (int)quality;
        int itemMaxQuality = (int)itemTemplate.MaxQuality;

        // Limit quality to item's max
        if (itemQuality > itemMaxQuality)
            itemQuality = itemMaxQuality;

        // Generate core stats
        List<InventoryItemStat> generatedStats = new();
        for (int i = 0; i < itemTemplate.CoreStats.Count; i++)
        {
            // Get the stat template from the inventory database based on the ItemStatIDs key
            ItemStatIDs statID = itemTemplate.CoreStats[i];
            SInventoryItemStat statTemplate = InventoryDatabase.StatDatabase[statID];
            int statValue = GenerateStatValue(statTemplate.Value, statTemplate.Variance, itemQuality);
            generatedStats.Add(new InventoryItemStat(statTemplate.ID, statValue));
        }

        // Generate optional stats based on qualities over the lowest
        int optionalStatCount = itemQuality > 1 ? itemQuality - 1 : 1;
        for (int i = 0; i < optionalStatCount; i++)
        {
            // Prevent errors with not enough possible stats to match item quality
            if (i >= itemTemplate.OptionalStats.Count) break;

            // Get the stat template from the inventory database based on the ItemStatIDs key
            ItemStatIDs statID = itemTemplate.OptionalStats[i];
            SInventoryItemStat statTemplate = InventoryDatabase.StatDatabase[statID];
            int statValue = GenerateStatValue(statTemplate.Value, statTemplate.Variance, itemQuality);

            // Add to stats collection
            generatedStats.Add(new InventoryItemStat(statTemplate.ID, statValue));
        }

        // Handle quantity
        int quantity = 1;
        bool isStackable = itemTemplate.CheckForCoreStat(ItemStatIDs.Stackable);
        if (isStackable)
            quantity = Random.Range(1, itemTemplate.MaxDropAmount + 1);

        return new InventoryItem(itemTemplate.ID, itemTemplate.Type, (ItemQualityIDs)itemQuality, generatedStats.ToArray(), isStackable, quantity);
    }
}