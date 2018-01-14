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
    private readonly WinTextController _winText;
    private readonly ScoreTextController _scoreText;

    private int _score;

    public GameController(GameStateChangedSignal gameStateChangedSignal, StateFactory stateFactory,
        PlayerWinsSignal playerWinsSignal, PlayerLosesSignal playerLosesSignal,
        GiveScorepointsSignal giveScorepointsSignal, ScoreTextController scoreText,
        WinTextController winText)
    {
        _gameStateChangedSignal = gameStateChangedSignal;
        _stateFactory = stateFactory;
        _winText = winText;
        _scoreText = scoreText;

        playerWinsSignal += OnPlayerWins;
        playerLosesSignal += OnPlayerLoses;
        giveScorepointsSignal += OnGainedScorepoints;
    }

    private void OnPlayerLoses()
    {
        _winText.ShowLose(_score);
    }

    private void OnGainedScorepoints(int scorepoints)
    {
        _score += scorepoints;
        _scoreText.UpdateScoreText(_score);
    }

    private void OnPlayerWins()
    {
        _winText.ShowWin(_score);
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