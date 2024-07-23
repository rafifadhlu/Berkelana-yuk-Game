using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class MQuestsList 
{
    public string nameWorld;
    public List<MQuest> questList = new List<MQuest>();
    public MQuestProgress Goal;

    public void Completed(int index){
        questList[index].isCompleted = true;
        questList[index].isActive = false;
        Debug.Log("You has finished first mission" + questList[index].titleMission);
    }       

}


[System.Serializable]
public class MQuest
{
    public string titleMission;
    public string descriptionMission;
    public bool isCompleted;
    public bool isActive;

}