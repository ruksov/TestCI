using Gobi.Game.Services.Camera;
using Gobi.Game.Services.Field;
using Gobi.Game.Services.Field.Generators;
using Gobi.Game.Services.Hero;
using Gobi.Infrastructure.Services.Input;
using Gobi.UI.Services.Factory;

namespace Gobi.Infrastructure.Services.GameState
{
  public class GameLoopState : IEnterableGameState
  {
    private readonly FieldSpawner m_fieldSpawner;
    private readonly IHeroFactory m_heroFactory;
    private readonly IInputService m_gameInput;
    private readonly IUIFactory m_uiFactory;
    private readonly IFieldGenerator m_fieldGenerator;
    private readonly ICameraFactory m_cameraFactory;
    private readonly FloatingOrigin m_floatingOrigin;

    public GameLoopState(
      FieldSpawner fieldSpawner,
      IHeroFactory heroFactory,
      IUIFactory uiFactory,
      IInputService gameInput,
      IFieldGenerator fieldGenerator,
      ICameraFactory cameraFactory,
      FloatingOrigin floatingOrigin)
    {
      m_fieldSpawner = fieldSpawner;
      m_heroFactory = heroFactory;
      m_gameInput = gameInput;
      m_fieldGenerator = fieldGenerator;
      m_uiFactory = uiFactory;
      m_cameraFactory = cameraFactory;
      m_floatingOrigin = floatingOrigin;
    }

    public void Enter()
    {
      m_fieldSpawner.Spawn();
      m_heroFactory.CreateHero();
      m_cameraFactory.CreateCamera();

      m_fieldGenerator.Start();
      m_floatingOrigin.Start();

      m_gameInput.Enable();

      m_uiFactory.CreateUIRoot();
      m_uiFactory.CreateHud();
    }

    public void Exit()
    {
    }
  }
}