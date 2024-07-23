using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    // Start is called before the first frame update
    public string name;
    [TextArea(3,20)]
    public string[] sentences;
    
}


        // //particle Effect First mission "Kawasan Sumur Maut"
        // if(getQuest.questList[0].isActive){
        //     particleEffect[0].SetActive(true);
        // }else if(getQuest.questList[0].isCompleted){
        //     particleEffect[0].SetActive(false);
        // }else{
        //     particleEffect[0].SetActive(false);
        // }
        
        // //particle Effect Second mission "Serambi Penyiksaan"
        // if(getQuest.questList[1].isActive && getQuest.questList[0].isCompleted ){
        //     particleEffect[1].SetActive(true);
        // }else if(getQuest.questList[1].isCompleted){
        //     particleEffect[1].SetActive(false);
        // }else{
        //     particleEffect[1].SetActive(false);
        // }

        // //particle Effect Third mission "PosDanDapur"
        // if(getQuest.questList[2].isActive && getQuest.questList[1].isCompleted ){
        //     particleEffect[2].SetActive(true);
        // }else if(getQuest.questList[2].isCompleted){
        //     particleEffect[2].SetActive(false);
        // }else{
        //     particleEffect[2].SetActive(false);
        // }

        // //particle Effect Fourth mission "Sumur Maut"
        // if(getQuest.questList[3].isActive && getQuest.questList[2].isCompleted ){
        //     particleEffect[3].SetActive(true);
        // }else if(getQuest.questList[3].isCompleted){
        //     particleEffect[3].SetActive(false);
        // }else{
        //     particleEffect[3].SetActive(false);
        // }

        // //particle Effect Last mission "Quiz"
        // if(getQuest.questList[4].isActive && getQuest.questList[3].isCompleted ){
        //     particleEffect[4].SetActive(true);
        // }else if(getQuest.questList[4].isCompleted){
        //     particleEffect[4].SetActive(false);
        // }else{
        //     particleEffect[4].SetActive(false);
        // }