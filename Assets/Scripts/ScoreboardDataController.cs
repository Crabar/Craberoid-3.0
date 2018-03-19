using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.Networking.Types;

public class ScoreboardDataController
{
    private LinkedList<GameResultDto> _scoreboard;
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
        if (_scoreboard.Count == 0)
        {
            _scoreboard.AddFirst(result);
        }
        else
        {
            var curResultNode = _scoreboard.First;
            var resAdded = false;
            while (curResultNode != null)
            {
                if (result.Score > curResultNode.Value.Score)
                {
                    _scoreboard.AddBefore(curResultNode, result);
                    resAdded = true;
                }

                if (result.Score <= curResultNode.Value.Score)
                {
                    _scoreboard.AddAfter(curResultNode, result);
                    resAdded = true;
                }

                if (resAdded)
                {
                    if (_scoreboard.Count > 5)
                    {
                        _scoreboard.RemoveLast();
                    }
                    break;
                }

                curResultNode = curResultNode.Next;
            }
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
            _scoreboard = obj as LinkedList<GameResultDto>;
        }

        if (_scoreboard == null)
        {
            _scoreboard = new LinkedList<GameResultDto>();
        }
    }
}

[Serializable]
public class GameResultDto
{
    public int Score;
    public DateTime Timestamp;
}
