using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour, IDataPersistence
{
    public GameObject menupanel;
    public GameObject chooseworld;
    public GameObject credits;
    public GameObject player;
    public GameObject settings;
    public QuestsList questsLB;
    public QuestsList questsTP;
    public QuestsList questsMSJKT;
    public Button[] Buttonlevels;
    public Image[] ImagesButton;
    public Sprite[] spriteWon;

    public Animator camAnimator;
    public Animator chooseWorldAnimator;

    private AudioToggle audioToggle;

    void Awake(){
        audioToggle = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioToggle>();
    }

    public void LoadData(GameData data){
        if(data.GameQuestData != null){
            this.questsLB = data.GameQuestData[0];
            this.questsTP = data.GameQuestData[1];
            this.questsMSJKT = data.GameQuestData[2];
        }
        RefreshInspector();
    }

    public void SaveData(ref GameData data){
        // data.GameQuestData[1] = this.quests;
    }

    void Start()
    {
        Time.timeScale = 1;
        menupanel.SetActive(true);
        chooseworld.SetActive(false);
        credits.SetActive(false);
        settings.SetActive(false);
        chooseWorldAnimator.SetBool("IdleChooseWorld",true);
    }

    // Update is called once per frame
    void Update()
    {
        if(questsLB.Goal.isAllCompleted){
            //Change Sprite button of LB
            ImagesButton[0].sprite = spriteWon[0];
            
            //Make TP Button Unlock
            Buttonlevels[0].interactable = true;
        }else{
            Buttonlevels[0].interactable = false;
        }

        if(questsTP.Goal.isAllCompleted){
            //Change Sprite button of TP
            ImagesButton[1].sprite = spriteWon[0];
            
            //Make MSJKT Button Unlock
            Buttonlevels[1].interactable = true;
        }else{
            Buttonlevels[1].interactable = false;
        }

        if(questsMSJKT.Goal.isAllCompleted){
            //Change Sprite button of MSJKT
            ImagesButton[2].sprite = spriteWon[2];
        }

    }

    public void GoesToSetting(){
        audioToggle.PlaySFX(audioToggle.BUTTONCLICKED);
        settings.SetActive(true);
        menupanel.SetActive(false);
    }
    
    public void goesToChooseWorld(){
        Debug.Log("Button Clicked");

        if (chooseWorldAnimator != null && chooseWorldAnimator.gameObject.activeInHierarchy)
        {
            // Set the animation parameters
            Debug.Log("Animation detected choose world method");
            chooseWorldAnimator.SetBool("OpenChooseWorld", true);
            chooseWorldAnimator.SetBool("IdleChooseWorld", false);
        }
        else
        {
            Debug.LogWarning("Animator or GameObject is not active!");
        }

        audioToggle.PlaySFX(audioToggle.BUTTONCLICKED);
        // chooseWorldAnimator.SetBool("OpenChooseWorld",true);
        // chooseWorldAnimator.SetBool("IdleChooseWorld",false);
    }

    public void goesToMainMenu(){
        audioToggle.PlaySFX(audioToggle.BUTTONCLICKED);
        menupanel.SetActive(true);
        credits.SetActive(false);
        chooseworld.SetActive(false);
        settings.SetActive(false);
        chooseWorldAnimator.SetBool("OpenChooseWorld",false);
        chooseWorldAnimator.SetBool("IdleChooseWorld",true);

        camAnimator.SetBool("CameraOpen",true);
        camAnimator.SetBool("CameraClosed",false);
    }

    public void goesToCredits(){
        audioToggle.PlaySFX(audioToggle.BUTTONCLICKED);
        credits.SetActive(true);
        menupanel.SetActive(false);
        camAnimator.SetBool("CameraClosed",true);
        camAnimator.SetBool("CameraOpen",false);
    }

    public void QuitGame(){
        Application.Quit();
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
