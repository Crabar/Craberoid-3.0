namespace States
{
    public interface IGameState
    {
        void Tick(IGameContext gameContext);
        void FixedTick(IGameContext gameContext);
    }
}
