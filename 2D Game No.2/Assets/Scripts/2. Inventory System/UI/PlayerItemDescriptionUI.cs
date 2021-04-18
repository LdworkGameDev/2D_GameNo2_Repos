using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItemDescriptionUI : MonoBehaviour
{
    public PlayerStatsController playerStatsController;
    public TextMeshProUGUI text_Name;
    public TextMeshProUGUI text_Description;
    public GameObject button_Use;
    public GameObject button_Discard;

    public void SetActiveButton(bool active)
    {
        button_Use.SetActive(active);
        button_Discard.SetActive(active);
    }

    public void SetButtonText(ItemType itemType, InventoryType inventoryType)
    {
        if(itemType == ItemType.Equipment)
        {
            if(inventoryType == InventoryType.Equipment)
            {
                button_Use.GetComponentInChildren<Text>().text = "Unequip";
            }
            else
            {
                button_Use.GetComponentInChildren<Text>().text = "Equip";
            }
        }
        else
        {
            button_Use.GetComponentInChildren<Text>().text = "Use";
        }
    }

    public void OnButtonUseClicked()
    {
        if (MouseController.clickedSlot == null) return;
        if (MouseController.clickedSlot.parentUI.inventory_SO.inventoryType == InventoryType.Inventory)
        {
            if(MouseController.clickedSlot.item_SO.itemType == ItemType.InscreasingStat)
            {
                playerStatsController.UseStoredItem(MouseController.clickedSlot);
            }
            else if (MouseController.clickedSlot.item_SO.itemType == ItemType.Equipment)
            {
                playerStatsController.EquipItem(MouseController.clickedSlot);
                MouseController.clickedSlot = null;
                ResetItemDefinition();
            }
        }
        else if(MouseController.clickedSlot.parentUI.inventory_SO.inventoryType == InventoryType.Equipment)
        {
            if (playerStatsController.UnequipItem(MouseController.clickedSlot))
            {
                MouseController.clickedSlot = null;
                ResetItemDefinition();
            }
        }
    }

    public void OnButtonDiscardClicked()
    {
        if (MouseController.clickedSlot == null) return;
        MouseController.clickedSlot.ClearSlot();
    }

    public void ResetItemDefinition()
    {
        if(MouseController.clickedSlot == null)
        {
            text_Name.text = "";
            text_Description.text = "";
            SetActiveButton(false);
        }
    }

    public void OnPointerEnterButton()
    {
        MouseController.isPointerInButton = true;
    }

    public void OnPointerExitButton()
    {
        MouseController.isPointerInButton = false;
    }
}


