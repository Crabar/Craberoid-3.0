using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameTextController : MonoBehaviour
{
    public void ShowWin(int totalScore)
    {
        SetText($"You win!\nYour score is {totalScore}.");
        GetComponent<Animator>().Play("EndGameTextAnimation");
    }

    public void ShowLose(int totalScore)
    {
        SetText($"Game over!\nYour score is {totalScore}.");
        GetComponent<Animator>().Play("EndGameTextAnimation");
    }

    private void SetText(string newText)
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = newText;
    }
}