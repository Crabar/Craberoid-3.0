using LevelGenerators;
using Signals;
using UnityEngine;
using Zenject;

namespace States
{
    public class StartingGameState : IGameState
    {
        private readonly LaunchBallSignal _launchBallSignal;
        private readonly StateFactory _stateFactory;
        private readonly MovePlayerSignal _movePlayerSignal;
        private readonly AttachToPlayerSignal _attachToPlayerSignal;
        private readonly PlayerWinsSignal _playerWinsSignal;
        private readonly LevelManager _levelManager;

        private readonly int _startingBallSpeed;
        private IGameContext _gameContext;

        public StartingGameState(
            StateFactory stateFactory,
            MovePlayerSignal movePlayerSignal,
            LaunchBallSignal launchBallSignal,
            AttachToPlayerSignal attachToPlayerSignal,
            LevelManager levelManager,
            PlayerWinsSignal playerWinsSignal,
            ResetPlayerStateSignal resetPlayerStateSignal)
        {
            _stateFactory = stateFactory;
            _levelManager = levelManager;
            _attachToPlayerSignal = attachToPlayerSignal;
            _playerWinsSignal = playerWinsSignal;
            _movePlayerSignal = movePlayerSignal;
            _launchBallSignal = launchBallSignal;

            resetPlayerStateSignal.Fire();
        }


        public void SetContext(IGameContext context)
        {
            _gameContext = context;
            
            var isNextLevelAvailable = _levelManager.TryGenerateNextLevel();
            
            if (isNextLevelAvailable)
            {
                _attachToPlayerSignal.Fire(true);
            }
            else
            {
                _gameContext.CurrentState = _stateFactory.CreateGameOverState(_gameContext);
                _playerWinsSignal.Fire();
            }
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
//                _launchBallSignal.Fire(new Vector3(0, 0, 1));
                _launchBallSignal.Fire(new Vector3(Random.Range(-20, 20), 0, Random.Range(5, 20)));
                _gameContext.CurrentState = _stateFactory.CreatePlayingState(_gameContext);
            }
        }

        public void FixedTick()
        {
            var moveHorizontal = Input.GetAxis("Horizontal");
            _movePlayerSignal.Fire(moveHorizontal);
        }

        public class Factory : Factory<StartingGameState>
        {
        }
    }
}
