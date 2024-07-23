using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MProcessDataFromTxt 
{
     private string[] rawData;
     public string[] dialog1;
     public string[] dialog2;
     public string[] dialog3;

     public string nameCharacterDialog1;
     public string nameCharacterDialog2;

     private TryGetDataTXT getData = new TryGetDataTXT();
     private List<string> temporarySentence1 = new List<string>();
     private List<string> temporarySentence2 = new List<string>();
     private List<string> temporarySentence3 = new List<string>();

    // Start is called before the first frame update
    public void ProcessData()
    {
        rawData = getData.getDataFromTxt("Content","D-Npc-Sifa");
        string temporaryText = "";

        foreach (string line in rawData)
        {

            if (line.StartsWith("1+"))
            {
                temporaryText = line.Substring(2).Trim() + "\n"; // Remove "1+" and trim any leading spaces
                nameCharacterDialog1 = temporaryText;
                temporaryText = "";
            }
            else if (line.StartsWith("2+"))
            {
                temporaryText = line.Substring(2).Trim() + "\n"; // Remove "2+" and trim any leading spaces
                nameCharacterDialog2 = temporaryText;
                temporaryText = "";
            }
            else if (line.StartsWith("1-"))
            {
                temporaryText = line.Substring(2).Trim() + "\n"; // Remove "1-" and trim any leading spaces
                temporarySentence1.Add(temporaryText);
                temporaryText = "";
            }
            else if (line.StartsWith("2-"))
            {
                temporaryText += line.Substring(2).Trim() + "\n"; // Remove "2-" and trim any leading spaces
                temporarySentence2.Add(temporaryText);
                temporaryText = "";
            }
            else if (line.StartsWith("3-"))
            {
                temporaryText += line.Substring(2).Trim() + "\n"; // Remove "3-" and trim any leading spaces
                temporarySentence3.Add(temporaryText);
                temporaryText = "";
            }
            
        }
        dialog1 = temporarySentence1.ToArray();
        dialog2 = temporarySentence2.ToArray();
        dialog3 = temporarySentence3.ToArray();

    }

}
