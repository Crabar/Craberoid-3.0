using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ScoreboardUIController : MonoBehaviour
{
    private DiContainer _diContainer;
    private Settings _settings;

    [Inject]
    public void Construct(DiContainer diContainer, Settings settings)
    {
        _diContainer = diContainer;
        _settings = settings;
    }

    public void AddElement(GameResultDto gameResultDto)
    {
        var gameResultText = _diContainer.InstantiatePrefab(_settings.GameResultPrefab);
        gameResultText.transform.SetParent(transform, false);
    }

    [Serializable]
    public class Settings
    {
        public GameObject GameResultPrefab;
    }
}
