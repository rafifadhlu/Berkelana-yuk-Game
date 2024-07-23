using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class TQuis : TGetContenQuis
{
    [Header("Display Property")]
    public TextMeshProUGUI textBox;
    private string[] lines;
    private string tempLine;

    private float textSpeed;
    private int index;

    [Header("Sprite Image Property")]
    public List<TSpriteList> ListImageMateri = new List<TSpriteList>();
    public Image spritePlaceToDisplay;


    [Header("GameObject Property")]
    public GameObject canvasGameButton;
    public GameObject canvasMateri;
    public GameObject canvasQuestions;

    private TAskAnswerSystem Questions;
    private Sprite[] PictureTuguPetir;
    private Sprite[] PictureTuguPeringatan;
    private Sprite[] PictureKawasan;
    private Sprite[] tempSprite;
    private int indexPic;
    bool isStageCleared ;

    public AudioClip[] audioMateriKawasanTP;
    public AudioClip[] audioMateriTuguPetir;
    public AudioClip[] audioMateriTuguPeringatanSatuTahun;

    private AudioClip[] PlaceTextAudioMaterial;
    public AudioManagerTP audioManager;
    private PopUpManagerTP PopUp;

    void Awake(){
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerTP>();
    }   

    // Start is called before the first frame update
    public void Start()
    {
        textBox.text = string.Empty;
        getAllMaterial();

        PictureKawasan = ListImageMateri[0].fileImage;
        PictureTuguPetir = ListImageMateri[1].fileImage;
        PictureTuguPeringatan = ListImageMateri[2].fileImage;
        PopUp = FindObjectOfType<PopUpManagerTP>();
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
            case "Menuju Tugu Petir":
                lines = new string[] { };
                tempSprite = new Sprite [] {};
                PlaceTextAudioMaterial = new AudioClip []{};
                textSpeed = 0.055f;
                
                PlaceTextAudioMaterial = audioMateriTuguPetir;
                tempLine = TextTuguPetir[0];
                lines = HelperDivideString(tempLine,"//");
                
                tempSprite = PictureTuguPetir;
                StartLineMaterial();
                break;

            case "Menuju Tugu Peringatan Satu Tahun":
                lines = new string[] { };
                tempSprite = new Sprite [] {};
                PlaceTextAudioMaterial = new AudioClip []{};
                textSpeed = 0.050f;
                
                PlaceTextAudioMaterial = audioMateriTuguPeringatanSatuTahun;
                tempLine = TextTuguPeringatan[0];
                lines = HelperDivideString(tempLine,"//");

                tempSprite = PictureTuguPeringatan;
                StartLineMaterial();
                break;
            
            case "Pengetahuan Kawasan Tugu Proklamasi":
                lines = new string[] { };
                tempSprite = new Sprite [] {};
                PlaceTextAudioMaterial = new AudioClip []{};
                textSpeed = 0.055f;
                
                PlaceTextAudioMaterial = audioMateriKawasanTP;
                tempLine = TextKawasanTP[0];
                lines = HelperDivideString(tempLine,"//");

                tempSprite = PictureKawasan;
                StartLineMaterial();
                break;
            
            case "Ambil Kuis":
                Debug.Log("Work In Progress");
                Questions.quizStart();

                break;

            default:
                Debug.Log("There are no choosen");
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
        Debug.Log("index : "+ index);
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
