using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData {
//    public QuestsList GameQuestData;
   public List<QuestsList> GameQuestData = new List<QuestsList>();

   public GameData(QuestGiverlv1 questGiverLB, QuestGiverlv2 questGiverTP,QuestGiverlv3 questGiverSM){

        if (questGiverLB.setQuestMission != null && questGiverTP != null){
            Debug.Log("World Detected: " + questGiverLB.setQuestMission.nameWorld);
            GameQuestData.Add(questGiverLB.setQuestMission);
            Debug.Log("World Detected: " + questGiverTP.setQuestMission.nameWorld);
            GameQuestData.Add(questGiverTP.setQuestMission);
            Debug.Log("World Detected: " + questGiverSM.setQuestMission.nameWorld);
            GameQuestData.Add(questGiverSM.setQuestMission);
        }else{
            Debug.LogError("Script : GameData / No data found for questGiver & questGiverTP");
        }

   }
}