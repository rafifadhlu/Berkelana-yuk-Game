using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class menuBaca : MonoBehaviour
{

    [Header("Material Attribute")]
    public TextMeshProUGUI materialLine;
    public Image imageMaterial;
    public Sprite defaultImage;

    
    [Header("Panel Property")]
    public GameObject panelUnlock;
    public GameObject panelMateri;
    
    public string[] materiKawasandanSumurMaut;
    public string[] materiSerambi;
    public string[] materiPosdanDapur;
    public string[] materiSumurMaut;

    public string[] HeaderbuttonKawasanSM;
    public string[] HeaderbuttonSerambi;
    public string[] HeaderbuttonPosdanDapur;
    public string[] HeaderbuttonSumurMaut;
    

    [Header("Sprite Image")]
    public List<SpriteList> spriteFile = new List<SpriteList>();
    public SpriteRenderer spriteRenderer;
    private int currentIndex;
    // public SpriteRenderer spriteRenderer;


    [Header("Quest Player")]
    public QuestsList quests;
    private ProcessDataContent data = new ProcessDataContent();
    private Button button;
    private GetContenQuis getData;
    private string tempoLine;
    private List<string> FullParagraph = new List<string>();
    private string[] temporarySpace;

    // Start is called before the first frame update
    void Start()
    {
        getData = FindObjectOfType<GetContenQuis>();

       data.GetCleanData();
       
       string[] raw = data.getRawData;

       HeaderbuttonKawasanSM = data.GetAllData(raw,"1+");
       materiKawasandanSumurMaut = data.GetAllData(raw,"1-");
       
       HeaderbuttonSerambi = data.GetAllData(raw,"2+");       
       materiSerambi = data.GetAllData(raw,"2-");

       HeaderbuttonPosdanDapur = data.GetAllData(raw,"3+");
       materiPosdanDapur = data.GetAllData(raw,"3-");
       
       HeaderbuttonSumurMaut = data.GetAllData(raw,"4+");
       materiSumurMaut = data.GetAllData(raw,"4-");

       button = GetComponent<Button>();
       imageMaterial.sprite = defaultImage;
       
       panelMateri.SetActive(false);
       panelUnlock.SetActive(true);   
    }

     public void StopRunningCoroutine(){
        StopAllCoroutines();
    }

    void Update(){
        quests = QuestManager.Instance.quests;
        
        if(quests.questList[0].isActive == false && quests.questList[0].isCompleted == false){
            panelMateri.SetActive(false);
            panelUnlock.SetActive(true);    
        }else{
            panelMateri.SetActive(true);
            panelUnlock.SetActive(false); 
        }
    }

    public void MateriPancasilasakti(){
        FullParagraph = new List<string>();
        temporarySpace = new string [] {};
        currentIndex = 0;
        StopAllCoroutines();

        if(quests.questList[0].isCompleted == false){
            imageMaterial.sprite = defaultImage;
            materialLine.text = "Maaf, Kamu harus menyelesaikan misi dulu untuk buka materi ini";
        }else{
            tempoLine = materiKawasandanSumurMaut[0];
            imageMaterial.sprite = defaultImage;
            temporarySpace = getData.HelperDivideString(tempoLine,"//"); 
            StartCoroutine(ChangeImageBy(spriteFile[3].fileImage, 3));

            Debug.Log($"Sprite reference: {spriteFile[0].fileImage[1]}");
            
            foreach(string line in temporarySpace){
                FullParagraph.Add(line);
            }
            materialLine.text = string.Join("\n\n", FullParagraph);
            
        }
    }

    public void MateriSerambiPenyiksaan(){
        FullParagraph = new List<string>();
        temporarySpace = new string [] {};
        currentIndex = 0;
        StopAllCoroutines();
        
        if(quests.questList[1].isCompleted == false){
            imageMaterial.sprite = defaultImage;
            materialLine.text = "Maaf, Kamu harus menyelesaikan misi dulu untuk buka materi ini";
        }else{
            tempoLine = materiSerambi[0];
            temporarySpace = getData.HelperDivideString(tempoLine,"//"); 
            StartCoroutine(ChangeImageBy(spriteFile[0].fileImage, 3));
            

            foreach(string line in temporarySpace){
                FullParagraph.Add(line);
            }
            
            materialLine.text = string.Join("\n\n", FullParagraph);
            

            // materialLine.text = materiSerambi[0];
        }
    }
    
    public void MateriPosDanDapurUmum(){
        FullParagraph = new List<string>();
        temporarySpace = new string [] {};
        currentIndex = 0;
        StopAllCoroutines();
        
        
        if(quests.questList[2].isCompleted == false){
            imageMaterial.sprite = defaultImage;
            materialLine.text = "Maaf, Kamu harus menyelesaikan misi dulu untuk buka materi ini";
        }else{

            tempoLine = materiPosdanDapur[0];
            temporarySpace = getData.HelperDivideString(tempoLine,"//"); 

            StartCoroutine(ChangeImageBy(spriteFile[1].fileImage, 3));

            foreach(string line in temporarySpace){
                FullParagraph.Add(line);
            }
            
            materialLine.text = string.Join("\n\n", FullParagraph);
        }
    }

    public void LastmateriSumurMaut(){
        Debug.Log("MateriSumurMaut");
        FullParagraph = new List<string>();
        StopAllCoroutines();

        if(quests.questList[3].isCompleted == false){
            imageMaterial.sprite = defaultImage;
            materialLine.text = "Maaf, Kamu harus menyelesaikan misi dulu untuk buka materi ini";
        }else{
            StartCoroutine(ChangeImageBy(spriteFile[2].fileImage, 3));

            foreach(string line in materiSumurMaut){
                FullParagraph.Add(line);
            }
            materialLine.text = string.Join("\n", FullParagraph);
            
            // materialLine.text = materiPosdanDapur[0];
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

   
}


// [System.Serializable]
// public class spriteList
// {
//     public string whoseIsSprite;
//     public Sprite[] fileImage;

// }
