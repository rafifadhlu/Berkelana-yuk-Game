using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MNPCInteract : MonoBehaviour
{
    public GameObject panelButtonEksplor;
    public GameObject panelDialogue;
    public GameObject panelMission;
    public Animator BoxAnimator;
    public GameObject[] particleEffect;


    private MProcessDataFromTxt getCleanData = new MProcessDataFromTxt();
    int dialogueCount; 
    public QuestsList getQuest;
    public MQuis startQuis;
    private MAskAnswerSystem quiz;
    private MPauseMenu gameMenu;
    private AudioManagerMS audioManager;
    private PopUpManagerMSJKT PopUp;

     void Awake(){
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerMS>();
    } 

    void Start(){
        getCleanData.ProcessData();
        for (int i = 0; i <= particleEffect.Count() - 1 ; i++)
        {
            particleEffect[i].SetActive(false);
        }

        if (MSJKTInstance.Instance != null)
        {
            getQuest = MSJKTInstance.Instance.quests;
            // Use questDetails as needed
            Debug.Log("Quest Details: " + getQuest.nameWorld);
        }
        
        quiz = FindObjectOfType<MAskAnswerSystem>();
        gameMenu = FindObjectOfType<MPauseMenu>();
        PopUp = FindObjectOfType<PopUpManagerMSJKT>();
    }
    void Update(){
        getQuest = MSJKTInstance.Instance.quests;
        checkParticle();
    }

    void checkParticle(){
        
        for (int i = 0; i < particleEffect.Length; i++) {
            
            if (particleEffect[i] == null) {
            continue;
            }

            if (getQuest.questList[i].isCompleted) {
                particleEffect[i].SetActive(false);
            } else if (i == 0 || getQuest.questList[i - 1].isCompleted) {
                particleEffect[i].SetActive(getQuest.questList[i].isActive);
            } else {
                particleEffect[i].SetActive(false);
            }
        }
    }

    public void Interact(){
        if(getQuest.Goal.isAllCompleted == true){
            FindObjectOfType<MNPCAnim>().startTalk();
            startDialogueAfterFinish();
        }else if(getQuest.questList[0].isActive || getQuest.questList[0].isCompleted){
            FindObjectOfType<MNPCAnim>().startTalk();
            startOtherDialogue();
        }else{
            FindObjectOfType<MNPCAnim>().startTalk();
            startFirstDialogue();
        }
    }

    public void InteractSejarahBangunan(){
        if(getQuest.questList[0].isActive == false){
           
            if(getQuest.questList[0].isCompleted == false){
                Debug.Log("You have to go to dialogue with NPC");
                
                audioManager.PlaySFX(audioManager.WARNING);
                PopUp.setPopup("AlertGoPrevous");

            }else{

                audioManager.PlaySFX(audioManager.WARNING);
                PopUp.setPopup("alertCompleted");

                Debug.Log("Mission has completed");
            }
        }
        else{
            Debug.Log("Quest Has Detected = " + getQuest.nameWorld);
            startQuis.startLearning(getQuest.questList[0].titleMission);

            PlayerCompletedOneofMission(0);
        }
    }

    public void InteractIsiMuseum(){
        if(getQuest.questList[1].isActive == false){
           
            if(getQuest.questList[1].isCompleted == false){
                Debug.Log("You have learn kawasan");

                audioManager.PlaySFX(audioManager.WARNING);
                PopUp.setPopup("AlertGoPrevous");

            }else{
                
                audioManager.PlaySFX(audioManager.WARNING);
                PopUp.setPopup("alertCompleted");

                Debug.Log("Mission has completed");
            }
        }
        else{

            if(getQuest.questList[0].isCompleted == false){
                Debug.Log("You have to go to dialogue with Ebi");
                
                audioManager.PlaySFX(audioManager.WARNING);
                PopUp.setPopup("AlertGoPrevous");

            }else{
                Debug.Log("Quest Has Detected = " + getQuest.nameWorld);
                startQuis.startLearning(getQuest.questList[1].titleMission);
                PlayerCompletedOneofMission(1);   
            }
        }
    }

    public void InteractQuis(){
        Debug.Log("Quis Still On Progress");

         if(getQuest.questList[2].isActive == false){
           
            if(getQuest.questList[2].isCompleted == false){
                Debug.Log("You have to go to dialogue with NPC");
                
                audioManager.PlaySFX(audioManager.WARNING);
                PopUp.setPopup("AlertGoPrevous");

            }else{
                
                audioManager.PlaySFX(audioManager.WARNING);
                PopUp.setPopup("alertCompleted");

                Debug.Log("Mission has completed");
            }
        }
        else{

            if(getQuest.questList[1].isCompleted == false){
                Debug.Log("You have to finish second Mission");
                
                audioManager.PlaySFX(audioManager.WARNING);
                PopUp.setPopup("AlertGoPrevous");

            }else{
                Debug.Log("Quest Has Detected = " + getQuest.nameWorld);
                quiz.quizStart();   
            }
        }
    }

     public void PlayerCompletedOneofMission(int index){
    //    Debug.Log(index);
        if(getQuest.questList[index].isCompleted == false){
            getQuest.Completed(index);
            getQuest.Goal.PlayerHasInteract();

            FindObjectOfType<MQuestGiver>().updateStatusofBorder();

            if(getQuest.Goal.IsReached()){
                getQuest.Goal.isAllCompleted = true;
                audioManager.PlaySFX(audioManager.COMPLETEDPART);

                gameMenu.GameEnded();
                Debug.Log("You have been played so hard. congratulations");
            }
        }
    }

    public void startFirstDialogue(){
        panelButtonEksplor.SetActive(false);
        panelMission.SetActive(false);
        panelDialogue.SetActive(true);

        BoxAnimator.SetBool("IsOpen",true);
        FindObjectOfType<MDialogueManager>().StartDialogue(getCleanData.nameCharacterDialog1,getCleanData.dialog1);
        dialogueCount += 1;
    }

    public void startDialogueAfterFinish(){
        panelButtonEksplor.SetActive(false);
        panelMission.SetActive(false);
        panelDialogue.SetActive(true);

        BoxAnimator.SetBool("IsOpen",true);
        FindObjectOfType<MDialogueManager>().StartDialogue(getCleanData.nameCharacterDialog2,getCleanData.dialog3);
        dialogueCount += 1;
    }

    public void startOtherDialogue(){
        panelButtonEksplor.SetActive(false);
        panelMission.SetActive(false);
        panelDialogue.SetActive(true);

        BoxAnimator.SetBool("IsOpen",true);
        FindObjectOfType<MDialogueManager>().StartDialogue(getCleanData.nameCharacterDialog2,getCleanData.dialog2);
        dialogueCount += 1;
    }

    public void closeDialogue(){
        Debug.Log("Inside Close Dialogue" + dialogueCount);

        if(getQuest.Goal.isAllCompleted == true){
            BoxAnimator.SetBool("IsOpen",false);
            panelButtonEksplor.SetActive(true);
            panelDialogue.SetActive(false);
            panelMission.SetActive(false);
            FindObjectOfType<MNPCAnim>().idle();

        }else if(getQuest.questList[0].isActive || getQuest.questList[0].isCompleted){
            BoxAnimator.SetBool("IsOpen",false);
            panelButtonEksplor.SetActive(true);
            panelDialogue.SetActive(false);
            FindObjectOfType<MNPCAnim>().idle();
            FindObjectOfType<MQuestGiver>().keepItOn();

        }else{
            BoxAnimator.SetBool("IsOpen",false);
            panelButtonEksplor.SetActive(true);
            panelDialogue.SetActive(false);
            panelMission.SetActive(true);
            FindObjectOfType<MQuestGiver>().openQuestWindow();
            FindObjectOfType<MNPCAnim>().idle();
            FindObjectOfType<MTutorialManager>().ShowsInstruction(1);
        }   
    }

}
