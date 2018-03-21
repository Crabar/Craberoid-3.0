using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Signals;
using States;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameController : ITickable, IFixedTickable, IGameContext, IInitializable
{
    private IGameState _currentState;

    public IGameState CurrentState
    {
        set
        {
            _currentState = value;
            _gameStateChangedSignal.Fire(value);
        }
    }

    private readonly GameStateChangedSignal _gameStateChangedSignal;
    private readonly StateFactory _stateFactory;
    private readonly EndGameTextController _endGameText;
    private readonly ScoreTextController _scoreText;
    private readonly ScoreboardDataController _scoreboardDataController;

    private int _score;

    public GameController(
        GameStateChangedSignal gameStateChangedSignal,
        StateFactory stateFactory,
        GiveScorepointsSignal giveScorepointsSignal,
        GameEndedSignal gameEndedSignal,
        ScoreTextController scoreText,
        EndGameTextController endGameText,
        ScoreboardDataController scoreboardDataController)
    {
        _gameStateChangedSignal = gameStateChangedSignal;
        _stateFactory = stateFactory;
        _endGameText = endGameText;
        _scoreText = scoreText;
        _scoreboardDataController = scoreboardDataController;

        giveScorepointsSignal += OnGainedScorepoints;
        gameEndedSignal += OnGameEnded;
    }

    private async void OnGameEnded(EndGameResult endGameResult)
    {
        _scoreboardDataController.SaveResultToScoreboard(new GameResultDto {Score = _score, Timestamp = DateTime.Now, IsHighlighted = true});
        switch (endGameResult)
        {
            case EndGameResult.Win:
                await _endGameText.ShowWin(_score);
                break;
            case EndGameResult.Lose:
                await _endGameText.ShowLose(_score);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(endGameResult), endGameResult, null);
        }

        _scoreboardDataController.ShowScoreboard();
        await Task.Delay(5000);
        SceneManager.LoadScene("Menu");
    }


    private void OnGainedScorepoints(int scorepoints)
    {
        _score += scorepoints;
        _scoreText.UpdateScoreText(_score);
    }

    public void Tick()
    {
        _currentState?.Tick();
    }

    public void FixedTick()
    {
        _currentState?.FixedTick();
    }

    public void Initialize()
    {
        CurrentState = _stateFactory.CreateStartingGameState(this);
        _scoreText.UpdateScoreText(_score);
    }
}
