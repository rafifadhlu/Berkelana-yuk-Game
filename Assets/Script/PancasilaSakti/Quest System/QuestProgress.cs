using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestProgress {
    public GoalType goalType;
    public int requiredTobeFinished;
    public int currentHasFinished;
    public bool isAllCompleted;

    public bool IsReached(){
        return (currentHasFinished >= requiredTobeFinished);
    }

    public void PlayerHasInteract(){
        if(goalType == GoalType.FinishMission) currentHasFinished++;
    }
}

public enum GoalType{
    FinishMission
}
