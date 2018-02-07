using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        gameResultText.GetComponent<TextMeshProUGUI>().text = $"{gameResultDto.Timestamp.Hour}:{gameResultDto.Timestamp.Minute}    {gameResultDto.Score}";
    }

    public void ShowPanel()
    {
        gameObject.SetActive(true);
    }

    public void HidePanel()
    {
        gameObject.SetActive(false);
    }

    [Serializable]
    public class Settings
    {
        public GameObject GameResultPrefab;
    }
}
