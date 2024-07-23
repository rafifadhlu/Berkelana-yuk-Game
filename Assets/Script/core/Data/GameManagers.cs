using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagers : MonoBehaviour
{
    public static GameManagers Instance {get; private set;}

    public  QuestsList questGiver;
    public  QuestsList questGiverTP;
    public  QuestsList questGiverSM;

    private void Awake(){
        if(Instance != null){
            Debug.Log("Found more than one Data Persistence Manager");
        }
        Instance = this;
    }

}



