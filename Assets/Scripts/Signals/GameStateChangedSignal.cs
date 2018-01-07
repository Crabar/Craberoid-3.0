using States;
using Zenject;

namespace Signals
{
    public class GameStateChangedSignal : Signal<GameStateChangedSignal, IGameState>
    {
    }
}