using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDefinition
{
    public int itemID = -1;
    public ItemAttribute[] attributes;

    public ItemDefinition()
    {
        itemID = -1;
    }

    public ItemDefinition(ItemDefinition_SO item_SO)
    {
        itemID = item_SO.itemID;

        if(item_SO.itemType == ItemType.Equipment)
        {
            EquipmentDefinition_SO equipment_SO = (EquipmentDefinition_SO)item_SO;

            attributes = new ItemAttribute[equipment_SO.attributes.Length];
            for (int i = 0; i < equipment_SO.attributes.Length; i++)
            {
                attributes[i] = new ItemAttribute(equipment_SO.attributes[i].attributeType, equipment_SO.attributes[i].minValue, equipment_SO.attributes[i].maxValue);
            }
        }
    }
}
