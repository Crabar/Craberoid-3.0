using System;
using Signals;
using UnityEngine;
using Zenject;

namespace States
{
    public class PlayingState : IGameState
    {
        private readonly MovePlayerSignal _movePlayerSignal;
        private readonly ResetPlayerStateSignal _resetPlayerStateSignal;
        private IGameState _gameStateImplementation;
        private IGameContext _gameContext;
        private readonly StateFactory _stateFactory;
        private readonly PlayerLosesSignal _playerLosesSignal;
        private readonly GiveScorepointsSignal _giveScorepointsSignal;

        public PlayingState(
            StateFactory stateFactory,
            MovePlayerSignal movePlayerSignal,
            GameEndedSignal gameEndedSignal,
            AttachToPlayerSignal attachToPlayerSignal,
            LevelCompletedSignal levelCompletedSignal,
            ResetPlayerStateSignal resetPlayerStateSignal,
            PlayerLosesSignal playerLosesSignal, GiveScorepointsSignal giveScorepointsSignal)
        {
            _stateFactory = stateFactory;
            _movePlayerSignal = movePlayerSignal;
            _resetPlayerStateSignal = resetPlayerStateSignal;
            _playerLosesSignal = playerLosesSignal;
            _giveScorepointsSignal = giveScorepointsSignal;
            gameEndedSignal += OnGameEnded;
            levelCompletedSignal += OnLevelCompleted;
            attachToPlayerSignal.Fire(false);
        }

        private void OnLevelCompleted()
        {
            _gameContext.CurrentState = _stateFactory.CreateStartingGameState(_gameContext);
            _giveScorepointsSignal.Fire(100);
        }

        private void OnGameEnded()
        {
            _resetPlayerStateSignal.Fire();
            _playerLosesSignal.Fire();
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