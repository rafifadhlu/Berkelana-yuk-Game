using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManagerMSJKT : MonoBehaviour
{
    public static QuestManagerMSJKT Instance { get; private set; }
    public MQuestsList quests;

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
