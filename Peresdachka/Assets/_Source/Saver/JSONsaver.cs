using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONsaver : ISaver
{
    private string _path => Application.persistentDataPath + "/save.json";

    public void Save(GameData data)
    {
        Debug.Log(Application.persistentDataPath);
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(_path, json);
    }

    public GameData Load()
    {
        if (!File.Exists(_path)) return new GameData();

        string json = File.ReadAllText(_path);
        return JsonUtility.FromJson<GameData>(json);
    }

    public void ClearSave()
    {
        if (File.Exists(_path))
        {
            File.Delete(_path);
        }
    }
}
