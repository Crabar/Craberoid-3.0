using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking.Types;

public class ScoreboardController
{
    private LinkedList<GameResultDto> _scoreboard;
    private readonly string _path;

    public ScoreboardController()
    {
        _path = Application.persistentDataPath + "/scores.bin";
        LoadScoreboard();
    }

    public void SaveResultToScoreboard(GameResultDto result)
    {
        var curResultNode = _scoreboard.First;
        while (curResultNode != null)
        {
            if (result.Score > curResultNode.Value.Score)
            {
                _scoreboard.AddBefore(curResultNode, result);
                _scoreboard.RemoveLast();
                break;
            }

            curResultNode = curResultNode.Next;
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
