using System.Collections.Generic;
using UnityEngine;

/// <summary> Stores maps of item and stat types to matching scriptable objects for item and item stat template retrieval. </summary>
public class InventoryDatabase : MonoBehaviour
{
    [SerializeField] SInventoryItem[] itemTemplates;
    [SerializeField] SInventoryItemStat[] statTemplates;
    
    public static readonly Dictionary<ItemIDs, SInventoryItem> ItemDatabase = new();
    public static readonly Dictionary<ItemStatIDs, SInventoryItemStat> StatDatabase = new();

    void BuildItemDatabase()
    {
        // Map all scriptable object item templates to their ids for simple retrieval
        for (int i = 0; i < itemTemplates.Length; i++)
        {
            ItemDatabase.Add(itemTemplates[i].ID, itemTemplates[i]);
        }
    }

    void BuildStatDatabase()
    {
        // Map all scriptable object item stat templates to their types for simple retrieval by stat id
        for (int i = 0; i < statTemplates.Length; i++)
        {
            StatDatabase.Add(statTemplates[i].ID, statTemplates[i]);
        }
    }

    void Awake()
    {
        BuildItemDatabase();
        BuildStatDatabase();
    }
}