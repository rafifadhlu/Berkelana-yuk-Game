using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using System;

public class MQuis : MGetContenQuis
{
    [Header("Display Property")]
    public TextMeshProUGUI textBox;
    private string[] lines;
    private string tempLine;

    private float textSpeed;
    private int index;

    [Header("Sprite Image Property")]
    public List<MSpriteList> ListImageMateri = new List<MSpriteList>();
    public Image spritePlaceToDisplay;


    [Header("GameObject Property")]
    public GameObject canvasGameButton;
    public GameObject canvasMateri;
    public GameObject canvasQuestions;
    public GameObject mgetConten;

    // private MGetContenQuis getData;
    private MAskAnswerSystem Questions;
    private Sprite[] PictureSejarahMuseum;
    private Sprite[] PictureIsiMuseum;
    private Sprite[] tempSprite;
    private int indexPic;
    bool isStageCleared ;

    public AudioClip[] audioMateriSejarahBangunan;
    public AudioClip[] audioMateriIsiMuseum;

    private AudioClip[] PlaceTextAudioMaterial;
    public AudioManagerMS audioManager;
    private PopUpManagerMSJKT PopUp;

    void Awake(){
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerMS>();
    }   

    // Start is called before the first frame update
    public void Start()
    {
        textBox.text = string.Empty;
        getAllMaterial();

        PictureSejarahMuseum = ListImageMateri[0].fileImage;
        PictureIsiMuseum = ListImageMateri[1].fileImage;
        PopUp = FindObjectOfType<PopUpManagerMSJKT>();
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

            case "Menuju Bangunan Museum Sejarah Jakarta":
                lines = new string[] { };
                tempSprite = new Sprite [] {};
                PlaceTextAudioMaterial = new AudioClip []{};
                textSpeed = 0.055f;
                
                PlaceTextAudioMaterial = audioMateriSejarahBangunan;
                lines = TextSejarahBangunan;

                tempSprite = PictureSejarahMuseum;
                
                StartLineMaterial();
                break;
            
            case "Menuju Pintu Tengah":
                lines = new string[] { };
                tempSprite = new Sprite [] {};
                PlaceTextAudioMaterial = new AudioClip []{};
                textSpeed = 0.055f;
                
                PlaceTextAudioMaterial = audioMateriIsiMuseum;
                tempLine = TextIsiMuseum[0];
                lines = HelperDivideString(tempLine,"//");

                tempSprite = PictureIsiMuseum;
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
            PopUp.setPopup("missionCompleted");
            Debug.Log("There's no line anymore, current index: " + index);
        }
    }


    public void displayNextLine(){
        if(textBox.text == lines[index]){
            NextLine();
        }else{
            // StopAllCoroutines();
            // textBox.text = lines[index];
            // Debug.Log("else Display nextline: " + index);
        }
    }
}

