using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
public class MGetContenQuis : MonoBehaviour
{
    private MProcessDataContent Content = new MProcessDataContent();

    public string[] TextSejarahBangunan;
    public string[] TextIsiMuseum;

    
    public void getAllMaterial(){
        Content.GetMaterialData();

        string[] raw = Content.getRawDataMaterial;

        TextSejarahBangunan = Content.GetAllData(raw,"1-");
        
        TextIsiMuseum = Content.GetAllData(raw,"2-");
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
