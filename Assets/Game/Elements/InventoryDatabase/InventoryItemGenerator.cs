using System.Collections.Generic;
using UnityEngine;

/// <summary> Generates an item with randomized stats based on a given type, quality, and level. </summary>
public class InventoryItemGenerator : MonoBehaviour
{
    public InventoryItem CreateInventoryItem(ItemIDs type, ItemQualityIDs quality, int level)
    {
        SInventoryItem itemTemplate = InventoryItemDatabase.ItemDatabase[type];

        // Set stat count based on desired quality by using the enum entry as the amount of stats
        int numberOfStats = (int)quality;

        // Generate stat types
        List<InventoryItemStat> generatedStats = new();
        for (int i = 0; i < numberOfStats; i++)
        {
            // Prevent errors with not enough possible stats to match item quality
            if (i > itemTemplate.PossibleStats.Count) break;

            SInventoryItemStat newStat = itemTemplate.PossibleStats[i];

            // Randomize stat values based on low/high constraints and desired level
            int randomBaseValue = Random.Range(newStat.ValueLow, newStat.ValueHigh + 1);
            int valueAfterLevelModifier = randomBaseValue * level;
            int valueAfterPerLevelIncrease = valueAfterLevelModifier * newStat.PerLevelIncrease;

            // Add to stats collection
            generatedStats.Add(new InventoryItemStat(newStat.Name, newStat.Description, newStat.Type, valueAfterPerLevelIncrease));
        }

        return new InventoryItem(itemTemplate.Name, itemTemplate.Description, itemTemplate.Type, quality, level, generatedStats);
    }
}