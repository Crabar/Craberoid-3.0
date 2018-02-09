using States;
using Zenject;

namespace Signals
{
    public class GameEndedSignal : Signal<GameEndedSignal, EndGameResult>
    {
    }
}
