using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment _SO", menuName = "Inventory System/Item/Items/Equipment")]
public class EquipmentDefinition_SO : ItemDefinition_SO
{
    public EquipmentType equipmentType;
    public ItemAttribute[] attributes;

    private void Awake()
    {
        itemType = ItemType.Equipment;
    }
}

public enum StaticAttributeType
{
    None,
    Health,
    Mana,
    Attack,
    Defence,
    Strength
}

public enum EquipmentType
{
    None,
    Weapon,
    Helmet,
    ChestArmor, 
    Boots,
    Shield,
    Accessory
}


