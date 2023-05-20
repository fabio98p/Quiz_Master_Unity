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
    [SerializeField] QuestionSO question;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtoms;
    int correctAnswerIndex;
    bool hasAnsweredEarly;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;



    void Start()
    {
        timer = FindObjectOfType<Timer>();
        GetNextQuestion();
    }

    private void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion == true)
        {
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
    }

    void DisplayAnswer(int i)
    {
        Image buttonImage;
        if (question.getCorrectAnswerIndex() == i)
        {
            questionText.text = "Correct";
            buttonImage = answerButtoms[i].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
        else
        {
            string responseText = $"Sorry the right answer was:\n {question.getCorrectAnswer()}";
            questionText.text = responseText;
            buttonImage = answerButtoms[question.getCorrectAnswerIndex()].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    void GetNextQuestion()
    {
        SetButtonState(true);
        SetDefaultButtonSprite();
        DisplayQuestion();
    }

    void DisplayQuestion()
    {
        //correctAnswerIndex = question.getCorrectAnswerIndex();
        questionText.text = question.getQuestion();

        for (int i = 0; i < answerButtoms.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtoms[i].GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault();
            buttonText.text = question.getAnswer(i);

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
