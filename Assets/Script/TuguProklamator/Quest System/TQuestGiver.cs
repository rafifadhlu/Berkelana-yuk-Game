using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using System.Linq;
using System;
using UnityEngine.UI;
using TMPro;

public class TQuestGiver : MonoBehaviour,IDataPersistence
{
    public QuestsList quests;
    public GameObject panelMission;
    public TextMeshProUGUI nameWold;
    public TextMeshProUGUI[] nameMission; 
    public GameObject[] borderMission;
    private int preventSpammedClick = 0;

   public void LoadData(GameData data){
        if(data.GameQuestData != null){
            this.quests = data.GameQuestData[1];
            InstanceTP.Instance.quests = this.quests;
        }
        RefreshInspector();
    }

    public void SaveData(ref GameData data){
        data.GameQuestData[1] = this.quests;
    }
    void Start()
    {
        StartCoroutine(checkLoadedData());
        
    }

    private IEnumerator checkLoadedData(){
        yield return new WaitForEndOfFrame();
        if (quests != null && quests.questList.Count > 0)
        {
            Debug.Log("Value From Start : " + quests.questList[0].titleMission);
            Debug.Log("Value From Instance : " + InstanceTP.Instance.quests.questList[0].titleMission);
            
            if(quests.questList[0].isActive || quests.questList[0].isCompleted){
                keepItOn();            
            }else{
                panelMission.SetActive(false);
            }
        }
        else
        {
            Debug.LogWarning("Quests data is not loaded properly.");
        }        
    }


    void Update(){
        quests = InstanceTP.Instance.quests;
    }

    public void updateStatusofBorder(){
        int indexLastQuests = quests.questList.Count - 1;
        // Debug.Log(indexLastQuests);

        Color completedColor = new Color(114f / 255f, 242f / 255f, 94f / 255f, 1f);
        Color activeColor = new Color(242f / 255f, 31f / 255f, 12f / 255f, 1f);

        for(int i = 0; i <= indexLastQuests; i++ ){
            // Debug.Log("Index :" + i);
            Image borderImage = borderMission[i].GetComponent<Image>();

            if(quests.questList[i].isCompleted){
                borderImage.color = completedColor;

            }

            if(quests.questList[i].isActive){
                borderImage.color = activeColor;
            }

        }    
    }

    public void openQuestWindow(){
        panelMission.SetActive(true);
        AcceptQuest();
    }

    public void keepItOn(){
        panelMission.SetActive(true);
        showsMission();
        updateStatusofBorder();
    }

    public void showsMission(){
        // string allMissionTitles = "";
        int indexLastQuests = quests.questList.Count - 1;
        Debug.Log(indexLastQuests);

        if(preventSpammedClick < 1){    
            for(int i = 0; i <= indexLastQuests; i++ ){
                Debug.Log("Index :" + i);
                nameMission[i].text += quests.questList[i].titleMission;
            }    
            preventSpammedClick ++;
        }

        // nameMission.text = allMissionTitles;
    }

    public void AcceptQuest(){
     
        // nameWold.text = quests.nameWorld;
        int indexLastQuests = quests.questList.Count - 1;

        for(int i = 0; i <= indexLastQuests; i++ ){
            nameMission[i].text += quests.questList[i].titleMission;

            if(quests.questList[i].isCompleted == true){
                quests.questList[i].isActive = false;
            }else{
                 quests.questList[i].isActive = true;
            }
            
        }

        if (quests != null)
        {
            if (InstanceTP.Instance != null)
            {
                InstanceTP.Instance.quests = quests;
            }
        }
        else
        {
            Debug.LogError("Player or quests is null!");
            Debug.Log($" Quests: {quests}");

        }
    }

    private void RefreshInspector() {
        #if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }

    
    public void saveData(ref GameData data)
    {
        throw new NotImplementedException();
    }

}
