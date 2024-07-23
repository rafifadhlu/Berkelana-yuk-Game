using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MQuestProgress {
    public MGoalType goalType;
    public int requiredTobeFinished;
    public int currentHasFinished;
    public bool isAllCompleted;

    public bool IsReached(){
        return (currentHasFinished >= requiredTobeFinished);
    }

    public void PlayerHasInteract(){
        if(goalType == MGoalType.FinishMission) currentHasFinished++;
    }
}

public enum MGoalType{
    FinishMission
}
