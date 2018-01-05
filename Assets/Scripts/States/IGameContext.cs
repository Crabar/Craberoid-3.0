namespace States
{
    public interface IGameContext
    {
        IGameState CurrentState { set; }
    }
}
