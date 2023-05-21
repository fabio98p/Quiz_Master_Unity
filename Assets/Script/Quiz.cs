using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtoms;
    int correctAnswerIndex;
    bool hasAnsweredEarly = true;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;

    public bool isComplete;

    private void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }
    void Start()
    {
        GetNextQuestion();
    }

    private void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion == true)
        {
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && timer.questionState == Timer.QuestionState.WaitNextQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }
    public void OnAnswerSelected(int i)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(i);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = $"Score :{scoreKeeper.CalculateScore()}%";

        if (progressBar.value == progressBar.maxValue)
        {
            isComplete = true;
        }
    }

    void DisplayAnswer(int i)
    {
        Image buttonImage;
        if (currentQuestion.getCorrectAnswerIndex() == i)
        {
            questionText.text = "Correct";
            buttonImage = answerButtoms[i].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            string responseText = $"Sorry the right answer was:\n {currentQuestion.getCorrectAnswer()}";
            questionText.text = responseText;
            buttonImage = answerButtoms[currentQuestion.getCorrectAnswerIndex()].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprite();
            GetRandomQuerstion();
            DisplayQuestion();
            progressBar.value++;
            scoreKeeper.IncrementQuestionsSeen();
        }
    }

    void GetRandomQuerstion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
    }
    void DisplayQuestion()
    {
        //correctAnswerIndex = question.getCorrectAnswerIndex();
        questionText.text = currentQuestion.getQuestion();

        for (int i = 0; i < answerButtoms.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtoms[i].GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault();
            buttonText.text = currentQuestion.getAnswer(i);

        }
    }

    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtoms.Length; i++)
        {
            Button button = answerButtoms[i].GetComponent<Button>();
            button.interactable = state;
        }
    }
    void SetDefaultButtonSprite()
    {
        foreach (var answer in answerButtoms)
        {
            answer.GetComponent<Image>().sprite = defaultAnswerSprite;
        }
    }
}
