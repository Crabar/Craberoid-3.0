using LevelGenerators;
using Signals;
using States;
using UnityEngine;
using Zenject;

public class MainMonoInstaller : MonoInstaller<MainMonoInstaller>
{
    [Inject] public BrickController.Settings BrickSettings;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameController>().AsSingle();
        // signals
        Container.DeclareSignal<LaunchBallSignal>();
        Container.DeclareSignal<GameStateChangedSignal>();
        Container.DeclareSignal<MovePlayerSignal>();
        Container.DeclareSignal<GameEndedSignal>();
        Container.DeclareSignal<GiveScorepointsSignal>();
        Container.DeclareSignal<AttachToPlayerSignal>();
        Container.DeclareSignal<LevelCompletedSignal>();
        Container.DeclareSignal<PlayerWinsSignal>();
        Container.DeclareSignal<ResetPlayerStateSignal>();
        Container.DeclareSignal<PlayerLosesSignal>();
        // factories
        Container.Bind<StateFactory>().AsSingle();
        Container.BindFactory<StartingGameState, StartingGameState.Factory>();
        Container.BindFactory<PlayingState, PlayingState.Factory>();
        Container.BindFactory<GameOverState, GameOverState.Factory>();

        Container.BindFactory<ClassicLevelGenerator, ClassicLevelGenerator.Factory>();
        Container.BindFactory<StarLevelGenerator, StarLevelGenerator.Factory>();
        //
        Container.BindFactory<BrickController, BrickController.Factory>()
                 .FromComponentInNewPrefab(BrickSettings.BrickPrefab)
                 .WithGameObjectName("Brick");
        Container.Bind<LevelManager>().AsSingle();
        Container.Bind<LevelGeneratorFactory>().AsSingle();
    }
}
