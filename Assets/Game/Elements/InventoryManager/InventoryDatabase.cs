using System.Collections.Generic;
using UnityEngine;

/// <summary> Stores maps of ids to matching scriptable objects, and provides accessors for template and info retrieval. </summary>
[System.Serializable]
public class InventoryDatabase
{
    [SerializeField] SInventoryItem[] itemTemplates;
    [SerializeField] SInventoryItemStat[] statTemplates;
    
    readonly Dictionary<ItemIDs, SInventoryItem> itemDatabase = new();
    readonly Dictionary<ItemStatIDs, SInventoryItemStat> statDatabase = new();

    public string GetItemName(ItemIDs id) => itemDatabase[id].Name;

    public string GetStatName(ItemStatIDs id) => statDatabase[id].Name;

    public string GetItemDescription(ItemIDs id) => itemDatabase[id].Description;

    public string GetStatDescription(ItemStatIDs id) => statDatabase[id].Description;

    public SInventoryItem GetItemTemplate(ItemIDs id) => itemDatabase[id];

    public SInventoryItemStat GetItemStatTemplate(ItemStatIDs id) => statDatabase[id];

    public void Initialize()
    {
        BuildItemDatabase();
        BuildStatDatabase();
    }

    void BuildItemDatabase()
    {
        // Map all scriptable object item templates to their ids for simple retrieval
        for (int i = 0; i < itemTemplates.Length; i++)
        {
            itemDatabase.Add(itemTemplates[i].ID, itemTemplates[i]);
        }
    }

    void BuildStatDatabase()
    {
        // Map all scriptable object item stat templates to their types for simple retrieval by stat id
        for (int i = 0; i < statTemplates.Length; i++)
        {
            statDatabase.Add(statTemplates[i].ID, statTemplates[i]);
        }
    }
}