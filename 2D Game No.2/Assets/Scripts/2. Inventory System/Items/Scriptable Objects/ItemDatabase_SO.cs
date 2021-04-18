using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Database_SO", menuName = "Inventory System/Item/Database", order = -1)]
public class ItemDatabase_SO : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemDefinition_SO[] items_SO;
    public Dictionary<int, ItemDefinition_SO> GetItem_SO;

    public void OnAfterDeserialize()
    {
        GetItem_SO = new Dictionary<int, ItemDefinition_SO>();
        for(int i = 0; i < items_SO.Length; i++)
        {
            items_SO[i].itemID = i;
            GetItem_SO.Add(i, items_SO[i]);
        }
    }

    public void OnBeforeSerialize()
    {

    }
}
