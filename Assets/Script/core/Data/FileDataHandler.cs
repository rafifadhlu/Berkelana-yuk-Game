using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileDataHandler{
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName){
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    
    public GameData Load(){
        string fullPath = Path.Combine(dataDirPath,dataFileName);
        // GameData loadedData = null;

        if (File.Exists(fullPath)) {
            string json = File.ReadAllText(fullPath);
            GameData changeFile = JsonUtility.FromJson<GameData>(json);
            Debug.Log("Loaded JSON: " + json);
            
            return changeFile;
        }
        Debug.LogError("Save file not found in " + fullPath);
        return null;
    }

    public void Save(GameData data){
        string fullPath = Path.Combine(dataDirPath,dataFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string dataToStore = JsonUtility.ToJson(data,true);

            using( FileStream stream = new FileStream(fullPath,FileMode.Create)){
                using(StreamWriter writer = new StreamWriter(stream)){
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log("Error : " + fullPath +"\n"+e);
            throw;
        }
    }

}
