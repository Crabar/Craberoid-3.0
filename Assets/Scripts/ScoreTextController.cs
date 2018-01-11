using System.Collections;
using System.Collections.Generic;
using Signals;
using TMPro;
using UnityEngine;
using Zenject;

public class ScoreTextController : MonoBehaviour
{
    private int _scorepoints = 0;

    [Inject]
    public void Construct(GiveScorepointsSignal giveScorepointsSignal)
    {
        UpdateScoreText(_scorepoints);
        giveScorepointsSignal += AddScorepoints;
    }

    private void AddScorepoints(int scorepoints)
    {
        _scorepoints += scorepoints;
        UpdateScoreText(_scorepoints);
    }

    private void UpdateScoreText(int newScore)
    {
        GetComponent<TextMeshProUGUI>().text = $"Score: {newScore}";
    }
}
