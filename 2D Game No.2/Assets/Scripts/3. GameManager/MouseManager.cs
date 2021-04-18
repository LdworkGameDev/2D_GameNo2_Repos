using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : Manager<MouseManager>
{
    public bool mouseInInventoryUI;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (MouseController.isPointerInButton == false)
            {
                UIManager.Instance.playerInventoryUI.itemDescriptionUI.text_Name.text = "";
                UIManager.Instance.playerInventoryUI.itemDescriptionUI.text_Description.text = "";
                UIManager.Instance.playerInventoryUI.itemDescriptionUI.SetActiveButton(false);
                UIManager.Instance.storeUI.itemDescription.ShowItemDescription(null);

                MouseController.clickedSlot = null;
            }
        }
    }
}

public static class MouseController
{
    public static bool isPointerInButton;
    public static InventorySlot clickedSlot;
    public static ItemDefinition_SO clickedItem_SO;
}
