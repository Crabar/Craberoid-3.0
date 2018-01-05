using States;
using UnityEngine;
using Zenject;

public class MainMonoInstaller : MonoInstaller<MainMonoInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameController>().AsSingle();
        // signals
        Container.DeclareSignal<MoveBallSignal>();
        Container.DeclareSignal<LaunchBallSignal>();
        Container.DeclareSignal<GameStateChangedSignal>();
        Container.DeclareSignal<MovePlayerSignal>();
        // factories
        Container.Bind<StateFactory>().AsSingle();
        Container.BindFactory<StartingGameState, StartingGameState.Factory>();
        Container.BindFactory<PlayingState, PlayingState.Factory>();
    }
}