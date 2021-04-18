using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Store Inventory", menuName = "Inventory System/Inventory/Store Inventory")]
public class StoreInventory_SO : ScriptableObject
{
    public ItemDefinition_SO[] itemsInStore;

    public ItemDefinition CreateItemDefinition(int index)
    {
        return new ItemDefinition(itemsInStore[index]);
    }
}
