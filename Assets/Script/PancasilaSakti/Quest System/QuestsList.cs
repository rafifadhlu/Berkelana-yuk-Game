using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class QuestsList 
{
    public string nameWorld;
    public List<Quest> questList = new List<Quest>();
    public QuestProgress Goal;

    public void Completed(int index){
        questList[index].isCompleted = true;
        questList[index].isActive = false;
        Debug.Log("You has finished first mission" + questList[index].titleMission);
    }       
}

[System.Serializable]
public class Quest
{
    public string titleMission;
    public string descriptionMission;
    public bool isCompleted;
    public bool isActive;

}