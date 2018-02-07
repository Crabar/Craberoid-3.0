using System;
using Signals;
using Zenject;

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
        private readonly SaveResultToScoreboardSignal _saveResultToScoreboardSignal;
        private readonly PlayerLosesSignal _playerLosesSignal;
        private readonly PlayerWinsSignal _playerWinsSignal;

        public GameOverState(SaveResultToScoreboardSignal saveResultToScoreboardSignal1, PlayerLosesSignal playerLosesSignal, PlayerWinsSignal playerWinsSignal)
        {
            _saveResultToScoreboardSignal = saveResultToScoreboardSignal1;
            _playerLosesSignal = playerLosesSignal;
            _playerWinsSignal = playerWinsSignal;
        }

        public void SetContext(IGameContext context)
        {
            _gameContext = context;
        }

        public void SetGameResult(EndGameResult endGameResult)
        {
            _gameResult = endGameResult;
        }

        public void Tick()
        {
            if (_isFirstFrame)
            {
                _saveResultToScoreboardSignal.Fire();
                switch (_gameResult)
                {
                    case EndGameResult.Win:
                    {
                        _playerWinsSignal.Fire();
                        break;
                    }
                    case EndGameResult.Lose:
                    {
                        _playerLosesSignal.Fire();
                        break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException();
                }

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
