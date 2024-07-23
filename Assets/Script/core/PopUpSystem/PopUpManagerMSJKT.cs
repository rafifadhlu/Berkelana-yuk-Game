using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManagerMSJKT : MonoBehaviour
{
    public GameObject popupAlertGoPrevous;
    public GameObject popupAlertMissionCompleted;
    public GameObject popupCompleted;
    public GameObject canvasPopup;
    private GameObject createdPopUpObject;



    void Start(){
        
    }

    public void setPopup(string type){

        switch (type)
        {
            case "AlertGoPrevous" :
                createdPopUpObject = Instantiate(popupAlertGoPrevous,canvasPopup.transform);
                break;
            
            case "alertCompleted" :
                createdPopUpObject = Instantiate(popupAlertMissionCompleted,canvasPopup.transform);
                break;
            case "missionCompleted" :
                createdPopUpObject = Instantiate(popupCompleted,canvasPopup.transform);
                break;
            default:
            Debug.Log("No function");
            break;
        }
        MovePopup(true);
    }

    public void MovePopup(bool move){
        StartCoroutine(FadeImage(move));
    }

     IEnumerator FadeImage(bool fadeAway)
    {
        RectTransform rectTransform = createdPopUpObject.GetComponent<RectTransform>();
        CanvasGroup canvasGroup = createdPopUpObject.GetComponent<CanvasGroup>();

        if (canvasGroup == null) {
            canvasGroup = createdPopUpObject.AddComponent<CanvasGroup>();
        }

        // Set the initial position of the popup new Vector2(200, 36)
        // rectTransform.position =  new Vector2(650, 636);
        yield return new WaitForSeconds(1);

        // Fade out the popup
        if (fadeAway)
        {
            for (float i = 1f; i >= 0; i -= Time.deltaTime)
            {
                if (createdPopUpObject != null)
                {
                    canvasGroup.alpha = i;
                }
                yield return null;
            }
            Destroy(createdPopUpObject);
        }

        // Check if the createdPopUpObject still exists before destroying it
        if (createdPopUpObject != null)
        {
            Destroy(createdPopUpObject);
        }
    }



}
