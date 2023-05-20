using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 30f;
    [SerializeField] float timeToShowCorrectAnswer = 10f;

    public bool loadNextQuestion = false;
    public QuestionState questionState = QuestionState.WaitResponse;
    
    float timerValue;
    public float fillFraction;

    private void Start()
    {
        timerValue = timeToCompleteQuestion;
    }
    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        if (timerValue < 0)
        {
            switch (questionState)
            {
                case QuestionState.WaitResponse:
                    questionState = QuestionState.WaitNextQuestion;
                    timerValue = timeToShowCorrectAnswer;
                    break;
                case QuestionState.WaitNextQuestion:
                    questionState = QuestionState.WaitResponse;
                    timerValue = timeToCompleteQuestion;
                    loadNextQuestion = true;
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (questionState)
            {
                case QuestionState.WaitResponse:
                    fillFraction = timerValue / timeToCompleteQuestion;
                    break;
                case QuestionState.WaitNextQuestion:
                    fillFraction = timerValue / timeToShowCorrectAnswer;
                    break;
                default:
                    break;
            }
        }
    }
    public enum QuestionState
    {
        WaitResponse,
        WaitNextQuestion,
    }
}
