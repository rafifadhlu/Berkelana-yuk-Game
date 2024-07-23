using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSJKTInstance : MonoBehaviour
{
    public static MSJKTInstance Instance { get; private set; }

    public QuestsList quests;

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
