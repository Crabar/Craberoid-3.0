using System;
using Signals;
using UnityEngine;
using Zenject;

namespace States
{
    public class PlayingState : IGameState
    {
        private readonly ResetPlayerStateSignal _resetPlayerStateSignal;
        private IGameState _gameStateImplementation;
        private IGameContext _gameContext;
        private readonly StateFactory _stateFactory;
        private readonly GiveScorepointsSignal _giveScorepointsSignal;
        private readonly MovePlayerToPositionSignal _movePlayerToPositionSignal;

        public PlayingState(
            StateFactory stateFactory,
            FloorTouchedSignal floorTouchedSignal,
            AttachToPlayerSignal attachToPlayerSignal,
            LevelCompletedSignal levelCompletedSignal,
            ResetPlayerStateSignal resetPlayerStateSignal,
            GiveScorepointsSignal giveScorepointsSignal,
            MovePlayerToPositionSignal movePlayerToPositionSignal)
        {
            _stateFactory = stateFactory;
            _resetPlayerStateSignal = resetPlayerStateSignal;
            _giveScorepointsSignal = giveScorepointsSignal;
            _movePlayerToPositionSignal = movePlayerToPositionSignal;
            floorTouchedSignal += OnGameEnded;
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
            _gameContext.CurrentState = _stateFactory.CreateGameOverState(_gameContext, EndGameResult.Lose);
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
            var targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _movePlayerToPositionSignal.Fire(targetPosition.x);
        }

        public class Factory : Factory<PlayingState>
        {
        }
    }
}
