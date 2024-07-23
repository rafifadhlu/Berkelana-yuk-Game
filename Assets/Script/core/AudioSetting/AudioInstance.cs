using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInstance : MonoBehaviour
{
    public static AudioInstance Instance {get; private set;}

    public  bool isSoundActive;
    public float volumeValue; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: keeps the manager across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
