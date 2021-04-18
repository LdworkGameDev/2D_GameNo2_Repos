using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStatsController : MonoBehaviour
{
    public Inventory_SO inventory_SO;
    public Inventory_SO equipments_SO;
    public PlayerBaseStats_SO playerBaseStats_SO;

    public PlayerStaticAttribute[] staticAttrbutes;
    [Space]
    public DynamicAttributeContainer dynamicContainer;

    [Space]
    public UnityEvent OnPlayerAttackedEvent;
    public UnityEvent OnPlayerDeathEvent;

    private void Start()
    {
        for (int i = 0; i < staticAttrbutes.Length; i++)
        {
            int baseValue = 0;
            foreach (BaseStats baseStat in playerBaseStats_SO.baseStats)
            {
                if(baseStat.staticType == staticAttrbutes[i].staticType)
                {
                    baseValue = baseStat.baseValue;
                }
            }
            staticAttrbutes[i].CreateAttribute(this, baseValue);
        }

        foreach(InventorySlot slot in equipments_SO.inventory.slots)
        {
            slot.OnSlotBeforeUpdate += HandleOnSlotBeforeUpdate;
            slot.OnSlotAfterUpdate += HandleOnSlotAfterUpdate;
        }

        SetDynamicAttributesMaxValue();
        ResetDynamicAttributeCurrentValue();
        UIManager.Instance.playerStaticStatsUI.UpdateStatsUI(staticAttrbutes);
        UIManager.Instance.playerDynamicStatsUI.UpdateDynamicAttributesUI(dynamicContainer);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SavePlayerData();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            LoadPlayerData();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemController itemController = collision.gameObject.GetComponent<ItemController>();
        if (itemController != null)
        {
            if (itemController.itemDefinition_SO.isStorable)
            {
                ItemDefinition item = new ItemDefinition(itemController.itemDefinition_SO);
                if (inventory_SO.AddItem(item, 1))
                {
                    Destroy(collision.gameObject.transform.parent.gameObject);
                }
            }
            else if (itemController.itemDefinition_SO.isDestroyedOnUse)
            {
                UseNotStoredItem((InscreasingStatItemDefinition_SO)itemController.itemDefinition_SO);
                Destroy(collision.gameObject.transform.parent.gameObject);
            }
        }
    }

    public void HandleOnSlotBeforeUpdate(InventorySlot slot_)
    {
        if (slot_.item_SO == null) return;
        switch (slot_.parentUI.inventory_SO.inventoryType)
        {
            case InventoryType.Inventory:
                break;
            case InventoryType.Equipment:
                if (slot_.item_SO.itemType != ItemType.Equipment) break;
                EquipmentDefinition_SO equipDefinition_SO = (EquipmentDefinition_SO)slot_.item_SO;
                if (slot_.item_SO.itemType != ItemType.Equipment) break;
                for (int i = 0; i < slot_.item.attributes.Length; i++)
                {
                    for (int j = 0; j < staticAttrbutes.Length; j++)
                    {
                        if (staticAttrbutes[j].staticType == slot_.item.attributes[i].attributeType)
                        {
                            staticAttrbutes[j].value.RemoveModifier(slot_.item.attributes[i]);

                            SetDynamicAttributesMaxValue();
                            UIManager.Instance.playerStaticStatsUI.UpdateStatsUI(staticAttrbutes);
                        }
                    }
                }
                break;
            default:
                break;
        }
    }

    public void HandleOnSlotAfterUpdate(InventorySlot slot_)
    {
        if (slot_.item_SO == null) return;
       
        switch (slot_.parentUI.inventory_SO.inventoryType)
        {
            case InventoryType.Inventory:
                break;
            case InventoryType.Equipment:
                if (slot_.item_SO.itemType != ItemType.Equipment) break;
                for (int i = 0; i < slot_.item.attributes.Length; i++)
                {
                    for (int j = 0; j < staticAttrbutes.Length; j++)
                    {
                        if(staticAttrbutes[j].staticType == slot_.item.attributes[i].attributeType)
                        {
                            staticAttrbutes[j].value.AddModifier(slot_.item.attributes[i]);

                            SetDynamicAttributesMaxValue();
                            UIManager.Instance.playerStaticStatsUI.UpdateStatsUI(staticAttrbutes);
                        }
                    }
                }
                break;
            default:
                break;
        }
    }




    #region Dynamic Attributes Functions

    public void UseNotStoredItem(InscreasingStatItemDefinition_SO item_SO)
    {
        if (UseItem(item_SO))
        {
            UIManager.Instance.playerDynamicStatsUI.UpdateDynamicAttributesUI(dynamicContainer);
        }
    }

    public void UseStoredItem(InventorySlot slot)
    {
        if (slot.item.itemID == -1) return;
        if (UseItem((InscreasingStatItemDefinition_SO)slot.item_SO))
        {
            Debug.Log("Has used item");
            slot.DescreaseAmount(1);
            UIManager.Instance.playerDynamicStatsUI.UpdateDynamicAttributesUI(dynamicContainer);
        }
    }

    public void SetDynamicAttributesMaxValue()
    {
        foreach (var dynamicAttribute in dynamicContainer.dynamicAttributes)
        {
            foreach(var staticAttribute in staticAttrbutes)
            {
                if(dynamicAttribute.refToStaticType == staticAttribute.staticType)
                {
                    dynamicAttribute.maxValue = staticAttribute.value.ModifiedValue;

                    dynamicAttribute.currentValue = (dynamicAttribute.currentValue > dynamicAttribute.maxValue) ? dynamicAttribute.maxValue : dynamicAttribute.currentValue;
                }
            }
        }
        UIManager.Instance.playerDynamicStatsUI.UpdateDynamicAttributesUI(dynamicContainer);
    }

    public void ResetDynamicAttributeCurrentValue()
    {
        foreach(var dynamicAttribute in dynamicContainer.dynamicAttributes)
        {
            dynamicAttribute.currentValue = dynamicAttribute.maxValue;
        }
        UIManager.Instance.playerDynamicStatsUI.UpdateDynamicAttributesUI(dynamicContainer);
    }

    public bool UseItem(InscreasingStatItemDefinition_SO item_SO)
    {
        bool hasUsed = false;
        foreach (PlayerDynamicAttribute attribute in dynamicContainer.dynamicAttributes)
        {
            if (item_SO.dynamicType == attribute.dynamicType)
            {
                if (attribute.dynamicType == DynamicAttributeType.Coin)
                {
                    attribute.currentValue += item_SO.inscreasingAmount;
                    hasUsed = true;
                }
                else if(attribute.currentValue != attribute.maxValue)
                {
                    attribute.currentValue = (item_SO.inscreasingAmount + attribute.currentValue > attribute.maxValue)
                        ? attribute.maxValue : item_SO.inscreasingAmount + attribute.currentValue;
                    hasUsed = true;
                }
            }
        }
        return hasUsed;
    }

    public PlayerDynamicAttribute GetHealthAttribute()
    {
        foreach(PlayerDynamicAttribute attribute in dynamicContainer.dynamicAttributes)
        {
            if(attribute.dynamicType == DynamicAttributeType.Health)
            {
                return attribute;
            }
        }
        return null;
    }

    public PlayerDynamicAttribute GetManaAttribute()
    {
        foreach (PlayerDynamicAttribute attribute in dynamicContainer.dynamicAttributes)
        {
            if (attribute.dynamicType == DynamicAttributeType.Mana)
            {
                return attribute;
            }
        }
        return null;
    }

    public PlayerDynamicAttribute GetCoinAttribute()
    {
        foreach (PlayerDynamicAttribute attribute in dynamicContainer.dynamicAttributes)
        {
            if (attribute.dynamicType == DynamicAttributeType.Coin)
            {
                return attribute;
            }
        }
        return null;
    }

    public void DescreaseHealth(int amount)
    {
        var healthAttribute = GetHealthAttribute();
        healthAttribute.currentValue = (healthAttribute.currentValue - amount >= 0) ? (healthAttribute.currentValue - amount) : 0;
        if(OnPlayerAttackedEvent != null)
        {
            OnPlayerAttackedEvent.Invoke();
        }

        if(healthAttribute.currentValue <= 0 && OnPlayerDeathEvent != null)
        {
            OnPlayerDeathEvent.Invoke();
        }

        UIManager.Instance.playerDynamicStatsUI.UpdateDynamicAttributesUI(dynamicContainer);
    }

    public void DescreaseMana(int amount)
    {
        var manaAttribute = GetManaAttribute();
        manaAttribute.currentValue = (manaAttribute.currentValue - amount >= 0) ? (manaAttribute.currentValue - amount) : 0;
        if (OnPlayerAttackedEvent != null)
        {
            OnPlayerAttackedEvent.Invoke();
        }

        if (manaAttribute.currentValue <= 0 && OnPlayerDeathEvent != null)
        {
            OnPlayerDeathEvent.Invoke();
        }

        UIManager.Instance.playerDynamicStatsUI.UpdateDynamicAttributesUI(dynamicContainer);
    }

    public void DescreaseCoin(int amount)
    {
        var coinAttribute = GetCoinAttribute();
        coinAttribute.currentValue = (coinAttribute.currentValue - amount >= 0) ? (coinAttribute.currentValue - amount) : 0;
        if (OnPlayerAttackedEvent != null)
        {
            OnPlayerAttackedEvent.Invoke();
        }

        if (coinAttribute.currentValue <= 0 && OnPlayerDeathEvent != null)
        {
            OnPlayerDeathEvent.Invoke();
        }

        UIManager.Instance.playerDynamicStatsUI.UpdateDynamicAttributesUI(dynamicContainer);
    }

    #endregion




    #region Static Attribute Function

    public void EquipItem(InventorySlot slot)
    {
        EquipmentDefinition_SO item_SO = (EquipmentDefinition_SO)slot.item_SO;
        foreach(var equipmentSlot in equipments_SO.inventory.slots)
        {
            foreach(var allowed in equipmentSlot.equipmentAllowed)
            {
                if(allowed == item_SO.equipmentType)
                {
                    equipments_SO.SwapSlot(slot, equipmentSlot);
                }
            }
        }
    }

    public bool UnequipItem(InventorySlot slot)
    {
        bool hasUnequipped = false;
        if(inventory_SO.AddItem(slot.item, slot.amount))
        {
            slot.ClearSlot();
            hasUnequipped = true;
        }
        return hasUnequipped;
    }

    public int GetAttackStat()
    {
        foreach(PlayerStaticAttribute attribute in staticAttrbutes)
        {
            if(attribute.staticType == StaticAttributeType.Attack)
            {
                return attribute.value.ModifiedValue;
            }
        }
        return 0;
    }

    public int GetDefenceStat()
    {
        foreach (PlayerStaticAttribute attribute in staticAttrbutes)
        {
            if (attribute.staticType == StaticAttributeType.Defence)
            {
                return attribute.value.ModifiedValue;
            }
        }
        return 0;
    }

    public int GetStrengthStat()
    {
        foreach (PlayerStaticAttribute attribute in staticAttrbutes)
        {
            if (attribute.staticType == StaticAttributeType.Strength)
            {
                return attribute.value.ModifiedValue;
            }
        }
        return 0;
    }

    #endregion




    #region Save, Load and Quit

    public void SavePlayerData()
    {
        SaveManager.Instance.SavePlayerData(this);
    }

    public void LoadPlayerData()
    {
        PlayerSaveData playerSaveData = SaveManager.Instance.LoadPlayerData();
        for (int i = 0; i < inventory_SO.inventory.slots.Length; i++)
        {
            inventory_SO.inventory.slots[i].UpdateSlot(playerSaveData.playerInventory.slots[i].item, playerSaveData.playerInventory.slots[i].amount);
        }
        for (int i = 0; i < equipments_SO.inventory.slots.Length; i++)
        {
            equipments_SO.inventory.slots[i].UpdateSlot(playerSaveData.equipmentsInventory.slots[i].item, playerSaveData.equipmentsInventory.slots[i].amount);
        }
        for (int i = 0; i < dynamicContainer.dynamicAttributes.Length; i++)
        {
            dynamicContainer.dynamicAttributes[i].UpdateAttribute(playerSaveData.dynamicAttributeContainer.dynamicAttributes[i]);
        }

        UIManager.Instance.playerDynamicStatsUI.UpdateDynamicAttributesUI(dynamicContainer);
        LoadLastCheckPoint();
    }

    public void LoadLastCheckPoint()
    {
        transform.position = SaveManager.Instance.lastCheckPointPosition;
    }


    private void OnApplicationQuit()
    {
        inventory_SO.ClearInventory();
        equipments_SO.ClearInventory();
    }

    #endregion
}
