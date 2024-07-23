using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TNPCInteract : MonoBehaviour
{
    public GameObject panelButtonEksplor;
    public GameObject panelDialogue;
    public GameObject panelMission;
    public Animator BoxAnimator;
    public GameObject[] particleEffect;


    private TProcessDataFromTxt getCleanData = new TProcessDataFromTxt();
    int dialogueCount; 
    public QuestsList getQuest = new QuestsList();
    public TQuis startQuis;
    private TAskAnswerSystem quiz;
    
    private TPauseMenu gameMenu;
    private AudioManagerTP audioManager;
    private PopUpManagerTP PopUp;
    
    void Awake(){
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerTP>();
    } 

    void Start(){
        getCleanData.ProcessData();
        
        for (int i = 0; i <= particleEffect.Count() - 1 ; i++)
        {
            particleEffect[i].SetActive(false);
        }

        if (InstanceTP.Instance != null)
        {
            getQuest = InstanceTP.Instance.quests;
            // Use questDetails as needed
            Debug.Log("Quest Details: " + getQuest.nameWorld);
        }
        gameMenu = FindObjectOfType<TPauseMenu>();
        quiz = FindObjectOfType<TAskAnswerSystem>();
        PopUp = FindObjectOfType<PopUpManagerTP>();
    }

    void Update(){
        getQuest = InstanceTP.Instance.quests;
        checkParticle();
    }

    private void checkParticle(){
        
        for (int i = 0; i < particleEffect.Length; i++) {
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
            FindObjectOfType<TNPCAnim>().startTalk();
            startDialogueAfterFinish();
        }else if(getQuest.questList[0].isActive || getQuest.questList[0].isCompleted){
            FindObjectOfType<TNPCAnim>().startTalk();
            startOtherDialogue();
        }else{
            FindObjectOfType<TNPCAnim>().startTalk();
            startFirstDialogue();
        }        
    }

    public void InteractLearnKawasan(){
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

    public void InteractSPetir(){
        if(getQuest.questList[1].isActive == false){
           
            if(getQuest.questList[1].isCompleted == false){
                
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

     public void InteractSatuTahun(){
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
                Debug.Log("You have to finish first Mission");
                
                audioManager.PlaySFX(audioManager.WARNING);
                PopUp.setPopup("AlertGoPrevous");

            }else{
                Debug.Log("Quest Has Detected = " + getQuest.nameWorld);
                startQuis.startLearning(getQuest.questList[2].titleMission);
                PlayerCompletedOneofMission(2);   
            }
        }
    }

    public void InteractQuis(){
        Debug.Log("Quis Still On Progress");

         if(getQuest.questList[3].isActive == false){
           
            if(getQuest.questList[3].isCompleted == false){
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

            if(getQuest.questList[2].isCompleted == false){
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

            //mark as finished change border colors
            FindObjectOfType<TQuestGiver>().updateStatusofBorder();

            if(getQuest.Goal.IsReached()){
                gameMenu.GameEnded();
                audioManager.PlaySFX(audioManager.COMPLETEDPART);

                getQuest.Goal.isAllCompleted = true;
                Debug.Log("You have been played so hard. congratulations");
            }
        }
    }

    public void startFirstDialogue(){
        panelButtonEksplor.SetActive(false);
        panelMission.SetActive(false);
        panelDialogue.SetActive(true);

        BoxAnimator.SetBool("IsOpen",true);
        FindObjectOfType<TDialogueManager>().StartDialogue(getCleanData.nameCharacterDialog1,getCleanData.dialog1);
        dialogueCount += 1;
    }

    public void startOtherDialogue(){
        panelButtonEksplor.SetActive(false);
        panelMission.SetActive(false);
        panelDialogue.SetActive(true);

        BoxAnimator.SetBool("IsOpen",true);
        FindObjectOfType<TDialogueManager>().StartDialogue(getCleanData.nameCharacterDialog2,getCleanData.dialog2);
        dialogueCount += 1;
    }

    public void startDialogueAfterFinish(){
        panelButtonEksplor.SetActive(false);
        panelMission.SetActive(false);
        panelDialogue.SetActive(true);

        BoxAnimator.SetBool("IsOpen",true);
        FindObjectOfType<TDialogueManager>().StartDialogue(getCleanData.nameCharacterDialog2,getCleanData.dialog3);
        dialogueCount += 1;
    }


    public void closeDialogue(){
        Debug.Log("Inside Close Dialogue" + dialogueCount);

        if(getQuest.Goal.isAllCompleted == true){
            BoxAnimator.SetBool("IsOpen",false);
            panelButtonEksplor.SetActive(true);
            panelDialogue.SetActive(false);
            panelMission.SetActive(false);
            FindObjectOfType<TNPCAnim>().idle();

        }else if(getQuest.questList[0].isActive || getQuest.questList[0].isCompleted){
            BoxAnimator.SetBool("IsOpen",false);
            panelButtonEksplor.SetActive(true);
            panelDialogue.SetActive(false);
            FindObjectOfType<TQuestGiver>().keepItOn();
            FindObjectOfType<TNPCAnim>().idle();

        }else{
            BoxAnimator.SetBool("IsOpen",false);
            panelButtonEksplor.SetActive(true);
            panelDialogue.SetActive(false);
            panelMission.SetActive(true);
            FindObjectOfType<TQuestGiver>().openQuestWindow();
            FindObjectOfType<TTutorialManager>().ShowsInstruction(1);
            FindObjectOfType<TNPCAnim>().idle();
        }   
    }
}
