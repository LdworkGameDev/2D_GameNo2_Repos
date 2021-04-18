using UnityEngine;


[System.Serializable]
public class PlayerSaveData 
{
    public Inventory playerInventory;
    public Inventory equipmentsInventory;
    public DynamicAttributeContainer dynamicAttributeContainer;
    public float[] lastCheckPoint;

    public PlayerSaveData(PlayerStatsController playerStatsController)
    {
        playerInventory = playerStatsController.inventory_SO.inventory;
        equipmentsInventory = playerStatsController.equipments_SO.inventory;
        dynamicAttributeContainer = playerStatsController.dynamicContainer;

        lastCheckPoint = new float[3];
        lastCheckPoint[0] = SaveManager.Instance.lastCheckPointPosition.x;
        lastCheckPoint[1] = SaveManager.Instance.lastCheckPointPosition.y;
        lastCheckPoint[2] = SaveManager.Instance.lastCheckPointPosition.z;
    }
}
