using System;
using Signals;
using UnityEngine;
using Zenject;

namespace States
{
    public class PlayingState : IGameState
    {
        private readonly MovePlayerSignal _movePlayerSignal;
        private IGameState _gameStateImplementation;
        private IGameContext _gameContext;
        private readonly StateFactory _stateFactory;

        public PlayingState(
            StateFactory stateFactory,
            MovePlayerSignal movePlayerSignal,
            GameEndedSignal gameEndedSignal,
            AttachToPlayerSignal attachToPlayerSignal,
            LevelCompletedSignal levelCompletedSignal)
        {
            _stateFactory = stateFactory;
            _movePlayerSignal = movePlayerSignal;
            gameEndedSignal += OnGameEnded;
            levelCompletedSignal += OnLevelCompleted;
            attachToPlayerSignal.Fire(false);
        }

        private void OnLevelCompleted()
        {
            _gameContext.CurrentState = _stateFactory.CreateStartingGameState(_gameContext);
        }

        private void OnGameEnded()
        {
            _gameContext.CurrentState = _stateFactory.CreateGameOverState(_gameContext);
        }

        public void SetContext(IGameContext context)
        {
            _gameContext = context;
        }

        public void Tick()
        {
            // do nothing
        }

        public void FixedTick()
        {
            var moveHorizontal = Input.GetAxis("Horizontal");
            _movePlayerSignal.Fire(moveHorizontal);
        }

        public class Factory : Factory<PlayingState>
        {
        }
    }
}
