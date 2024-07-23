// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;


// [System.Serializable]
// public class TQuestsList 
// {
//     public string nameWorld;
//     public List<TQuest> questList = new List<TQuest>();
//     public TQuestProgress Goal;

//     public void Completed(int index){
//         questList[index].isCompleted = true;
//         questList[index].isActive = false;
//         Debug.Log("You has finished first mission" + questList[index].titleMission);
//     }       

// }


// [System.Serializable]
// public class TQuest
// {
//     public string titleMission;
//     public string descriptionMission;
//     public bool isCompleted;
//     public bool isActive;

// }