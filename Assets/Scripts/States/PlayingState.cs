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
        private StateFactory _stateFactory;

        public PlayingState(StateFactory stateFactory, MovePlayerSignal movePlayerSignal, GameEndedSignal gameEndedSignal)
        {
            _stateFactory = stateFactory;
            _movePlayerSignal = movePlayerSignal;
            gameEndedSignal += OnGameEnded;
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
