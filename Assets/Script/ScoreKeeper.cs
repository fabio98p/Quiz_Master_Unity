using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{


    public int correctAnswers { get; set; }
    public int questionSeen { get; set; }

    private void Start()
    {
        correctAnswers = 0;
        questionSeen = 0;
    }

    public void IncrementCorrectAnswers()
    {
        correctAnswers++;
    }
    public void IncrementQuestionsSeen()
    {
        questionSeen++;
    }

    public int CalculateScore()
    {
        return Mathf.RoundToInt(correctAnswers / (float)questionSeen * 100);
    }
}
