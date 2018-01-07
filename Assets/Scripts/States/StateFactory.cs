using Zenject;

namespace States
{
    public class StateFactory
    {
        private readonly StartingGameState.Factory _startingGameStateFactory;
        private readonly PlayingState.Factory _playingStateFactory;
        private readonly GameOverState.Factory _gameOverStateFactory;

        public StateFactory(
            StartingGameState.Factory startingGameStateFactory,
            PlayingState.Factory playingStateFactory,
            GameOverState.Factory gameOverStateFactory)
        {
            _startingGameStateFactory = startingGameStateFactory;
            _playingStateFactory = playingStateFactory;
            _gameOverStateFactory = gameOverStateFactory;
        }

        private IGameState CreateAndInitState<TState>(IFactory<TState> factory, IGameContext context) where TState : IGameState
        {
            var state = factory.Create();
            state.SetContext(context);
            return state;
        }

        public IGameState CreateStartingGameState(IGameContext context)
        {
            return CreateAndInitState(_startingGameStateFactory, context);
        }

        public IGameState CreatePlayingState(IGameContext context)
        {
            return CreateAndInitState(_playingStateFactory, context);
        }

        public IGameState CreateGameOverState(IGameContext context)
        {
            return CreateAndInitState(_gameOverStateFactory, context);
        }
    }
}
