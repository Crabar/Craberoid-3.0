using System.Collections;
using System.Collections.Generic;
using States;
using UnityEngine;
using Zenject;

public class GameController : ITickable, IFixedTickable, IGameContext
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

    public GameController(GameStateChangedSignal gameStateChangedSignal, StateFactory stateFactory)
    {
        _gameStateChangedSignal = gameStateChangedSignal;
        CurrentState = stateFactory.CreateStartingGameState();
    }

    public void Tick()
    {
        _currentState.Tick(this);
    }

    public void FixedTick()
    {
        _currentState.FixedTick(this);
    }
}