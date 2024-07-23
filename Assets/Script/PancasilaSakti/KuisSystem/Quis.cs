using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class Quis : GetContenQuis
{
    [Header("Display Property")]
    public TextMeshProUGUI textBox;
    private string[] lines;
    private string tempLine;

    private float textSpeed;
    private int index;

    [Header("Sprite Image Property")]
    public List<SpriteList> ListImageMateri = new List<SpriteList>();
    public Image spritePlaceToDisplay;


    [Header("GameObject Property")]
    public GameObject canvasGameButton;
    public GameObject canvasMateri;
    public GameObject canvasQuestions;
    private AskAnswerSystem Questions;
    private Sprite[] PictureSerambi;
    private Sprite[] PicturePosDanDapur;
    private Sprite[] PictureSumurMaut;
    private Sprite[] PictureKawasan;
    private Sprite[] tempSprite;
    private int indexPic;
    bool isStageCleared;

    public AudioClip[] audioMateriKawasan;
    public AudioClip[] audioMateriSerambiPenyiksaan;
    public AudioClip[] audioMateriPosDanDapur;
    public AudioClip[] audioMaterisumurMaut;


    private AudioClip[] PlaceTextAudioMaterial;

    public AudioManagerLB audioManager;
    private PopUpManagerLB PopUp;

    void Awake(){
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerLB>();
    }   

    // Start is called before the first frame update
    public void Start()
    {
        textBox.text = string.Empty;
        getAllMaterial();

        PictureSerambi = ListImageMateri[0].fileImage;
        PicturePosDanDapur = ListImageMateri[1].fileImage;
        PictureSumurMaut = ListImageMateri[2].fileImage;
        PictureKawasan = ListImageMateri[3].fileImage;
        PopUp = FindObjectOfType<PopUpManagerLB>(); 
    }
    
    public void startLearning(string nameMission){
        canvasGameButton.SetActive(false);
        canvasMateri.SetActive(true);
        textBox.text = string.Empty;
        spritePlaceToDisplay.sprite = null;

        // Time.timeScale = 0;
        Debug.Log(nameMission);

        switch (nameMission)
        {
            case "Menuju Depan Kawasan":
                lines = new string[] { };
                tempSprite = new Sprite [] {};
                PlaceTextAudioMaterial = new AudioClip []{};
                textSpeed = 0.055f;

                
                PlaceTextAudioMaterial = audioMateriKawasan;
                tempLine = TextKawasandanSumurMaut[0];
                lines = HelperDivideString(tempLine,"//");
                
                tempSprite = PictureKawasan;
                StartLineMaterial();
                break;

            case "Menuju Serambi Penyiksaan":
                lines = new string[] { };
                tempSprite = new Sprite [] {};
                PlaceTextAudioMaterial = new AudioClip []{};
                textSpeed = 0.035f;

                PlaceTextAudioMaterial = audioMateriSerambiPenyiksaan;

                tempLine = TextSerambi[0];
                lines = HelperDivideString(tempLine,"//");

                tempSprite = PictureSerambi;
                StartLineMaterial();
                break;
            
            case "Menuju Pos dan Dapur":
                lines = new string[] { };
                tempSprite = new Sprite [] {};
                PlaceTextAudioMaterial = new AudioClip []{};
                textSpeed = 0.055f;

                PlaceTextAudioMaterial = audioMateriPosDanDapur;
                tempLine = TextPosdanDapur[0];
                lines = HelperDivideString(tempLine,"//");

                tempSprite = PicturePosDanDapur;
                StartLineMaterial();
                break;
            
            case "Menuju sumur Maut":
                lines = new string[] { };
                tempSprite = new Sprite [] {};
                PlaceTextAudioMaterial = new AudioClip []{};
                textSpeed = 0.055f;
                
                PlaceTextAudioMaterial = audioMaterisumurMaut;
                lines = TextSumurMaut;
                // lines = getData.HelperDivideString(tempLine,"//");
                
                tempSprite = PictureSumurMaut;
                StartLineMaterial();
                break;
            
            case "Ambil Kuis":
                Questions.quizStart();

                break;

            default:
                Debug.Log("There are no choices");
                break;
        }
    }


    void StartLineMaterial(){
        index = 0;
        indexPic = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine(){
        spritePlaceToDisplay.sprite = tempSprite[indexPic];

        audioManager.PlayAudioText(PlaceTextAudioMaterial[index]);

        foreach(char c in lines[index].ToCharArray()){
            textBox.text += c;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }

    void NextLine(){
        if(indexPic < tempSprite.Length - 1){
            indexPic++;
        }else{
            indexPic = 0;
        }

        if(index < lines.Length - 1){
            index++;
            textBox.text = string.Empty;
            StartCoroutine(TypeLine());
            Debug.Log("True NextLine: " + index);
        }else{
            canvasGameButton.SetActive(true);
            canvasMateri.SetActive(false);
            Time.timeScale = 1;
            Debug.Log("There's no line anymore, current index: " + index);
            PopUp.setPopup("missionCompleted");
        }
    }


    public void displayNextLine(){
        if(textBox.text == lines[index]){
            NextLine();
        }
    }
}


// [System.Serializable]
// public class linesFull
// {
//     public string nameWorld;
//     public linesList[] lines;
//     // public AudioClip audio;

// }


// [System.Serializable]
// public class linesList
// {
//     public string titleMission;
//     public AudioClip audio;

// }
