using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemDescriptionUI : MonoBehaviour
{
    public GameObject button_Buy;
    public TextMeshProUGUI text_ItemName;
    public TextMeshProUGUI text_ItemDescription;
    public Text text_Price;

    private GameObject player;
    private PlayerStatsController playerStatsController;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStatsController = player.GetComponent<PlayerStatsController>();
    }

    public void SetButtonActive(bool active)
    {
        button_Buy.SetActive(active);
    }

    public void ShowItemDescription(ItemDefinition_SO item_SO)
    {
        if (item_SO == null)
        {
            text_ItemName.text = "";
            text_ItemDescription.text = "";
            text_Price.text = "";
            button_Buy.SetActive(false);
        }
        else
        {
            text_ItemName.text = item_SO.itemName.ToString();
            text_ItemDescription.text = item_SO.itemDescription.ToString();
            text_Price.text = item_SO.price.ToString();
            button_Buy.SetActive(true);
        }
    }
    
    public void OnBuyButtonClicked()
    {
        var item_SO = MouseController.clickedItem_SO;
        var currentCoin = playerStatsController.GetCoinAttribute().currentValue;
        if (item_SO == null || currentCoin - item_SO.price < 0) return;

        playerStatsController.DescreaseCoin(item_SO.price);
        ItemDefinition newItem = new ItemDefinition(item_SO);
        playerStatsController.inventory_SO.AddItem(newItem, 1);
    }

    public void OnEnterButton()
    {
        MouseController.isPointerInButton = true;
    }

    public void OnExitButton()
    {
        MouseController.isPointerInButton = false;
    }
}
