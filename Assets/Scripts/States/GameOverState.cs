using Signals;
using Zenject;

namespace States
{
    public class GameOverState : IGameState
    {
        private IGameContext _gameContext;

        public GameOverState(MovePlayerSignal movePlayerSignal)
        {
            movePlayerSignal.Fire(0);
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
