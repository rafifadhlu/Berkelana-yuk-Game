using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour
{
    public QuestsList _QuestData;
    private void Awake(){
        _QuestData = new QuestsList();
    }

    void Start(){
        Load();
    }

    public void Save(){
        Debug.Log("Saving");
        string json = JsonUtility.ToJson(_QuestData, true);
        File.WriteAllText(Application.persistentDataPath + "/DataGame.json", json);
    }

    public void Load(){
        string path = Application.persistentDataPath + "/DataGame.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            _QuestData = JsonUtility.FromJson<QuestsList>(json);
            Debug.Log("Loaded data: " + json);
        }
        else
        {
            Debug.LogWarning("Save file not found");
        }
    }
}
