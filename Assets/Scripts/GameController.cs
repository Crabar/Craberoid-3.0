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
    private readonly ScoreboardController _scoreboardController;

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
        ScoreboardController scoreboardController)
    {
        _gameStateChangedSignal = gameStateChangedSignal;
        _stateFactory = stateFactory;
        _endGameText = endGameText;
        _scoreText = scoreText;
        _scoreboardController = scoreboardController;

        playerWinsSignal += OnPlayerWins;
        playerLosesSignal += OnPlayerLoses;
        giveScorepointsSignal += OnGainedScorepoints;
        saveResultToScoreboardSignal += OnSaveGameResult;
    }

    private void OnSaveGameResult()
    {
        _scoreboardController.SaveResultToScoreboard(new GameResultDto { Score = _score, Timestamp = DateTime.Now});
    }

    private void OnPlayerLoses()
    {
        _endGameText.ShowLose(_score);
    }

    private void OnGainedScorepoints(int scorepoints)
    {
        _score += scorepoints;
        _scoreText.UpdateScoreText(_score);
    }

    private void OnPlayerWins()
    {
        _endGameText.ShowWin(_score);
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
