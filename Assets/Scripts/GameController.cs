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

    public GameController(GameStateChangedSignal gameStateChangedSignal, StateFactory stateFactory)
    {
        _gameStateChangedSignal = gameStateChangedSignal;
        _stateFactory = stateFactory;
    }

    public void Tick()
    {
        _currentState.Tick();
    }

    public void FixedTick()
    {
        _currentState.FixedTick();
    }

    public void Initialize()
    {
        CurrentState = _stateFactory.CreateStartingGameState(this);
    }
}