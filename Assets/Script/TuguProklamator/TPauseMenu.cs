using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TPauseMenu : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject panelButtonEksplor;
    public GameObject panelDialogue;
    public GameObject panelMenuBaca;
    public GameObject pauseButton;
    public GameObject panelFinish;
     public GameObject panelSetting;
     public GameObject CanvasGameButton;

    public AudioManagerTP audioManager;
    

    void Awake(){
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerTP>();
    }


    void Start(){
        Time.timeScale = 1;
        panelButtonEksplor.SetActive(true);
        CanvasGameButton.SetActive(true);

        panelDialogue.SetActive(false);
        pauseButton.SetActive(true);
        panelMenuBaca.SetActive(false);
        PausePanel.SetActive(false);
        panelFinish.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame(){
        audioManager.PlaySFX(audioManager.BUTTONCLICKED);
        CanvasGameButton.SetActive(false);

        PausePanel.SetActive(true);
        PausePanel.transform.GetChild(0).gameObject.SetActive(true);
        panelSetting.SetActive(false);

        Time.timeScale = 0;
        Debug.Log("Pause Pressed");
    }

    public void GameEnded(){
        audioManager.PlaySFX(audioManager.BUTTONCLICKED);
        CanvasGameButton.SetActive(false);

        panelFinish.SetActive(true);
        panelButtonEksplor.SetActive(false);
        Time.timeScale = 0;
    }

    public void ContinousGame(){
        audioManager.PlaySFX(audioManager.BUTTONCLICKED);
        CanvasGameButton.SetActive(true);

        PausePanel.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale=1;
    }

    public void WhereDoWeGo(string scenename){
        audioManager.PlaySFX(audioManager.BUTTONCLICKED);
        SceneManager.LoadScene(scenename);
    }

    public void GoesToMenuBaca(){
        audioManager.PlaySFX(audioManager.BUTTONCLICKED);

        panelButtonEksplor.SetActive(false);
        panelMenuBaca.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0;
    }

    public void GoesToSetting(){
        audioManager.PlaySFX(audioManager.BUTTONCLICKED);
        CanvasGameButton.SetActive(false);

        PausePanel.SetActive(true);
        PausePanel.transform.GetChild(0).gameObject.SetActive(false);
        panelSetting.SetActive(true);

        Time.timeScale = 0;
    }

    public void BackToResume(){
        audioManager.PlaySFX(audioManager.BUTTONCLICKED);
        PausePanel.transform.GetChild(0).gameObject.SetActive(true);
        panelSetting.SetActive(false);
    }

    public void MenuToMainMenu(){
        audioManager.PlaySFX(audioManager.BUTTONCLICKED);
        panelButtonEksplor.SetActive(true);
        panelMenuBaca.SetActive(false);
        pauseButton.SetActive(true);
        FindObjectOfType<TmenuBaca>().StopRunningCoroutine();
        Time.timeScale=1;
    }

}
