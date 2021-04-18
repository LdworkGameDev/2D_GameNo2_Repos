using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Inscreasing Item_SO", menuName = "Inventory System/Item/Items/Inscreasing Stat Item")]
public class InscreasingStatItemDefinition_SO : ItemDefinition_SO
{
    public DynamicAttributeType dynamicType;
    public int inscreasingAmount;

    private void Awake()
    {
        itemType = ItemType.InscreasingStat;
    }
}

public enum DynamicAttributeType
{
    None, 
    Health,
    Mana, 
    Coin
}
