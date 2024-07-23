using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class TProcessDataContent
{
    private TryGetDataTXT getData = new TryGetDataTXT();
    public string[] getRawData;
    public string[] getRawDataLB;
    public string[] getRawDataMaterial;

    public void GetCleanData(){
       getRawData = getData.getDataFromTxt("Content","Materi-TP");
    }

    public void GetCleanDataLB(){
       getRawDataLB = getData.getDataFromTxt("Content","Materi-LB");
    }
    public void GetMaterialData(){
       getRawDataMaterial = getData.getDataFromTxt("Content","Materi-TP-Split");
    }

    public string[] GetQuestionData(){
       string[] getRawDataQuestion = getData.getDataFromTxt("Content","Soal-TP");
       return getRawDataQuestion;
    }


    public string[] GetAllData(string[] raw, string prefix){
        string result;
        List<string> temporarySentence1 = new List<string>();

        if(raw.Length>1){
            foreach (string line in raw)
            {
            result = StartsWithPrefix(line,prefix); 
            if (!string.IsNullOrEmpty(result))
            {
                temporarySentence1.Add(result);
        
            }
            }
        }

        
         return temporarySentence1.ToArray();
    }

    // public void GetAllData(string[] raw, string prefix){

    //     List<string> temporarySentence1 = new List<string>();

    //     foreach (string line in raw)
    //     {
    //         // Debug.Log(line);
    //         // Debug.Log(prefix);
    //         string result = StartsWithPrefix(line,prefix);

    //         // Debug.Log(result); 

    //         if (!string.IsNullOrEmpty(result))
    //         {
    //             temporarySentence1.Add(result);

    //         }
    //         KawasanSumurMautMateri = temporarySentence1.ToArray();

    //     }
        
    // }

    public static string StartsWithPrefix(string input, string prefix)
    {
        string temporaryText = "";

        if(input.StartsWith(prefix)){
            temporaryText = input.Substring(2).Trim() + "\n";
            return temporaryText;
        }else{
            return "";
        }
    }

}
