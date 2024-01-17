using System.Collections.Generic;
using UnityEngine;

/// <summary> Stores a map of ItemStatIDs to matching scriptable objects for item template retrieval. </summary>
public class InventoryItemStatDatabase : MonoBehaviour
{
    [SerializeField] SInventoryItemStat[] itemStatTemplates;
    
    public static readonly Dictionary<ItemStatTypes, SInventoryItemStat> ItemStatDatabase = new();

    void BuildDatabase()
    {
        // Map all scriptable object item stat templates to their types for simple retrieval by stat id
        for (int i = 0; i < itemStatTemplates.Length; i++)
        {
            ItemStatDatabase.Add(itemStatTemplates[i].Type, itemStatTemplates[i]);
        }
    }

    void Awake()
    {
        BuildDatabase();
    }
}