using System;
using Signals;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Object = UnityEngine.Object;

namespace States
{
    public enum EndGameResult
    {
        Win,
        Lose
    }

    public class GameOverState : IGameState
    {
        private IGameContext _gameContext;
        private EndGameResult _gameResult;

        private bool _isFirstFrame = true;
        private readonly GameEndedSignal _gameEndedSignal;
        
        [Inject] public InputController InputController;

        public GameOverState(GameEndedSignal gameEndedSignal1)
        {
            _gameEndedSignal = gameEndedSignal1;
        }

        public void SetContext(IGameContext context)
        {
            _gameContext = context;
        }

        public void SetGameResult(EndGameResult endGameResult)
        {
            _gameResult = endGameResult;
        }
        
        private void StopAllAnimation() 
        {
            var allAnims = Object.FindObjectsOfType<Animation>();
            foreach(var anim in allAnims) 
            {
                anim.Stop();
            }
        }

        public void Tick()
        {
            if (_isFirstFrame)
            {
                InputController.OnDoubleTap += () =>
                {
                    StopAllAnimation();
                    SceneManager.LoadScene("Menu");
                };
                _gameEndedSignal.Fire(_gameResult);
                _isFirstFrame = false;
            }
        }

        public void FixedTick()
        {
            //
        }

        public class Factory : Factory<GameOverState>
        {
        }
    }
}
