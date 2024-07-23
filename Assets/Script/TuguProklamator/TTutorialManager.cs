using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TTutorialManager : MonoBehaviour
{
    [Header("----PROPERTI IN-GAME TUTORIAL----")]
    public GameObject canvasInstruction;
    public TextMeshProUGUI textTutorial;
    public Image PlaceForDisplay;
    public Sprite[] features;
    public Sprite[] missions;
    int indexIntroFeature;
    int indexIntroMission;
    
    private string[] IntroductionFeatures = new string[]
    {
        "Selamat datang di World Kedua! aku akan ingatkan lagi yaa",
        "Pertama Kamu ada Joystick. Joystick ini untuk kamu bergerak",
        "Kedua yaitu, Interaksi. Tombol ini untuk berinteraksi dengan NPC dan mengerjakan misi.",
        "Ketiga yaitu, Setting dan menu baca. Tombol setting memungkinkan kamu untuk mengatur volume.",
        "dan menu baca merupakan menu untuk membaca kembali informasi yang sudah kamu dapatkan.",
        "Selanjutnya kamu bisa memulai misi dengan cara interaksi dengan Ebi"
    };

    private string[] IntroductionMission = new string[]
    {
        "Wah kamu sudah dapat misi dari Ebi!",
        "Kamu bisa liat misi yang diberikan pada panel disamping",
        "selanjutnya kamu harus mencari dan menuju titik-titik untuk menyelesaikan misi yaa",
        "lalu kamu menuju ke tanda itu dan interaksi",
        "Ingatt yaa! harus menyimak penjelasan Septi. Karena semua yang diberikan akan menjadi bekal di misi terakhir. Terima kasihh!"
    };

    public QuestsList quests;
    private int stateTutor;

    void Start(){
        if (InstanceTP.Instance != null)
        {
            quests = InstanceTP.Instance.quests;
            // Use questDetails as needed
            Debug.Log("Quest Details in Player Interact: " + quests.nameWorld);
        }

        if(!quests.questList[0].isActive && !quests.questList[0].isCompleted){
            ShowsInstruction(0);
        }
    }

    public void ShowsInstruction(int cases){
        switch (cases)
        {
            case 0 :
                Time.timeScale = 0;
                canvasInstruction.SetActive(true);
                displayTipsFeatureToScreen();
                break;
            case 1 :
                Time.timeScale = 0;
                canvasInstruction.SetActive(true);
                displayTipsMissionToScreen();
                break;
            default:
                canvasInstruction.SetActive(false);
                break;
        }
    }

    public void nextTips(){

        if(stateTutor == 0){
            if(indexIntroFeature < IntroductionFeatures.Length - 1){
                indexIntroFeature++;
                ShowsInstruction(0);
                Debug.Log("True NextLine: " + indexIntroFeature);
            }else{
                canvasInstruction.SetActive(false);
                Time.timeScale = 1;
                Debug.Log("There's no line anymore, current index: " + indexIntroFeature);
            }
        }else{
            if(indexIntroMission < IntroductionMission.Length - 1){
                indexIntroMission++;
                ShowsInstruction(1);
                Debug.Log("True NextLine: " + indexIntroMission);
            }else{
                canvasInstruction.SetActive(false);
                Time.timeScale = 1;
                Debug.Log("There's no line anymore, current index: " + indexIntroMission);
            }
        }
    
    }

    private void displayTipsFeatureToScreen(){
        stateTutor = 0;
        textTutorial.text = string.Empty;
        PlaceForDisplay.sprite = features[indexIntroFeature];
        textTutorial.text = IntroductionFeatures[indexIntroFeature];
    }

    private void displayTipsMissionToScreen(){
        stateTutor = 1;
        textTutorial.text = string.Empty;

        PlaceForDisplay.sprite = missions[indexIntroMission];
        textTutorial.text = IntroductionMission[indexIntroMission];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
