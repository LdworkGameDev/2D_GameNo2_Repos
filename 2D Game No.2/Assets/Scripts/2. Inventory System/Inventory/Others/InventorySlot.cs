using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void SlotUpdate(InventorySlot slot);

[System.Serializable]
public class InventorySlot
{
    [NonSerialized] public InventoryUI parentUI;
    [NonSerialized] public GameObject slotObject;
    [NonSerialized] public SlotUpdate OnSlotBeforeUpdate;
    [NonSerialized] public SlotUpdate OnSlotAfterUpdate;

    public ItemType[] ItemAllowed;
    public EquipmentType[] equipmentAllowed;
    
    public ItemDefinition item;
    public int amount;

    public ItemDefinition_SO item_SO
    {
        get
        {
            if(item.itemID >= 0)
            {
                return parentUI.inventory_SO.itemDatabase_SO.GetItem_SO[item.itemID];
            }
            return null;
        }
    }

    public InventorySlot()
    {
        UpdateSlot(new ItemDefinition(), 0);
    }

    public InventorySlot(ItemDefinition item_, int amount_)
    {
        UpdateSlot(item_, amount_);
    }

    public void ClearSlot()
    {
        UpdateSlot(new ItemDefinition(), 0);
    }

    public void AddAmount(int amount_)
    {
        UpdateSlot(item, amount + amount_);
    }

    public void DescreaseAmount(int amount_)
    {
        if(amount - amount_ <= 0)
        {
            ClearSlot();
        }
        else
        {
            UpdateSlot(item, amount - amount_);
        }
    }

    public void UpdateSlot(ItemDefinition item_, int amount_)
    {
       
        if(OnSlotBeforeUpdate != null)
        {
            OnSlotBeforeUpdate.Invoke(this);
        }

        item = item_;
        amount = amount_;
        
        if (OnSlotAfterUpdate != null)
        {
            OnSlotAfterUpdate.Invoke(this);
        }
    }

    public bool CanReplaceItemInSlot(ItemDefinition_SO item_SO)
    {
        if (ItemAllowed.Length == 0 || item_SO == null || item_SO.itemID == -1) return true;

        if(item_SO.itemType == ItemType.Equipment)
        {
            foreach(EquipmentType equipType in equipmentAllowed)
            {
                if (equipType == ((EquipmentDefinition_SO)item_SO).equipmentType) return true;
            }
        }
        else
        {
            foreach (ItemType itemType in ItemAllowed)
            {
                if (itemType == item_SO.itemType) return true;
            }
        }
        return false;
    }
}
