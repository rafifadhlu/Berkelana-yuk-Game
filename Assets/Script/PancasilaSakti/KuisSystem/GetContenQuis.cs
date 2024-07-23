using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
public class GetContenQuis : MonoBehaviour
{
    private ProcessDataContent Content ;

    public string[] TextKawasandanSumurMaut;
    public string[] TextSerambi;
    public string[] TextPosdanDapur;
    public string[] TextSumurMaut;
    
    public void getAllMaterial(){
        Content = new ProcessDataContent();
        
        Content.GetMaterialData();

        string[] raw = Content.getRawDataMaterial;

        TextKawasandanSumurMaut = Content.GetAllData(raw,"1-");
        
        TextSerambi = Content.GetAllData(raw,"2-");
        
        TextPosdanDapur = Content.GetAllData(raw,"3-");
        
        TextSumurMaut = Content.GetAllData(raw,"4-");

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
