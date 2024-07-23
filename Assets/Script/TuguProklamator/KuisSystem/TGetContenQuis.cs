using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
public class TGetContenQuis : MonoBehaviour
{
    private TProcessDataContent Content;

    public string[] TextTuguPetir;
    public string[] TextTuguPeringatan;
    public string[] TextKawasanTP;

    
    public void getAllMaterial(){
        Content = new TProcessDataContent();
        
        Content.GetMaterialData();

        string[] raw = Content.getRawDataMaterial;

        TextKawasanTP = Content.GetAllData(raw,"1-");
        
        TextTuguPetir = Content.GetAllData(raw,"2-");
        
        TextTuguPeringatan = Content.GetAllData(raw,"3-");
    }

    public string[] GetAllQuestionData(){
       string[] getAllRawQuestions = Content.GetQuestionData();
       return getAllRawQuestions;
    }



    public string[] HelperDivideString(string line, string pattern){
        string[] linesNew;
        List<string> result = new List<string>();

        string[] parts = Regex.Split(line, pattern);

        // Add the parts to the result list
        result.AddRange(parts);

        linesNew = result.ToArray();
        return linesNew;
    }



}
