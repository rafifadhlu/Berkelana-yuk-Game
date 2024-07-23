// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class TesInstance : MonoBehaviour
// {
//     public static TesInstance Instance { get; private set; }
//     public QuestsList Quests;

//     private void Awake()
//     {
//         if (Instance == null)
//         {
//             Instance = this;
//             DontDestroyOnLoad(gameObject); // Optional: keeps the manager across scenes
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//     }
// }
