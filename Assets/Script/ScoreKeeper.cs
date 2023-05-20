using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    //int correctAnswers = 0;
    //int questionSeen = 0;

    public int correctAnswers { get; set; }
    public int questionSeen { get; set; }
    //public int correctAnswers
    //{
    //    get { return correctAnswers; }
    //    set { correctAnswers = value; }
    //}

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
