using System.Collections.Generic;
using UnityEngine;

/// <summary> An extension of InventoryManager that handles generating either a specific item or one with randomized stats based on a given level. </summary>
public class InventoryGenerateItem
{
    public InventoryItem CreateRandomItemAvailableAtLevel(int level)
    {
        // Get a random item type based on the amount of item types and their unlock level, skipping None
        SInventoryItem itemTemplate = null;
        int itemTypesCount = System.Enum.GetValues(typeof(ItemIDs)).Length;
        int tries = 999;
        while (tries > 0)
        {
            tries--;
            ItemIDs randomItemType = (ItemIDs)Random.Range(1, itemTypesCount);
            SInventoryItem template = Game.Instance.InventoryGetItemTemplate(randomItemType);
            if (template.UnlockLevel <= level)
            {
                itemTemplate = template;
                break;
            }
        }

        // Generate quality based on item's max quality limiter (use ItemQualityIDs enum value as the amount of stats)
        int secondaryStatAmountLimit = 0;
        int itemQuality = 1;
        int itemMaxQuality = (int)itemTemplate.MaxQuality;
        if (itemMaxQuality > 1)
        {
            // Add secondary stats for any quality above 1 (lowest level quality)
            itemQuality = Random.Range(1, itemMaxQuality + 1);
            secondaryStatAmountLimit = itemQuality - 1;
        }

        // If there is a primary stat, assign value based on that stat's modifier/variance and item level
        InventoryItemStat primaryStat = new();
        ItemStatIDs primaryStatID = itemTemplate.PrimaryStat;
        if (primaryStatID != ItemStatIDs.None)
        {
            SInventoryItemStat primaryStatTemplate = Game.Instance.InventoryGetItemStatTemplate(primaryStatID);
            int primaryStatValue = GenerateStatValue(primaryStatTemplate.Modifier, primaryStatTemplate.Variance, level, itemQuality);
            primaryStat = new(primaryStatID, primaryStatValue, primaryStatTemplate.IsPercentage);
        }

        // Generate secondary stats based on quality 
        List<InventoryItemStat> secondaryStats = new();
        for (int i = 0; i < secondaryStatAmountLimit; i++)
        {
            // Prevent errors with not enough possible stats to match the max determined by item quality
            if (i >= itemTemplate.AssignableStats.Count) break;

            // Get the stat template from the inventory database based on the ItemStatIDs key
            ItemStatIDs statID = itemTemplate.AssignableStats[i];
            SInventoryItemStat statTemplate = Game.Instance.InventoryGetItemStatTemplate(statID);
            int statValue = GenerateStatValue(statTemplate.Modifier, statTemplate.Variance, level, itemQuality);

            // Add to stats collection
            secondaryStats.Add(new InventoryItemStat(statTemplate.ID, statValue, statTemplate.IsPercentage));
        }

        // Handle quantity
        int quantity = 1;
        bool isStackable = itemTemplate.CheckForDefiningStat(ItemStatIDs.Stackable);
        if (isStackable)
            quantity = Random.Range(1, itemTemplate.MaxDropAmount + 1);

        return new InventoryItem(itemTemplate.ID, itemTemplate.Type, (ItemQualityIDs)itemQuality, primaryStat, secondaryStats.ToArray(), isStackable, level, quantity);
    }

    public InventoryItem CreateSpecificItem(ItemIDs type, ItemQualityIDs quality, int level)
    {
        SInventoryItem itemTemplate = Game.Instance.InventoryGetItemTemplate(type);

        // Generate stat count based on quality (uses the ItemQualityIDs enum value as the amount of optional stats)
        int itemQuality = (int)quality;
        int itemMaxQuality = (int)itemTemplate.MaxQuality;

        // Limit quality to item's max
        if (itemQuality > itemMaxQuality)
            itemQuality = itemMaxQuality;

        // Assign value to primary stat based on that stat's modifier/variance and item level
        InventoryItemStat primaryStat = new();
        ItemStatIDs primaryStatID = itemTemplate.PrimaryStat;
        if (primaryStatID != ItemStatIDs.None)
        {
            SInventoryItemStat primaryStatTemplate = Game.Instance.InventoryGetItemStatTemplate(primaryStatID);
            int primaryStatValue = GenerateStatValue(primaryStatTemplate.Modifier, primaryStatTemplate.Variance, 0, itemQuality);
            primaryStat = new(primaryStatID, primaryStatValue, primaryStatTemplate.IsPercentage);
        }

        // Generate secondary stats for qualities above lowest 
        int secondaryStatAmountLimit = itemQuality > 1 ? itemQuality - 1 : 0;
        List<InventoryItemStat> secondaryStats = new();
        for (int i = 0; i < secondaryStatAmountLimit; i++)
        {
            // Prevent errors with not enough possible stats to match the max determined by item quality
            if (i >= itemTemplate.AssignableStats.Count) break;

            // Get the stat template from the inventory database based on the ItemStatIDs key
            ItemStatIDs statID = itemTemplate.AssignableStats[i];
            SInventoryItemStat statTemplate = Game.Instance.InventoryGetItemStatTemplate(statID);
            int statValue = GenerateStatValue(statTemplate.Modifier, statTemplate.Variance, 0, itemQuality);

            // Add to stats collection
            secondaryStats.Add(new InventoryItemStat(statTemplate.ID, statValue, statTemplate.IsPercentage));
        }

        // Handle quantity
        int quantity = 1;
        bool isStackable = itemTemplate.CheckForDefiningStat(ItemStatIDs.Stackable);
        if (isStackable)
            quantity = Random.Range(1, itemTemplate.MaxDropAmount + 1);

        return new InventoryItem(itemTemplate.ID, itemTemplate.Type, (ItemQualityIDs)itemQuality, primaryStat, secondaryStats.ToArray(), isStackable, level, quantity);
    }

    int GenerateStatValue(float statModifier, float statVariance, int itemLevel, int quality)
    {
        // Factor in quality, increasing the stat's effectiveness every quality level above 1
        float qualityAdjustedValue = statModifier;
        qualityAdjustedValue *= quality;

        // Adjust the value based on level if given level is over 0
        float levelAdjustedValue = itemLevel > 0 ? qualityAdjustedValue * itemLevel : qualityAdjustedValue;

        // Add variance based on quality
        float generatedVariance = Random.Range(0, statVariance);
        float varianceAdjustedValue = levelAdjustedValue + (generatedVariance * quality);

        return (int)varianceAdjustedValue;
    }
}