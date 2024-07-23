using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MPlayerInteract : MonoBehaviour
{

    [SerializeField] private Button buttonObject;
    public GameObject panelButtonEksplor;
    public GameObject canvasInstruction;
    
    public QuestsList quests;

    
    void Start(){
        if (MSJKTInstance.Instance != null)
        {
            quests = MSJKTInstance.Instance.quests;
            // Use questDetails as needed
            Debug.Log("Quest Details in Player Interact: " + quests.nameWorld);
        }
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
            if(collider.TryGetComponent(out MNPCInteract npcInteract)){
                string collidedObjectName = collider.gameObject.name;
                Debug.Log(collidedObjectName);
                switch (collidedObjectName)
                {
                     case "KuisTrigger":
                        Debug.Log("Kuis triggered");
                        npcInteract.InteractQuis();
                        break;
                    case "IsiMuseum":
                        Debug.Log("TuguSatuTahun triggered");
                        npcInteract.InteractIsiMuseum();
                        break;
                    case "SejarahBangunanMuseum":
                        Debug.Log("TuguPetir Triggered");
                        npcInteract.InteractSejarahBangunan();
                        break;
                    case "NPCSifa":
                        npcInteract.Interact();
                        break;
                    default:
                        Debug.Log("There's no funct");  
                        break;
                }
            }
        }
    }

    public MNPCInteract GetInteractableObject(){
        float interactRange = 2f;
        Collider[] colliderarray = Physics.OverlapSphere(transform.position, interactRange);
        foreach(Collider collider in colliderarray){
            if(collider.TryGetComponent(out MNPCInteract npcInteract)){
                return npcInteract;
            }
        }
        return null;
    }

}
