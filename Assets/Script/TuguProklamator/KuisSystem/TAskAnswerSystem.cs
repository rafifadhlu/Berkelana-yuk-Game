using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TAskAnswerSystem : MonoBehaviour
{
    //Properti untuk membuat list pertanyaan dan pertanyaan soal sebelumnya
    public List<TQuestion> listQuestions;
    public TQuestion PreviouslyWorldQuestions;
    
    //properti display pertanyaan dan jawaban
    [Header("Display Questions Property")]
    public TextMeshProUGUI textQuestion;
    public TextMeshProUGUI[] answers;
    public GameObject panelQuestions;

    //properti helper untuk kuis 
    private TNPCInteract triggerFinishStage;
    private bool answersCorrect = false;
    private int correct;
    
    private int currentIndexQuestions;
    private int indexGeneral;
    private int limitQuestionsToDisplay = 3;
    public bool isStageClear;
    

    //method ini dipanggil sesaat frame pertama kali dijalankan    
    void Start()
    {   
        panelQuestions.SetActive(false);
        triggerFinishStage = FindObjectOfType<TNPCInteract>();
    }

    //method untuk memulai kuis
    //pada method ini terdapat dua index yang digunakan yaitu untuk semua pertanyaan
    //dan untuk pertanyaan world kedua
    public void quizStart(){
        panelQuestions.SetActive(true);
        //acak soal khusus taman proklamator
        FisherAlgorithm();
        
        // set index default = 0 dan tampilkan pertanyaan
        currentIndexQuestions = 0;
        indexGeneral = 0;
        displayQuestions();
    }

    //method untuk menampilkan pertanyaan
    void displayQuestions(){
        //set total pertanyaan
        int questionsLength = listQuestions.Count + 1;
        
        //index general default = 0 sehingga player melakukan kuis diawali dengan pertanyaan world pertama
        //jika player berhasil menjawab soal pertama maka index general akan bertambah
            if(indexGeneral == 0){
                //generate soal dengan topik pertanyaan pada world pertama
                generateQuestion(PreviouslyWorldQuestions.aquestions.question, PreviouslyWorldQuestions.aquestions.answers);
                correct = PreviouslyWorldQuestions.aquestions.correctAnswer;
            }else{
                //set index agar tidak keluar batas array soal
                if (indexGeneral < questionsLength)
                {
                    //set a question dengan index khusus soal taman proklamator 
                    TQuestion currentQuestion = listQuestions[currentIndexQuestions];
                    generateQuestion(currentQuestion.aquestions.question, currentQuestion.aquestions.answers);
                    correct = currentQuestion.aquestions.correctAnswer;
                    //index bertambah untuk mengganti soal
                    currentIndexQuestions++;
                }
            }
       
        }
    
    // Method untuk menampilkan pertanyaan ke player
    void generateQuestion(string question, string[] listAnswers){

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
            // jika TRUE maka indexGeneral untuk pertanyaan yang sedang berjalan bertambah
            indexGeneral++;
            // dilakukan pengecekan apakah jumlah soal <=3 (start from 0,1,2,3) = true
            // jika true maka tampilkan soal kembali
            if (indexGeneral <= limitQuestionsToDisplay)
            {
                displayQuestions();
            }
            // else akan berjalan apabila player berhasil menjawab seluruh pertanyaan dengan benar
            else
            {
                isStageClear = true;
                Debug.Log("Quiz Completed Successfully");
                triggerFinishStage.PlayerCompletedOneofMission(3);
                panelQuestions.SetActive(false);
            }
        }
        //jika salah maka fungsi akan di terminated saat itu juga
        else
        {
            isStageClear = false;
            panelQuestions.SetActive(false);
            currentIndexQuestions = 0;
            indexGeneral = 0;
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
        for (int i = n - 1; i > 1; i--)
        {
            int j = Random.Range(0, i + 1);
            TQuestion temp = listQuestions[i];
            listQuestions[i] = listQuestions[j];
            listQuestions[j] = temp;
        }
    }

    }


//pembuatan list pertanyaan agar dapat terlihat di inspector.
[System.Serializable]
public class TQuestion{

    [System.Serializable]
    public class aQuestion{
        public string question;
        public string[] answers;
        public int correctAnswer;
    }
    public aQuestion aquestions;
}