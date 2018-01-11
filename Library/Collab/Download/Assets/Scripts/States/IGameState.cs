namespace States
{
    public interface IGameState
    {
        void SetContext(IGameContext context);
        void Tick();
        void FixedTick();
    }
}
