using System.Collections.Generic;
using UnityEngine;

/// <summary> Stores a map of ItemIDs to matching scriptable objects for item template retrieval. </summary>
public class InventoryItemDatabase : MonoBehaviour
{
    [SerializeField] SInventoryItem[] itemTemplates;
    
    Dictionary<ItemIDs, SInventoryItem> itemDatabase = new();

    void BuildDatabase()
    {
        // Map all scriptable object item templates to their types for simple retrieval by item id
        for (int i = 0; i < itemTemplates.Length; i++)
        {
            itemDatabase.Add(itemTemplates[i].Type, itemTemplates[i]);
        }
    }

    void Awake()
    {
        BuildDatabase();
    }
}