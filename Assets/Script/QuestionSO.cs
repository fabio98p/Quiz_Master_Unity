using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = " New Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2,6)][SerializeField]
    string question = "Enter new question text here";

    [SerializeField]
    List<string> answers = new List<string>();

    [SerializeField]
    int correctAnswerIndex;
    //public string getQuestion
    //{
    //    get { return question; }
    //}
    public string getQuestion()
    {
        return question;
    }
    public string getAnswer(int i) { 
        return answers[i]; 
    }
    public List<string> getAnswers()
    {
        return answers;
    }
    public int getCorrectAnswerIndex()
    {
        return correctAnswerIndex;
    }
    public string getCorrectAnswer()
    {
        return answers[correctAnswerIndex];
    }
}
