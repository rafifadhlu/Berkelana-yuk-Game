using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TPlayerInteract : MonoBehaviour
{

    [SerializeField] private Button buttonObject;
    public GameObject panelButtonEksplor;
    public GameObject canvasInstruction;
    
    public QuestsList quests = new QuestsList();

    
    void Start(){
        if (InstanceTP.Instance != null)
        {
            quests = InstanceTP.Instance.quests;
            // Use questDetails as needed
            Debug.Log("Quest Details in Player Interact: " + quests.nameWorld);
        }
    }

    void ShowsInstruction(){
        panelButtonEksplor.SetActive(false);
        canvasInstruction.SetActive(true);
    }

    public void ClosedShowsInstruction(){
        panelButtonEksplor.SetActive(true);
        canvasInstruction.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(GetInteractableObject() != null){
            ActiveButton();
        }else{
            nonActiveButton();
        }
    }

    private void ActiveButton(){
        buttonObject.interactable = true;
    }
    private void nonActiveButton(){
        buttonObject.interactable = false;
    }

    public void interactButton(){
        float interactRange = 2f;
        Collider[] colliderarray = Physics.OverlapSphere(transform.position, interactRange);
        foreach(Collider collider in colliderarray){
            if(collider.TryGetComponent(out TNPCInteract npcInteract)){
                string collidedObjectName = collider.gameObject.name;
                Debug.Log(collidedObjectName);
                switch (collidedObjectName)
                {
                     case "KuisTrigger":
                        Debug.Log("Kuis triggered");
                        npcInteract.InteractQuis();
                        break;
                    case "TuguSatuTahun":
                        Debug.Log("TuguSatuTahun triggered");
                        npcInteract.InteractSatuTahun();
                        break;
                    case "TuguPetir":
                        Debug.Log("TuguPetir Triggered");
                        npcInteract.InteractSPetir();
                        break;
                    case "KawasanTP":
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

    public TNPCInteract GetInteractableObject(){
        float interactRange = 2f;
        Collider[] colliderarray = Physics.OverlapSphere(transform.position, interactRange);
        foreach(Collider collider in colliderarray){
            if(collider.TryGetComponent(out TNPCInteract npcInteract)){
                return npcInteract;
            }
        }
        return null;
    }

}
