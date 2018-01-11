namespace States
{
    public class StateFactory
    {
        private readonly StartingGameState.Factory _startingGameStateFactory;
        private readonly PlayingState.Factory _playingStateFactory;

        public StateFactory(StartingGameState.Factory startingGameStateFactory, PlayingState.Factory playingStateFactory)
        {
            _startingGameStateFactory = startingGameStateFactory;
            _playingStateFactory = playingStateFactory;
        }

        public IGameState CreateStartingGameState()
        {
            return _startingGameStateFactory.Create();
        }

        public IGameState CreatePlayingState()
        {
            return _playingStateFactory.Create();
        }
    }
}
