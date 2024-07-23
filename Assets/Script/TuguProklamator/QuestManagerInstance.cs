using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManagerInstance : MonoBehaviour
{
    public static QuestManagerInstance Instance { get; private set; }
    public QuestsList quests = new QuestsList();

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
