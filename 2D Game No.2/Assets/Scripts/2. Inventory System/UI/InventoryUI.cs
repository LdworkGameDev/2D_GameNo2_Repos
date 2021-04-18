using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Inventory_SO inventory_SO;
    public PlayerItemDescriptionUI itemDescriptionUI;

    [SerializeField] private CanvasGroup canvasGroup;

    public GameObject[] slotsObject;

    public Dictionary<GameObject, InventorySlot> slotsOnUI;

    private void Start()
    {
        slotsOnUI = new Dictionary<GameObject, InventorySlot>();
        CreateSlots();
        inventory_SO.ClearInventory();

        SetActive(false);
    }

    private void CreateSlots()
    {
        slotsOnUI = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory_SO.inventory.slots.Length; i++)
        {
            inventory_SO.inventory.slots[i].parentUI = this;
            inventory_SO.inventory.slots[i].slotObject = slotsObject[i];
            inventory_SO.inventory.slots[i].OnSlotAfterUpdate += OnSlotUpdate;
            slotsOnUI.Add(slotsObject[i], inventory_SO.inventory.slots[i]);
        }
    }

    private void OnSlotUpdate(InventorySlot slot)
    {
        if (slot.item.itemID >= 0)
        {
            slot.slotObject.transform.GetChild(0).GetComponentInChildren<Image>().sprite = slot.item_SO.sprite;
            slot.slotObject.transform.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
        }
        else
        {
            slot.slotObject.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
            slot.slotObject.transform.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }

    public void OnMouseEnterSlot(GameObject slotObject)
    {
        ItemMouseControll.slotObjectBeingPointed = slotObject;
        MouseController.isPointerInButton = true;
    }

    public void OnMouseExitSlot(GameObject slotObject)
    {
        ItemMouseControll.slotObjectBeingPointed = null;
        MouseController.isPointerInButton = false;
    }

    public void OnMouseDragSlotBegin(GameObject slotObject)
    {
        if (slotsOnUI[slotObject].item.itemID < 0) return;

        var mouseObject = new GameObject();
        var rectTransform = mouseObject.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(100, 100);
        mouseObject.transform.SetParent(transform.parent);

        var image = mouseObject.AddComponent<Image>();
        image.sprite = slotsOnUI[slotObject].item_SO.sprite;
        image.raycastTarget = false;

        ItemMouseControll.slotObjectBeingDragged = mouseObject;

        MouseController.clickedSlot = slotsOnUI[slotObject];
        UpdateItemDescriptionUIWhenClickToSlot(slotObject);
    }

    public void OnMouseDragSlotEnd(GameObject slotObject)
    {
        Destroy(ItemMouseControll.slotObjectBeingDragged);
        
        if(ItemMouseControll.currentInventoryUI == null)
        {
            slotsOnUI[slotObject].ClearSlot();
        }
        else if(ItemMouseControll.slotObjectBeingPointed != null)
        {
            InventorySlot pointedSlot = ItemMouseControll.currentInventoryUI.slotsOnUI[ItemMouseControll.slotObjectBeingPointed];
            inventory_SO.SwapSlot(slotsOnUI[slotObject], pointedSlot);
        }

        MouseController.clickedSlot = null;
        UpdateItemDescriptionUIWhenClickToSlot(null);
    }

    public void OnMouseDragSlot(GameObject slotObject)
    {
        if (ItemMouseControll.slotObjectBeingDragged != null)
        {
            ItemMouseControll.slotObjectBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }

    public void OnMouseEnterInventoryUI(GameObject UIObject)
    {
        ItemMouseControll.currentInventoryUI = UIObject.GetComponent<InventoryUI>();
    }

    public void OnMouseExitInventoryUI(GameObject UIObject)
    {
        ItemMouseControll.currentInventoryUI = null;
    }

    public void OnPointerClicked(GameObject slotObject)
    {
        UpdateItemDescriptionUIWhenClickToSlot(slotObject);
    }

    public void UpdateItemDescriptionUIWhenClickToSlot(GameObject slotObject)
    {
        if (slotObject == null || slotsOnUI[slotObject].item.itemID == -1)
        {
            itemDescriptionUI.text_Name.text = "";
            itemDescriptionUI.text_Description.text = "";

            UIManager.Instance.playerInventoryUI.itemDescriptionUI.SetActiveButton(false);
        }
        else
        {
            MouseController.clickedSlot = slotsOnUI[slotObject];

            ItemDefinition_SO item_SO = slotsOnUI[slotObject].item_SO;
            itemDescriptionUI.text_Name.text = item_SO.itemName.ToString();
            itemDescriptionUI.text_Description.text = item_SO.itemDescription.ToString();

            itemDescriptionUI.SetActiveButton(true);
            itemDescriptionUI.SetButtonText(slotsOnUI[slotObject].item_SO.itemType, slotsOnUI[slotObject].parentUI.inventory_SO.inventoryType);
        }
    }

    public void SetActive(bool active)
    {
        float alpha = active ? 1f : 0f;
        canvasGroup.alpha = alpha;
        canvasGroup.blocksRaycasts = active;
        canvasGroup.interactable = active;
    }
}
