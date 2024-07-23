using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class NPCInteract : MonoBehaviour
{
    //Properti panel 
    public GameObject panelButtonEksplor;
    public GameObject panelDialogue;
    public GameObject panelMission;
    
    //Properti Animation dan Effect 
    public Animator BoxAnimator;
    public GameObject[] particleEffect;
    
    //Properti Quis serta materi Pembelajaran 
    private ProcessDataFromTxt getCleanData = new ProcessDataFromTxt();
    int dialogueCount; 
    public QuestsList getQuest;
    public Quis startQuis;
    private AskAnswerSystem quiz;
    private PauseMenu gameMenu;

    public AudioManagerLB audioManager;
    private PopUpManagerLB PopUp;
    
    //method awake dipanggil sebelum frame mulai
    void Awake(){
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerLB>();
    }    

    //method ini dipanggil sesaat frame pertama kali dijalankan    
    void Start(){
        //Mengambil data content melalui materi yang tersedia
        getCleanData.ProcessData();

        for (int i = 0; i <= particleEffect.Count() - 1 ; i++)
        {
            particleEffect[i].SetActive(false);
        }

        // Mengambil data game dari properti Instance 
        if(QuestManager.Instance != null)
        {
            getQuest = QuestManager.Instance.quests;
            Debug.Log("Quest Details: " + getQuest.nameWorld);
        }
        //Inisialisasi script yang diperlukan
        gameMenu = FindObjectOfType<PauseMenu>();
        quiz = FindObjectOfType<AskAnswerSystem>();
        PopUp = FindObjectOfType<PopUpManagerLB>(); 
    }
    
    //method ini dipanggil setiap frame berjalan e.g 30 fps (frame per second), 
    //maka method ini berjalan 30 kali setiap detik
     void Update(){
        //Mengupdate data ke dalam properti setiap ada perubahan
        getQuest = QuestManager.Instance.quests;
        //Mengupdate data ke dalam properti setiap ada perubahan
        checkParticle();
    }

    //Visual Effect untuk menandai tempat untuk menjalankan misi
    void checkParticle(){
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

    //Method Interact player dengan NPC di awal game.
    public void Interact(){
        if(getQuest.Goal.isAllCompleted == true){
            startDialogueAfterFinish();
            FindObjectOfType<NPCAnim>().startTalk();
        }else if(getQuest.questList[0].isActive || getQuest.questList[0].isCompleted){
            FindObjectOfType<NPCAnim>().startTalk();
            startOtherDialogue();
        }else{
            FindObjectOfType<NPCAnim>().startTalk();
            startFirstDialogue();
        }        
    }

    //Method Interact Object di tempat untuk mendapatkan pengetahuan tentang kawasan monumen pancasila sakti.
    public void InteractLearnKawasan(){
        //Kondisi untuk ketentuan saat menjalankan misi.

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
        //Apabila kondisi diatas tidak terpenuhi maka player dapat menjalankan misi dan menyelesaikan satu misi
        else{
            Debug.Log("Quest Has Detected = " + getQuest.nameWorld);
            //Memulai materi pembelajaran melalui method yang ada di script Quis
            startQuis.startLearning(getQuest.questList[0].titleMission);

            PlayerCompletedOneofMission(0);
        }
    }
    
    //Method Interact Object di tempat untuk mendapatkan pengetahuan tentang Serambi Penyiksaan.
    public void InteractSerambi(){
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

     //Method Interact Object di tempat untuk mendapatkan pengetahuan tentang Pos Komando dan dapur umum.
     public void InteractPosDanDapur(){
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
     //Method Interact Object di tempat untuk mendapatkan pengetahuan tentang Sumur Maut.
    public void InteractSumurMaut(){
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
                startQuis.startLearning(getQuest.questList[3].titleMission);
                PlayerCompletedOneofMission(3);   
            }
        }
    }
     //Method Interact Object di tempat untuk mendapatkan pengetahuan tentang Quiz.
    public void InteractQuis(){
         if(getQuest.questList[4].isActive == false){
           
            if(getQuest.questList[4].isCompleted == false){
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

            if(getQuest.questList[3].isCompleted == false){
                Debug.Log("You have to finish second Mission");
                audioManager.PlaySFX(audioManager.WARNING);
                PopUp.setPopup("AlertGoPrevous");

            }else{
                Debug.Log("Quest Has Detected = " + getQuest.nameWorld);
                //Memulai kuis melalui method yang ada di script Quiz
                quiz.quizStart();   
            }
        }
    }

    //method untuk mengupdate informasi misi yang sudah selesai dengan satu parameter index
     public void PlayerCompletedOneofMission(int index){
        //melakukan pengecekan apakah benar questlist belum complete?
        if(getQuest.questList[index].isCompleted == false){
            getQuest.Completed(index);
            getQuest.Goal.PlayerHasInteract();
            
            //mark as finished change border colours
            FindObjectOfType<QuestGiver>().updateStatusofBorder();

            //mengembil method IsReached untuk pengecekan nilai. Apabila berhasil menyelesaikan semua misi
            //maka player memenangkan world tersebut.
            if(getQuest.Goal.IsReached()){
                
                audioManager.PlaySFX(audioManager.COMPLETEDPART);
                
                getQuest.Goal.isAllCompleted = true;
                gameMenu.GameEnded();
                Debug.Log("You have been played so hard. congratulations");
            }
        }
    }

    //Pada method interact untuk dialog, terdapat 3 kondisi yaitu sebelum bermain, bermain, dan setelah bermain 
    //method ini digunakan untuk memulai dialog saat player pertama kali bermain
    public void startFirstDialogue(){
        panelButtonEksplor.SetActive(false);
        panelMission.SetActive(false);
        panelDialogue.SetActive(true);

        BoxAnimator.SetBool("IsOpen",true);
        FindObjectOfType<DialogueManager>().StartDialogue(getCleanData.nameCharacterDialog1,getCleanData.dialog1);
        dialogueCount += 1;
    }

    //method ini digunakan untuk memulai dialog saat player sudah menyelesaikan game
    public void startDialogueAfterFinish(){
        panelButtonEksplor.SetActive(false);
        panelMission.SetActive(false);
        panelDialogue.SetActive(true);

        BoxAnimator.SetBool("IsOpen",true);
        FindObjectOfType<DialogueManager>().StartDialogue(getCleanData.nameCharacterDialog2,getCleanData.dialog3);
        dialogueCount += 1;
    }

    //method ini digunakan untuk memulai dialog saat player sedang menyelesaikan game
    public void startOtherDialogue(){
        panelButtonEksplor.SetActive(false);
        panelMission.SetActive(false);
        panelDialogue.SetActive(true);

        BoxAnimator.SetBool("IsOpen",true);
        FindObjectOfType<DialogueManager>().StartDialogue(getCleanData.nameCharacterDialog2,getCleanData.dialog2);
        dialogueCount += 1;
    }
    
    //method untuk menutup dialogue box
    public void closeDialogue(){
        Debug.Log("Inside Close Dialogue" + dialogueCount);

        if(getQuest.Goal.isAllCompleted == true){
            BoxAnimator.SetBool("IsOpen",false);
            panelButtonEksplor.SetActive(true);
            panelDialogue.SetActive(false);
            panelMission.SetActive(false);
            FindObjectOfType<NPCAnim>().idle();

        }else if(getQuest.questList[0].isActive || getQuest.questList[0].isCompleted){
            BoxAnimator.SetBool("IsOpen",false);
            panelButtonEksplor.SetActive(true);
            panelDialogue.SetActive(false);
            FindObjectOfType<NPCAnim>().idle();
            FindObjectOfType<QuestGiver>().keepItOn();

        }else{
            BoxAnimator.SetBool("IsOpen",false);
            panelButtonEksplor.SetActive(true);
            panelDialogue.SetActive(false);
            panelMission.SetActive(true);
            FindObjectOfType<QuestGiver>().openQuestWindow();
            FindObjectOfType<NPCAnim>().idle();
            FindObjectOfType<TutorialManager>().ShowsInstruction(1);
        }   
    }

}
