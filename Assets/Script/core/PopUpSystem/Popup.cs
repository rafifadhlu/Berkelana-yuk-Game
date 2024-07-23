using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Popup : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public TextMeshProUGUI message;

    public GameObject _ALERTPOPUP;
    public GameObject _COMPLETEDPOPUP;

    public void setPopupName(string text){
        Text.text = text;
    }

    public void setPopupMessage(string text){
        message.text = text;    
    }
}
