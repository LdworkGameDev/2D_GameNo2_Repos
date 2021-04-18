using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Manager<UIManager>
{
    public InventoryUI playerInventoryUI;
    public InventoryUI playerEquipmentUI;

    public PlayerStaticStatsUI playerStaticStatsUI;
    public PlayerDynamicStatsUI playerDynamicStatsUI;

    public StoreInventoryUI storeUI;

    public InteractiveCanvasUI interactiveCanvasUI;

    private bool isInventoryActive = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isInventoryActive = !isInventoryActive;
            playerInventoryUI.SetActive(isInventoryActive);
            playerEquipmentUI.SetActive(isInventoryActive);
            playerStaticStatsUI.SetActive(isInventoryActive);
        }
    }
}
