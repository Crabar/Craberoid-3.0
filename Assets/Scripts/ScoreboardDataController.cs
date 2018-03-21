using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using ModestTree;
using TMPro;
using UnityEngine;
using UnityEngine.Networking.Types;

public class ScoreboardDataController
{
    private List<GameResultDto> _scoreboard;
    private readonly string _path;
    private readonly ScoreboardUIController _scoreboardUiController;

    public ScoreboardDataController(ScoreboardUIController scoreboardUiController)
    {
        _scoreboardUiController = scoreboardUiController;
        _scoreboardUiController.HidePanel();
        _path = Application.persistentDataPath + "/scores.bin";
        LoadScoreboard();
    }

    public void ShowScoreboard()
    {
        foreach (var gameResultDto in _scoreboard)
        {
            _scoreboardUiController.AddElement(gameResultDto);
        }

        _scoreboardUiController.ShowPanel();
    }

    public void SaveResultToScoreboard(GameResultDto result)
    {
        _scoreboard.Add(result);
        _scoreboard.Sort((dto, resultDto) => dto.Score > resultDto.Score ? -1 : 1);

        if (_scoreboard.Count > 5)
        {
            _scoreboard = _scoreboard.GetRange(0, 5);
        }

        var formatter = new BinaryFormatter();
        var fileStream = new FileStream(_path, FileMode.Create);
        formatter.Serialize(fileStream, _scoreboard);
        fileStream.Close();
    }

    private void LoadScoreboard()
    {
        if (File.Exists(_path))
        {
            var formatter = new BinaryFormatter();
            var fileStream = new FileStream(_path, FileMode.Open);
            var obj = formatter.Deserialize(fileStream);
            fileStream.Close();
            if (obj is LinkedList<GameResultDto>)
            {
                _scoreboard = (obj as LinkedList<GameResultDto>).ToList();
            }
            else if (obj is List<GameResultDto>)
            {
                _scoreboard = obj as List<GameResultDto>;
            }  
        }

        if (_scoreboard == null)
        {
            _scoreboard = new List<GameResultDto>();
        }

        _scoreboard.Select(r =>
        {
            r.IsHighlighted = false;
            return r;
        }).ToList();
    }
}

[Serializable]
public class GameResultDto
{
    public int Score;
    public DateTime Timestamp;
    public bool IsHighlighted;
}
