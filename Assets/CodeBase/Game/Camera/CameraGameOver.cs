using Gobi.Game.Common;
using Gobi.Infrastructure.Services.Input;
using Gobi.UI.Services.Window;
using UnityEngine;
using Zenject;

namespace Gobi.Game.Camera
{
  public class CameraGameOver : MonoBehaviour
  {
    public ConstantMover CameraMover;
    public EnterTrigger Trigger;

    private IInputService m_input;
    private IWindowService m_windowService;

    [Inject]
    private void Construct(IInputService inputService, IWindowService windowService)
    {
      m_input = inputService;
      m_windowService = windowService;

      Trigger.Triggered += GameOver;
    }

    private void GameOver()
    {
      CameraMover.enabled = false;
      m_input.Disable();

      m_windowService.Open(WindowId.GameOver);
    }
  }
}