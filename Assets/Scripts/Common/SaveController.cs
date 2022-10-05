using System;
using UnityEngine;

public class SaveController
{
    private const string SAVE_KEY = "GAME_DATA";

    public GameData LoadData()
    {
        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            return JsonUtility.FromJson<GameData>(PlayerPrefs.GetString(SAVE_KEY));
        }

        return new GameData();
    }

    public void SaveData(GameData data)
    {
        PlayerPrefs.SetString(SAVE_KEY, JsonUtility.ToJson(data));
    }
}

[Serializable]
public class GameData
{
    public int Coins;
    public int Level;
}

// [Serializable]
// public class Position
// {
//     public int id;
//     public int x;
//     public int y;
// }