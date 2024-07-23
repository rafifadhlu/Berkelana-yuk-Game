using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDatata : MonoBehaviour
{
    public QuestsList quests;

    //method ini dipanggil sesaat frame pertama kali dijalankan    
    //     if(!quests.questList[0].isActive && !quests.questList[0].isCompleted){
    //         ShowsInstruction(0);
    //     }

     void Start(){
        if (MSJKTInstance.Instance != null)
        {
            quests = MSJKTInstance.Instance.quests;
            // Use questDetails as needed
            Debug.Log("Quest Details in TestData: " + quests.nameWorld);
        }
    }
}
