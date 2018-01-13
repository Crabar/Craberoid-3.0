using System.Collections;
using System.Collections.Generic;
using Signals;
using TMPro;
using UnityEngine;
using Zenject;

public class ScoreTextController : MonoBehaviour
{
    public void UpdateScoreText(int newScore)
    {
        GetComponent<TextMeshProUGUI>().text = $"Score: {newScore}";
    }
}
