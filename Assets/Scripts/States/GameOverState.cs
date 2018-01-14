using Signals;
using Zenject;

namespace States
{
    public class GameOverState : IGameState
    {
        private IGameContext _gameContext;

        public GameOverState()
        {
        }

        public void SetContext(IGameContext context)
        {
            _gameContext = context;
        }

        public void Tick()
        {
            //
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
