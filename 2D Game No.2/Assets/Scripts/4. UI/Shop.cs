using Cinemachine.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Vector2 offset;

    private void Update()
    {
        if (PlayerManager.canInteractWithUI && Input.GetButtonDown("Attack"))
        {
            PlayerManager.canReceivePlayerActionInput = false;
            PlayerManager.isInteractingWithUI = true;

            UIManager.Instance.storeUI.SetActive(true);
            UIManager.Instance.playerInventoryUI.SetActive(true);
        }

        if (PlayerManager.isInteractingWithUI == true && Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerManager.canReceivePlayerActionInput = true;
            PlayerManager.isInteractingWithUI = false;

            UIManager.Instance.storeUI.SetActive(false);
            UIManager.Instance.playerInventoryUI.SetActive(false);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            UIManager.Instance.interactiveCanvasUI.ShowUI(transform.position, offset, "Buy");
            PlayerManager.canReceiveAttackInput = false;
            PlayerManager.canInteractWithUI = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            UIManager.Instance.interactiveCanvasUI.SetActive(false);
            PlayerManager.canReceiveAttackInput = true;
            PlayerManager.canInteractWithUI = false;
        }
    }
}
