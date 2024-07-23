using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    public TextMeshProUGUI nameCharacter;
    public TextMeshProUGUI messageToPlayer;

    // Update is called once per frame
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(string name, string[] dialogueSentence){
        nameCharacter.text = name;

        sentences.Clear();
        
        foreach(string sentence in dialogueSentence){
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence(){
        if(sentences.Count == 0){
            FinishDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        messageToPlayer.text = sentence;
    }

    void FinishDialogue(){
        Debug.Log("End Conversation");
        FindObjectOfType<NPCInteract>().closeDialogue();
    }
}
