using Gobi.Game.Services.Camera;
using Gobi.Game.Services.Field;
using Gobi.Game.Services.Field.Factory;
using Gobi.Game.Services.Field.Generators;
using Gobi.Game.Services.Hero;
using Gobi.Game.Services.Obstacles;
using Gobi.Infrastructure.Services.CoroutineRunner;
using Gobi.Infrastructure.Services.GameState;
using Gobi.Infrastructure.Services.Input;
using Gobi.Infrastructure.Services.Interaction;
using Gobi.Infrastructure.Services.SceneLoader;
using Gobi.Infrastructure.Services.StaticData;
using Gobi.UI.Services.Factory;
using Gobi.UI.Services.Window;
using Zenject;

namespace Gobi.Infrastructure.Installers
{
  public class ProjectInstaller : MonoInstaller, ICoroutineRunner
  {
    public override void InstallBindings()
    {
      RegisterCoroutineRunner();
      RegisterInputServices();
      RegisterSceneServices();
      RegisterStaticData();

      RegisterFieldServices();
      RegisterHeroServices();
      RegisterCameraServices();
      RegisterUIServices();

      RegisterFloatingOriginService();

      RegisterGameStateMachine();
      RegisterGameStates();
    }

    private void RegisterCoroutineRunner()
    {
      Container.Bind<ICoroutineRunner>().FromInstance(this);
    }

    private void RegisterSceneServices()
    {
      Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle().NonLazy();
    }

    private void RegisterInputServices()
    {
      Container.Bind<IInputService>().To<InputService>().AsSingle().NonLazy();
      Container.Bind<InteractionService>().AsSingle().NonLazy();
    }

    private void RegisterStaticData()
    {
      IStaticDataService staticData = new StaticDataService();
      staticData.Load();
      Container.Bind<IStaticDataService>().FromInstance(staticData);
    }

    private void RegisterFieldServices()
    {
      Container.Bind<IGridConverter>().To<GridConverter>().AsSingle().NonLazy();
      Container.Bind<ITileMap>().To<TileMap>().AsSingle().NonLazy();

      Container.Bind<IObstacleFactory>().To<ObstacleFactory>().AsSingle().NonLazy();
      Container.Bind<IObstacleDataProvider>().To<ObstacleDataProvider>().AsSingle().NonLazy();

      Container.Bind<IChunkFactory>().To<ChunkFactory>().AsSingle().NonLazy();

      Container.Bind<IObstaclesGenerator>().To<ObstaclesGenerator>().AsSingle().NonLazy();
      Container.Bind<IChunksGenerator>().To<ChunksGenerator>().AsSingle().NonLazy();
      Container.Bind<IFieldGenerator>().To<FieldGenerator>().AsSingle().NonLazy();

      Container.Bind<FieldSpawner>().AsSingle().NonLazy();
    }

    private void RegisterHeroServices()
    {
      Container.Bind<IHeroProvider>().To<HeroProvider>().AsSingle().NonLazy();
      Container.Bind<IHeroFactory>().To<HeroFactory>().AsSingle().NonLazy();
    }

    private void RegisterCameraServices()
    {
      Container.Bind<ICameraProvider>().To<CameraProvider>().AsSingle().NonLazy();
      Container.Bind<ICameraFactory>().To<CameraFactory>().AsSingle().NonLazy();
    }

    private void RegisterUIServices()
    {
      Container.Bind<IUIFactory>().To<UIFactory>().AsSingle().NonLazy();
      Container.Bind<IWindowService>().To<WindowService>().AsSingle().NonLazy();
    }

    private void RegisterFloatingOriginService()
    {
      Container.Bind<FloatingOrigin>().AsSingle().NonLazy();
    }

    private void RegisterGameStateMachine()
    {
      Container.Bind<IGameStateFactory>().To<GameStateFactory>().AsSingle().NonLazy();
      Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle().NonLazy();
    }

    private void RegisterGameStates()
    {
      Container.Bind<LoadLevelState>().AsSingle();
      Container.Bind<GameLoopState>().AsSingle();
    }
  }
}