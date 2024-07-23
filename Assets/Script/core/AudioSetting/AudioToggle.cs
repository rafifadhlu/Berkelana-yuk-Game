using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioToggle : MonoBehaviour
{
    [Header("----Icon Sound----")]
    public bool isActive;
    public Sprite[] spriteSound;
    public Image buttonImage;

    [Header("----Audio Source----")]
    public AudioSource placeToStartBGM;
    public AudioSource placeToStartSFX;

    [Header("----Audio Clip----")]
    public AudioClip BGMAUDIO;
    public AudioClip BUTTONCLICKED;

    
    [Header("----Volume----")]
    public float valueVolume;

    // Start is called before the first frame update
    void Start()
    {
        placeToStartBGM.volume = AudioInstance.Instance.volumeValue;
        placeToStartBGM.clip = BGMAUDIO;
        placeToStartBGM.Play();
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

    public void PlaySFX(AudioClip audio){
        placeToStartSFX.PlayOneShot(audio);
    }

    public void OnValueChanged(float value){
        valueVolume = value;
        AudioInstance.Instance.volumeValue = value;
        placeToStartBGM.volume = value;
    }
}
