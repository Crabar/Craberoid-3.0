using UnityEngine;
using Zenject;

namespace States
{
    public class StartingGameState : IGameState
    {
        private readonly MoveBallSignal _moveBallSignal;
        private readonly LaunchBallSignal _launchBallSignal;
        private readonly StateFactory _stateFactory;
        private readonly MovePlayerSignal _movePlayerSignal;
        private readonly PlayerController.Settings _playerSettings;

        private readonly int _startingBallSpeed;

        public StartingGameState(
            StateFactory stateFactory,
            MoveBallSignal moveBallSignal,
            MovePlayerSignal movePlayerSignal,
            PlayerController.Settings playerSettings,
            LaunchBallSignal launchBallSignal)
        {
            _stateFactory = stateFactory;
            _moveBallSignal = moveBallSignal;
            _movePlayerSignal = movePlayerSignal;
            _playerSettings = playerSettings;
            _launchBallSignal = launchBallSignal;
        }

        public void Tick(IGameContext gameContext)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _launchBallSignal.Fire(new Vector3(Random.Range(5, 20), 0, Random.Range(5, 20)));
                gameContext.CurrentState = _stateFactory.CreatePlayingState();
            }
        }

        public void FixedTick(IGameContext gameContext)
        {
            var moveHorizontal = Input.GetAxis("Horizontal");
            _movePlayerSignal.Fire(moveHorizontal);
            _moveBallSignal.Fire(new Vector3(moveHorizontal * _playerSettings.playerSpeed, 0, 0));
        }

        public class Factory : Factory<StartingGameState>
        {
        }
    }
}
