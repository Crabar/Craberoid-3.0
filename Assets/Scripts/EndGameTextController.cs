using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Utils.Extensions;

public class EndGameTextController : MonoBehaviour
{
    public async Task ShowWin(int totalScore)
    {
        SetText($"You win!\nYour score is {totalScore}.");
        await GetComponent<Animator>().PlayAsync("EndGameTextAnimation");
    }

    public async Task ShowLose(int totalScore)
    {
        SetText($"Game over!\nYour score is {totalScore}.");
        await GetComponent<Animator>().PlayAsync("EndGameTextAnimation");
    }

    private void SetText(string newText)
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = newText;
    }
}
