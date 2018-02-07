using System;
using System.Collections;
using System.Collections.Generic;
using Signals;
using States;
using UnityEngine;
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
        PlayerWinsSignal playerWinsSignal,
        PlayerLosesSignal playerLosesSignal,
        GiveScorepointsSignal giveScorepointsSignal,
        SaveResultToScoreboardSignal saveResultToScoreboardSignal,
        ScoreTextController scoreText,
        EndGameTextController endGameText,
        ScoreboardDataController scoreboardDataController)
    {
        _gameStateChangedSignal = gameStateChangedSignal;
        _stateFactory = stateFactory;
        _endGameText = endGameText;
        _scoreText = scoreText;
        _scoreboardDataController = scoreboardDataController;

        playerWinsSignal += OnPlayerWins;
        playerLosesSignal += OnPlayerLoses;
        giveScorepointsSignal += OnGainedScorepoints;
        saveResultToScoreboardSignal += OnSaveGameResult;
    }

    private void OnSaveGameResult()
    {
        _scoreboardDataController.SaveResultToScoreboard(new GameResultDto { Score = _score, Timestamp = DateTime.Now });
    }

    private async void OnPlayerLoses()
    {
        await _endGameText.ShowLose(_score);
        _scoreboardDataController.ShowScoreboard();
    }

    private void OnGainedScorepoints(int scorepoints)
    {
        _score += scorepoints;
        _scoreText.UpdateScoreText(_score);
    }

    private async void OnPlayerWins()
    {
        await _endGameText.ShowWin(_score);
        _scoreboardDataController.ShowScoreboard();
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
