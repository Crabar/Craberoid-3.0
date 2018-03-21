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
        var textMesh = gameResultText.GetComponent<TextMeshProUGUI>();
        textMesh.color = gameResultDto.IsHighlighted ? new Color32(100, 255, 100, 255) : new Color32(255, 255, 255, 255);
        textMesh.text = $"{gameResultDto.Timestamp:HH:mm}    {gameResultDto.Score:D4}";
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
