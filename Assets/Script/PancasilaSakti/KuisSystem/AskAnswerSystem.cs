using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AskAnswerSystem : MonoBehaviour
{
    //Properti untuk membuat list pertanyaan 
    public List<Question> listQuestions;
    
    //properti display pertanyaan dan jawaban
    [Header("Display Questions Property")]
    public TextMeshProUGUI textQuestion;
    public TextMeshProUGUI[] answers;
    public GameObject panelQuestions;
    
    //properti helper untuk kuis 
    private NPCInteract triggerFinishStage;
    private bool answersCorrect = false;
    private int correct;
    private int currentIndex;
    private int limitQuestionsToDisplay = 2;
    public bool isStageClear;
    

    //method ini dipanggil sesaat frame pertama kali dijalankan    
    void Start()
    {   
        panelQuestions.SetActive(false);
        triggerFinishStage = FindObjectOfType<NPCInteract>();
    }

    //method untuk memulai kuis
    public void quizStart(){
        panelQuestions.SetActive(true);
        //lakukan pengacakan soal dengan algoritma fisher yates
        FisherAlgorithm();
        //set soal pertama dan tampilkan pertanyaan serta jawaban
        currentIndex = 0;
        displayQuestions();
    }

    //method ini berfungsi untuk menampilkan pertanyaan  
    void displayQuestions(){
        //buat batas agar soal yang keluar hanya as many as the limit of number of questions
        if (currentIndex < listQuestions.Count)
            {
                //Ambil pertanyaan sesuai dengan index saat ini
                Question currentQuestion = listQuestions[currentIndex];
                Debug.Log(currentQuestion.aquestions.question);
                //Tampilkan pertanyaan ke screen player
                generateQuestion(currentQuestion.aquestions.question, currentQuestion.aquestions.answers);
                
                //Set the correct answers ke dalam properti correct untuk pengecekan jawaban
                correct = currentQuestion.aquestions.correctAnswer;
            }
    }

    // Method untuk menampilkan pertanyaan ke player
    void generateQuestion(string question, string[]listAnswers){

        textQuestion.text = question;

        for (int i = 0; i < listAnswers.Length; i++)
        {
            answers[i].text = listAnswers[i];
        }
    }
    //Method ini di attach ke button untuk mengirim jawaban terpilih dari player
    public void Opsi(int jawaban)
    {
       Debug.Log(jawaban);
       //set jawaban ke method buttonAnswer untuk melihat hasil dari jawaban yang sudah di kirim       
       ButtonAnswer(jawaban);
       
       //jika jawaban benar maka melanjutkan prosedur selanjutnya
       if (answersCorrect)
        {
            // jika TRUE maka currentIndex bertambah
            currentIndex++;
            // dilakukan pengecekan apakah jumlah soal <=2 (start from 0) = true
            // jika true maka tampilkan soal kembali
            if (currentIndex <= limitQuestionsToDisplay)
            {
                displayQuestions();
            }
            // else akan berjalan apabila player berhasil menjawab seluruh pertanyaan dengan benar
            else
            {
                isStageClear = true;
                Debug.Log("Quiz Completed Successfully");
                triggerFinishStage.PlayerCompletedOneofMission(4);
                panelQuestions.SetActive(false);
            }
        }
        //jika salah maka fungsi akan di terminated saat itu juga
        else
        {
            isStageClear = false;
            panelQuestions.SetActive(false);
            Debug.Log("Quiz Stopped due to Incorrect Answer");
        }

    }

    //method pengecekan jawaban player
    public void ButtonAnswer(int jawabanPlayer){

        if(jawabanPlayer == correct){
            answersCorrect = true;
        }else{
            Debug.Log("False Answer");
            answersCorrect = false;
        }
    }

    //algoritma fisher yates
    void FisherAlgorithm()
    {
        int n = listQuestions.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Question temp = listQuestions[i];
            listQuestions[i] = listQuestions[j];
            listQuestions[j] = temp;
        }
    }

}


//pembuatan list pertanyaan agar dapat terlihat di inspector.
[System.Serializable]
public class Question{

    [System.Serializable]
    public class aQuestion{
        public string question;
        public string[] answers;
        public int correctAnswer;
    }
    public aQuestion aquestions;
}