using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManagerLB : MonoBehaviour
{
    [Header("----Icon Sound----")]
    public bool isActive;
    public Sprite[] spriteSound;
    public Image buttonImage;

    [Header("----Audio Source----")]
    public AudioSource placeToStartBGM;
    public AudioSource placeToStartSFX;
    public AudioSource placeToStartAudioText;
    public AudioSource placeToStartSFXWalking;
    public AudioSource placeToStartSFXRunning;

    [Header("----Audio Clip----")]
    public AudioClip BGMAUDIO;
    public AudioClip BUTTONCLICKED;
    public AudioClip CHARACTERWALK;
    public AudioClip CHARACTERRUN;
    public AudioClip WARNING;
    public AudioClip COMPLETEDPART;

    
    [Header("----Volume----")]
    public float valueVolume;

    void Awake(){
        placeToStartBGM.volume = AudioInstance.Instance.volumeValue;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(!AudioInstance.Instance.isSoundActive){
            isActive = false;
            placeToStartBGM.Stop();
            buttonImage.sprite = spriteSound[1];
        }else{
            isActive = true;
            placeToStartBGM.clip = BGMAUDIO;
            placeToStartBGM.Play();
            buttonImage.sprite = spriteSound[0];   
        }
    }

    // Update is called once per frame
    void Update()
    {
        isActive = AudioInstance.Instance.isSoundActive;
        valueVolume = AudioInstance.Instance.volumeValue;
        placeToStartBGM.volume = AudioInstance.Instance.volumeValue;
    }

    public void activatingSound(){
        if(isActive == false){
            isActive = true;
            placeToStartBGM.clip = BGMAUDIO;
            placeToStartBGM.Play();
            buttonImage.sprite = spriteSound[0];
            AudioInstance.Instance.isSoundActive = isActive;
        }else{
            isActive = false;
            placeToStartBGM.Stop();
            buttonImage.sprite = spriteSound[1];
            AudioInstance.Instance.isSoundActive = isActive;
        }
        
    }

    public void PlayWalkEffect(AudioClip audio){
        placeToStartSFXWalking.clip = audio;
        placeToStartSFXWalking.Play();
    }

    public void StopWalkEffect(){
        placeToStartSFXWalking.Stop();
    }

    public void PlayRunEffect(AudioClip audio){
        placeToStartSFXRunning.clip = audio;
        placeToStartSFXRunning.Play();
    }

    public void StopRunEffect(){
        placeToStartSFXRunning.Stop();
    }

    public void PlaySFX(AudioClip audio){
        placeToStartSFX.PlayOneShot(audio);
    }

    public void PlayAudioText(AudioClip audio){
        placeToStartAudioText.PlayOneShot(audio);
    }

    public void OnValueChanged(float value){
        valueVolume = value;
        AudioInstance.Instance.volumeValue = value;
        placeToStartBGM.volume = value;
    }
}
