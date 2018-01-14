using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Signals;
using TMPro;
using UnityEngine;
using Zenject;

public class ScoreTextController : MonoBehaviour
{
    private const float SecondsForPoint = 0.1f;
    
    private int _currentScore;
    private int _realScore;
    private float _timestamp;
    
    public void UpdateScoreText(int newScore)
    {
        _timestamp = Time.time;
        _realScore = newScore;
    }

    private void Update()
    {
        if (_currentScore >= _realScore)
        {
            return;
        }
        
        if (Time.time - _timestamp < SecondsForPoint)
        {
            return;
        }

        _currentScore += 1;
        GetComponent<TextMeshProUGUI>().text = _currentScore.ToString("D5");
        _timestamp = Time.time;
    }
}