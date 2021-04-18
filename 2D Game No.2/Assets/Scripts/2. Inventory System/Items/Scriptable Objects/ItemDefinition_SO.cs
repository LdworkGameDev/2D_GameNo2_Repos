using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType 
{
    Default,
    InscreasingStat,
    Equipment
}

public abstract class ItemDefinition_SO : ScriptableObject
{
    public ItemType itemType;

    public GameObject prefab;
    public Sprite sprite;
    public int itemID;
    public string itemName;

    [TextArea(10, 15)]
    public string itemDescription;
    
    [Space]
    public int price;

    [Space]
    public bool isStorable = false;
    public bool isStackable = false;
    public bool isDestroyedOnUse = false;
}

