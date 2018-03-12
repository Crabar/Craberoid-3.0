using LevelGenerators;
using UI;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Zenject;

//[CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    public PlayerController.Settings Player;
    public BallController.Settings Ball;
    public BrickController.Settings Brick;
    public LevelManager.Settings LevelManager;
    public ScoreboardUIController.Settings Scoreboard;
    public SettingsController.Settings Sound;

    public override void InstallBindings()
    {
        Container.BindInstance(Player);
        Container.BindInstance(Ball);
        Container.BindInstance(Brick);
        Container.BindInstance(LevelManager);
        Container.BindInstance(Scoreboard);
        Container.BindInstance(Sound);
    }
}