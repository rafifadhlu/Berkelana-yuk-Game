using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    //properti UI Game
    [SerializeField] private Button buttonObject;
    public GameObject panelButtonEksplor;
    public GameObject canvasInstruction;
    
    //properti Data Game
    public QuestsList quests;

    //method ini dipanggil sesaat frame pertama kali dijalankan    
    void Start(){
        // Mengambil data game dari properti Instance 
        if (QuestManager.Instance != null)
        {
            quests = QuestManager.Instance.quests;
            // Use questDetails as needed
            Debug.Log("Quest Details in Player Interact: " + quests.nameWorld);
        }
    }


    //method menampilkan tampilan intruksi
    void ShowsInstruction(){
        panelButtonEksplor.SetActive(false);
        canvasInstruction.SetActive(true);
    }

    //method menonaktifkan tampilan intruksi
    public void ClosedShowsInstruction(){
        panelButtonEksplor.SetActive(true);
        canvasInstruction.SetActive(false);
    }


    //method ini dipanggil setiap frame berjalan e.g 30 fps (frame per second), 
    //maka method ini berjalan 30 kali setiap detik
    void Update()
    {
        //fungsi untuk tombol interaksi. Jika player tidak berada di dekat object maka tombol interaksi akan nonaktif
        if(GetInteractableObject() != null){
            ActiveButton();
        }else{
            nonActiveButton();
        }
    }

    //Mengaktifkan tombol
    private void ActiveButton(){
        buttonObject.interactable = true;
    }

    //Menonaktifkan tombol
    private void nonActiveButton(){
        buttonObject.interactable = false;
    }
    
    //Method untuk mendeteksi interaksi object
    public void interactButton(){
        float interactRange = 2f;
        // properti ini akan mendeteksi player saat player mendekati object
        Collider[] colliderarray = Physics.OverlapSphere(transform.position, interactRange);
        
        // Object yang terdeteksi akan di return untuk di teruskan ke script NPC 
        foreach(Collider collider in colliderarray){
            if(collider.TryGetComponent(out NPCInteract npcInteract)){
                string collidedObjectName = collider.gameObject.name;
                Debug.Log(collidedObjectName);
                
                //Object yang terdeteksi akan di cek dengan misi yang tersedia
                switch (collidedObjectName)
                {
                     case "KuisTrigger":
                        Debug.Log("Pos Dan Dapur triggered");
                        npcInteract.InteractQuis();
                        break;
                    case "SumurMaut":
                        Debug.Log("Pos Dan Dapur triggered");
                        npcInteract.InteractSumurMaut();
                        break;
                    case "PosdanDapur":
                        Debug.Log("Pos Dan Dapur triggered");
                        npcInteract.InteractPosDanDapur();
                        break;
                    case "SerambiPenyiksaan":
                        Debug.Log("Serambi Triggered");
                        npcInteract.InteractSerambi();
                        break;
                    case "GateKawasanL":
                        npcInteract.InteractLearnKawasan();
                        break;
                    case "NPCEbi":
                        npcInteract.Interact();
                        break;
                    default:
                        Debug.Log("There's no funct");  
                        break;
                }
            }
        }
    }

    //Method untuk mendeteksi interaksi object dan return by name
    public NPCInteract GetInteractableObject(){
        float interactRange = 2f;
        Collider[] colliderarray = Physics.OverlapSphere(transform.position, interactRange);
        foreach(Collider collider in colliderarray){
            if(collider.TryGetComponent(out NPCInteract npcInteract)){
                return npcInteract;
            }
        }
        return null;
    }

}
