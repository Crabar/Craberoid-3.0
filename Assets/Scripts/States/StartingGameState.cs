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

        private readonly int _startingBallSpeed;
        private IGameContext _gameContext;

        public StartingGameState(
            StateFactory stateFactory,
            MovePlayerSignal movePlayerSignal,
            LaunchBallSignal launchBallSignal,
            AttachToPlayerSignal attachToPlayerSignal,
            LevelManager levelManager,
            PlayerWinsSignal playerWinsSignal,
            [Inject(Id = "PlayerTransform")]
            Transform playerTransform)
        {
            _stateFactory = stateFactory;
            _movePlayerSignal = movePlayerSignal;
            _launchBallSignal = launchBallSignal;

            var isNextLevelAvailable = levelManager.TryGenerateNextLevel();
            if (isNextLevelAvailable)
            {
                attachToPlayerSignal.Fire(true);
                playerTransform.position = new Vector3(0, 0, -19);
            }
            else
            {
                playerWinsSignal.Fire();
            }
        }


        public void SetContext(IGameContext context)
        {
            _gameContext = context;
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _launchBallSignal.Fire(new Vector3(0, 0, 1));
//                _launchBallSignal.Fire(new Vector3(Random.Range(-20, 20), 0, Random.Range(5, 20)));
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
