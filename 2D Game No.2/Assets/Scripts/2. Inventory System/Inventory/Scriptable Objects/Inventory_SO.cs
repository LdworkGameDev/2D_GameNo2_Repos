using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public enum InventoryType
{
    Inventory,
    Equipment
}

[CreateAssetMenu(fileName = "New Inventory_SO", menuName = "Inventory System/Inventory/Player Inventory")]
public class Inventory_SO : ScriptableObject
{
    public InventoryType inventoryType;
    public ItemDatabase_SO itemDatabase_SO;
    public Inventory inventory;

    public bool AddItem(ItemDefinition item, int amount)
    {
        if (CountEmptySlots() == 0) return false;

        InventorySlot slot = FindSlotWithSpecificItem(item);
        if (slot == null || !slot.item_SO.isStackable)
        {
            SetItemForEmptySlot(item, amount);
            return true;
        }
        slot.AddAmount(amount);

        return true;
    }

    private InventorySlot FindSlotWithSpecificItem(ItemDefinition item)
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (item.itemID == inventory.slots[i].item.itemID)
            {
                return inventory.slots[i];
            }
        }
        return null;
    }

    public int CountEmptySlots()
    {
        int counter = 0;
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.slots[i].item.itemID <= -1)
            {
                counter++;
            }
        }
        return counter;
    }

    public void SetItemForEmptySlot(ItemDefinition item, int amount)
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.slots[i].item.itemID == -1)
            {
                inventory.slots[i].UpdateSlot(item, amount);
                break;
            }
        }
    }

    public void SwapSlot(InventorySlot slot1, InventorySlot slot2)
    {
        if (slot2.CanReplaceItemInSlot(slot1.item_SO) && slot1.CanReplaceItemInSlot(slot2.item_SO))
        {
            InventorySlot tempSlot = new InventorySlot(slot2.item, slot2.amount);
            slot2.UpdateSlot(slot1.item, slot1.amount);
            slot1.UpdateSlot(tempSlot.item, tempSlot.amount);
        }
    }

    public void ClearInventory()
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            inventory.slots[i].ClearSlot();
        }
    }
}
