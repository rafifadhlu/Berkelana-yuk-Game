using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AsyncLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private Slider loadingSlider;
    [SerializeField]

    private AudioToggle audioToggle;

    void Awake(){
        audioToggle = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioToggle>();
    }

    public void loadWorldButton(string scenename){
        audioToggle.PlaySFX(audioToggle.BUTTONCLICKED);
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);
        StartCoroutine(GoesToWorldAsync(scenename));
    }

    IEnumerator GoesToWorldAsync(string scenename){
        AsyncOperation loadOperation =  SceneManager.LoadSceneAsync(scenename);

        while(!loadOperation.isDone){
            float progressValue = Mathf.Clamp01(loadOperation.progress/0.9f);
            loadingSlider.value =  progressValue;
            yield return null;
        }
    }
    
    // public void GoesToGame(string scenename){
    //     audioToggle.PlaySFX(audioToggle.BUTTONCLICKED);
    //     SceneManager.LoadScene(scenename);
    // }
}
