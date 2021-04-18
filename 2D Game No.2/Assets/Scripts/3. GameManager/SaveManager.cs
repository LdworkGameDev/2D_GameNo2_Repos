using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;

public class SaveManager : Manager<SaveManager>
{
    public string playerDataSavePath;
    public Vector3 lastCheckPointPosition;

    public void SavePlayerData(PlayerStatsController statsController)
    {
        PlayerSaveData playerData = new PlayerSaveData(statsController);
        BinaryFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, playerDataSavePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, playerData);
        stream.Close();
    }

    public PlayerSaveData LoadPlayerData()
    {
        if (!File.Exists(string.Concat(Application.persistentDataPath, playerDataSavePath))) return null;
        BinaryFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, playerDataSavePath), FileMode.Open, FileAccess.Read);
        PlayerSaveData playerData = formatter.Deserialize(stream) as PlayerSaveData;
        stream.Close();

        lastCheckPointPosition.x = playerData.lastCheckPoint[0];
        lastCheckPointPosition.y = playerData.lastCheckPoint[1];
        lastCheckPointPosition.z = playerData.lastCheckPoint[2];
        return playerData;
    }

}
