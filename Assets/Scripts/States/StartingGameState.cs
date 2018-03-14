using System;
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
        private readonly AttachToPlayerSignal _attachToPlayerSignal;
        private readonly LevelManager _levelManager;

        private readonly int _startingBallSpeed;
        private IGameContext _gameContext;

        [Inject] public InputController InputController;

        public StartingGameState(
            StateFactory stateFactory,
            LaunchBallSignal launchBallSignal,
            AttachToPlayerSignal attachToPlayerSignal,
            LevelManager levelManager,
            ResetPlayerStateSignal resetPlayerStateSignal)
        {
            _stateFactory = stateFactory;
            _levelManager = levelManager;
            _attachToPlayerSignal = attachToPlayerSignal;
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
                _gameContext.CurrentState = _stateFactory.CreateGameOverState(_gameContext, EndGameResult.Win);
            }
        }

        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _launchBallSignal.Fire(new Vector3(UnityEngine.Random.Range(-20, 20), 0,
                    UnityEngine.Random.Range(5, 20)));
                _gameContext.CurrentState = _stateFactory.CreatePlayingState(_gameContext);
            }
        }

        public void FixedTick()
        {
            InputController.ProcessPlayerMovement();
        }

        public class Factory : Factory<StartingGameState>
        {
        }
    }
}