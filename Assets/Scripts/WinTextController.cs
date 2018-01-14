using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinTextController : MonoBehaviour
{
    public void ShowWin(int totalScore)
    {
        GetComponent<TextMeshProUGUI>().text = $"You win!\nYour score is {totalScore}.";
        GetComponent<Animator>().Play("WinTextAnimation");
    }

    public void ShowLose(int totalScore)
    {
        GetComponent<TextMeshProUGUI>().text = $"Game over!\nYour score is {totalScore}.";
        GetComponent<Animator>().Play("WinTextAnimation");
    }
}