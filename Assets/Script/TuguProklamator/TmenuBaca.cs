using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.Events;
using System;

public class TmenuBaca : MonoBehaviour
{

    [Header("Material Attribute")]
    public TextMeshProUGUI materialLine;
    public Image imageMaterial;
    public Sprite defaultImage;
    public TextMeshProUGUI[] TextbuttonMateri;
    public Button[] buttonMateri;
    public Button[] materiWorld;
    public GameObject ButtonExtra;

    
    [Header("Panel Property")]
    public GameObject panelUnlock;
    public GameObject panelMateri;
    
    string[] materiTuguPetir;
    string[] materiTuguPeringatan;
    string[] materiKawasanTP;
    string[] HeaderbuttonKawasan;
    string[] HeaderbuttonTuguPetir;
    string[] HeaderbuttonTuguPeringatan;

    string[] materiKawasandanSumurMaut;
    string[] materiSerambi;
    string[] materiPosdanDapur;
    string[] materiSumurMaut;

    string[] HeaderbuttonKawasanSM;
    string[] HeaderbuttonSerambi;
    string[] HeaderbuttonPosdanDapur;
    string[] HeaderbuttonSumurMaut;
    


    [Header("Sprite Image")]
    public List<TSpriteList> spriteFile = new List<TSpriteList>();
    public SpriteRenderer spriteRenderer;
    private int currentIndex;
    // public SpriteRenderer spriteRenderer;


    [Header("Quest Player")]
    public QuestsList quests = new QuestsList();
    
    private TProcessDataContent data;
    private TGetContenQuis getData ;
    private string tempoLine;
    private List<string> FullParagraph = new List<string>();
    private string[] temporarySpace;

    // Start is called before the first frame update
    void Start()
    {
       data = new TProcessDataContent();
       getData = FindObjectOfType<TGetContenQuis>();
       
       data.GetCleanData();
       data.GetCleanDataLB();
       
       string[] raw = data.getRawData;
       string[] rawLB = data.getRawDataLB;
       
       // Materi LB
       HeaderbuttonKawasanSM = data.GetAllData(rawLB,"1+");
       materiKawasandanSumurMaut = data.GetAllData(rawLB,"1-");
       
       HeaderbuttonSerambi = data.GetAllData(rawLB,"2+");       
       materiSerambi = data.GetAllData(rawLB,"2-");

       HeaderbuttonPosdanDapur = data.GetAllData(rawLB,"3+");
       materiPosdanDapur = data.GetAllData(rawLB,"3-");
       
       HeaderbuttonSumurMaut = data.GetAllData(rawLB,"4+");
       materiSumurMaut = data.GetAllData(rawLB,"4-");
       
       // Materi TP
       HeaderbuttonKawasan = data.GetAllData(raw,"1+");
       materiKawasanTP = data.GetAllData(raw,"1-");

       HeaderbuttonTuguPetir = data.GetAllData(raw,"2+");
       materiTuguPetir = data.GetAllData(raw,"2-");

       HeaderbuttonTuguPeringatan =data.GetAllData(raw,"3+");       
       materiTuguPeringatan = data.GetAllData(raw,"3-");

       imageMaterial.sprite = defaultImage;

       SelectMateri(1);
        
       panelMateri.SetActive(false);
       panelUnlock.SetActive(true);    
    }

    void Update(){
        quests = InstanceTP.Instance.quests;
        
        if(quests.questList[0].isActive == false && quests.questList[0].isCompleted == false){
            panelMateri.SetActive(false);
            panelUnlock.SetActive(true);
            
            for (int i = 0; i < materiWorld.Count(); i++)
            {
                materiWorld[i].interactable = false;
            }    

        }else{
            panelMateri.SetActive(true);
            panelUnlock.SetActive(false);
            for (int i = 0; i < materiWorld.Count(); i++)
            {
                materiWorld[i].interactable = true;
            } 
        }
    }

    public void SelectMateri(int MateriSelected){

        switch (MateriSelected)
        {
            case 0:
                ButtonExtra.SetActive(true);
                // buttonMateri[0].IsActive = true;

                TextbuttonMateri[0].text =  HeaderbuttonKawasanSM[0];
                TextbuttonMateri[1].text =  HeaderbuttonSerambi[0];
                TextbuttonMateri[2].text =  HeaderbuttonPosdanDapur[0];
                TextbuttonMateri[3].text =  HeaderbuttonSumurMaut[0];

                buttonMateri[0].onClick.AddListener(MateriPancasilasakti);
                buttonMateri[1].onClick.AddListener(MateriSerambiPenyiksaan);
                buttonMateri[2].onClick.AddListener(MateriPosDanDapurUmum);
                buttonMateri[3].onClick.AddListener(LastmateriSumurMaut);
                

                break;
            case 1:
                ButtonExtra.SetActive(false);
                TextbuttonMateri[0].text =  HeaderbuttonKawasan[0];
                TextbuttonMateri[1].text =  HeaderbuttonTuguPetir[0];
                TextbuttonMateri[2].text =  HeaderbuttonTuguPeringatan[0];

                buttonMateri[0].onClick.AddListener(MateriKawasanTP);
                buttonMateri[1].onClick.AddListener(MateriTuguPetir);
                buttonMateri[2].onClick.AddListener(MateriTuguPeringatan);

                break;
            default:
                Debug.LogWarning("There's no other matrials!");
                break;
        }
    }

    public void StopRunningCoroutine(){
        StopAllCoroutines();
    }

    void MateriTuguPetir(){
        FullParagraph = new List<string>();
        temporarySpace = new string [] {};
        currentIndex = 0;
        StopAllCoroutines();

        if(quests.questList[1].isCompleted == false){
            imageMaterial.sprite = defaultImage;
            materialLine.text = "Maaf, Kamu harus menyelesaikan misi dulu untuk buka materi ini";
        }else{
            tempoLine = materiTuguPetir[0];
            imageMaterial.sprite = defaultImage;
            temporarySpace = getData.HelperDivideString(tempoLine,"//"); 

            // Debug.Log($"Sprite reference: {spriteFile[0].fileImage[0]}");
            StartCoroutine(ChangeImageBy(spriteFile[1].fileImage, 3));
            
            foreach(string line in temporarySpace){
                FullParagraph.Add(line);
            }

            materialLine.text = string.Join("\n\n", FullParagraph);
            
        }
    }

    void MateriTuguPeringatan(){
        FullParagraph = new List<string>();
        temporarySpace = new string [] {};
        currentIndex = 0;
        StopAllCoroutines();
        
        if(quests.questList[2].isCompleted == false){
            imageMaterial.sprite = defaultImage;
            materialLine.text = "Maaf, Kamu harus menyelesaikan misi dulu untuk buka materi ini";
        }else{
            tempoLine = materiTuguPeringatan[0];
            temporarySpace = getData.HelperDivideString(tempoLine,"//"); 
            StartCoroutine(ChangeImageBy(spriteFile[2].fileImage, 3));
            

            foreach(string line in temporarySpace){
                FullParagraph.Add(line);
            }
            
            materialLine.text = string.Join("\n\n", FullParagraph);
            

            // materialLine.text = materiSerambi[0];
        }
    }
    
    void MateriKawasanTP(){
        FullParagraph = new List<string>();
        temporarySpace = new string [] {};
        currentIndex = 0;
        StopAllCoroutines();
        
        
        if(quests.questList[0].isCompleted == false){
            imageMaterial.sprite = defaultImage;
            materialLine.text = "Maaf, Kamu harus menyelesaikan misi dulu untuk buka materi ini";
        }else{

            tempoLine = materiKawasanTP[0];
            temporarySpace = getData.HelperDivideString(tempoLine,"//"); 

            StartCoroutine(ChangeImageBy(spriteFile[0].fileImage, 3));

            foreach(string line in temporarySpace){
                FullParagraph.Add(line);
            }
            
            materialLine.text = string.Join("\n\n", FullParagraph);
        }
    }

    IEnumerator ChangeImageBy(Sprite[] image, int timeSeconds){
        imageMaterial.sprite = null;

        while(true){
            Debug.Log("ChangeImageBy coroutine Started");
            imageMaterial.sprite = image[currentIndex];
            yield return new WaitForSecondsRealtime(timeSeconds);
            Debug.Log($"Assigned sprite: {imageMaterial.sprite}");

            currentIndex = (currentIndex + 1) % image.Length;
        }

    }

    void MateriPancasilasakti(){
        FullParagraph = new List<string>();
        temporarySpace = new string [] {};
        currentIndex = 0;
        StopAllCoroutines();
        
        tempoLine = materiKawasandanSumurMaut[0];
        imageMaterial.sprite = defaultImage;
        temporarySpace = getData.HelperDivideString(tempoLine,"//"); 
        StartCoroutine(ChangeImageBy(spriteFile[6].fileImage, 3));

        // Debug.Log($"Sprite reference: {spriteFile[0].fileImage[1]}");
            
        foreach(string line in temporarySpace){
            FullParagraph.Add(line);
        }
        materialLine.text = string.Join("\n\n", FullParagraph);
    }

    void MateriSerambiPenyiksaan(){
        FullParagraph = new List<string>();
        temporarySpace = new string [] {};
        currentIndex = 0;
        StopAllCoroutines();
        
        tempoLine = materiSerambi[0];
        temporarySpace = getData.HelperDivideString(tempoLine,"//"); 
        StartCoroutine(ChangeImageBy(spriteFile[3].fileImage, 3));

        foreach(string line in temporarySpace){
            FullParagraph.Add(line);
        }    
        materialLine.text = string.Join("\n\n", FullParagraph);
    }
    
    void MateriPosDanDapurUmum(){
        FullParagraph = new List<string>();
        temporarySpace = new string [] {};
        currentIndex = 0;
        StopAllCoroutines();
        
        tempoLine = materiPosdanDapur[0];
        temporarySpace = getData.HelperDivideString(tempoLine,"//"); 
        StartCoroutine(ChangeImageBy(spriteFile[4].fileImage, 3));
        foreach(string line in temporarySpace){
            FullParagraph.Add(line);
        }    
        materialLine.text = string.Join("\n\n", FullParagraph);
    }

    void LastmateriSumurMaut(){
        Debug.Log("MateriSumurMaut");
        FullParagraph = new List<string>();
        StopAllCoroutines(); 
        StartCoroutine(ChangeImageBy(spriteFile[5].fileImage, 3));
        foreach(string line in materiSumurMaut){
            FullParagraph.Add(line);
        }
            materialLine.text = string.Join("\n", FullParagraph);
    }

}

