using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.Networking;
using System;

public class TryGetDataTXT 
{
    // the structure of data dialogue is 
    // String name and Array of group sentence
    // Start is called before the first frame update

    //    List<string> temporarySentence = new List<string>();

    //     string nameCharacterDialog1 = "";
    //     string nameCharacterDialog2 = "";

    //     string[] sentencesDialog1 = {};
    //     string[] sentencesDialog2 = {};

    public string[] getDataFromTxt(string folder, string nameFile){
        
        // string readFromTxtFile = Application.dataPath + $"/{folder}/" + $"{nameFile}" + ".txt";
        // string[] sentences = File.ReadAllLines(readFromTxtFile).ToArray();
        
        
        // return sentences;

        string[] sentences = null;
        string readFromTxtFile = Path.Combine(Application.streamingAssetsPath, folder, $"{nameFile}.txt");

        if (Application.platform == RuntimePlatform.Android)
        {
            UnityWebRequest request = UnityWebRequest.Get(readFromTxtFile);
            request.SendWebRequest();

            while (!request.isDone)
            {
                // Wait for the request to complete
            }

            if (request.result == UnityWebRequest.Result.Success)
            {
                sentences = request.downloadHandler.text.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                Debug.LogError("Failed to load file: " + request.error);
            }
        }
        else
        {
            if (File.Exists(readFromTxtFile))
            {
                sentences = File.ReadAllLines(readFromTxtFile);
            }
            else
            {
                Debug.LogError("File not found: " + readFromTxtFile);
            }
        }

        return sentences;

    }
    
    // void Start()
    // {

    //     string readFromTxtFile = Application.dataPath + "/Content/" + "D-Npc-Ebi.txt";
    //     string[] sentences = File.ReadAllLines(readFromTxtFile).ToArray();
    //     string temporaryText = "";

    //     foreach (string line in sentences)
    //     {
    //         if (line.StartsWith("1+"))
    //         {
    //             temporaryText = line.Substring(2).Trim() + "\n"; // Remove "1+" and trim any leading spaces
    //             nameCharacterDialog1 = temporaryText;
    //             temporaryText = "";
    //         }
    //         else if (line.StartsWith("2+"))
    //         {
    //             temporaryText = line.Substring(2).Trim() + "\n"; // Remove "2+" and trim any leading spaces
    //             nameCharacterDialog2 = temporaryText;
    //             temporaryText = "";
    //         }
    //         else if (line.StartsWith("1-"))
    //         {
    //             temporaryText = line.Substring(2).Trim() + "\n"; // Remove "1-" and trim any leading spaces
    //             temporarySentence.Add(temporaryText);
    //             temporaryText = "";
    //             sentencesDialog1 = temporarySentence.ToArray();
    //         }
    //         else if (line.StartsWith("2-"))
    //         {
    //             temporaryText += line.Substring(2).Trim() + "\n"; // Remove "2-" and trim any leading spaces
                
    //             temporarySentence.Add(temporaryText);
    //             temporaryText = "";
    //             sentencesDialog2 = temporarySentence.ToArray();
    //         }
            
    //     }

    //     foreach(string line in sentences){
    //         Debug.Log(line);
    //     }

    //     // foreach(string line in fileLines){
    //     //     string[] splitLine = line.Split(delimeterChar);
    //     //     foreach(string sentence in splitLine){
    //     //         Debug.Log(sentence);
    //     //     }
    //     // }
        
    // }


    
// Assets/Content/D-Npc-Ebi.txt
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
