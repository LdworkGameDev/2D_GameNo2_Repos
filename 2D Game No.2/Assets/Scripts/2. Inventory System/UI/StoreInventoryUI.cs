using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class StoreInventoryUI : MonoBehaviour
{
    public StoreInventory_SO storeInventory_SO;
    public StoreItemDescriptionUI itemDescription;
    public CanvasGroup canvasGroup;
    public GameObject[] slotsObject;

    public Dictionary<GameObject, ItemDefinition_SO> slotsOnUI;

    private void Start()
    {
        CreateSlots();
        SetActive(false);
    }

    private void CreateSlots()
    {
        slotsOnUI = new Dictionary<GameObject, ItemDefinition_SO>();
        if (slotsObject.Length != storeInventory_SO.itemsInStore.Length) return;

        for (int i = 0; i < storeInventory_SO.itemsInStore.Length; i++)
        {
            slotsOnUI.Add(slotsObject[i], storeInventory_SO.itemsInStore[i]);
            slotsObject[i].transform.GetChild(0).GetComponent<Image>().sprite = storeInventory_SO.itemsInStore[i].sprite;
        }
    }

    public void OnPointerEnterSlot(GameObject slotObject)
    {
        ItemMouseControll.slotObjectBeingPointed = slotObject;
        MouseController.isPointerInButton = true;
    }

    public void OnPointerExitSlot(GameObject slotObject)
    {
        ItemMouseControll.slotObjectBeingPointed = null;
        MouseController.isPointerInButton = false;
    }

    public void OnPointerClicked(GameObject slotObject)
    {
        if (slotObject == null) return;
        MouseController.clickedItem_SO = slotsOnUI[slotObject];
        itemDescription.ShowItemDescription(slotsOnUI[slotObject]);
    }

    public void SetActive(bool active)
    {
        float alpha = active ? 1f : 0f;
        canvasGroup.alpha = alpha;
        canvasGroup.blocksRaycasts = active;
        canvasGroup.interactable = active;
    }
}
