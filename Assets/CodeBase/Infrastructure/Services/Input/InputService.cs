using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gobi.Infrastructure.Services.Input
{
  class InputService : IInputService, GameInput.IGameActions
  {
    public event Action Touch;
    public event Action TouchEnd;
    public event Action<SwipeDir> Swipe;

    private readonly GameInput m_gameInput;
    private readonly SwipeDetector m_swipeDetector;

    public InputService()
    {
      m_gameInput = new GameInput();
      m_gameInput.Game.SetCallbacks(this);
      m_gameInput.Enable();

      m_swipeDetector = new SwipeDetector();
    }

    public Vector2 TouchPosition() =>
      m_gameInput.Game.TouchPosition.ReadValue<Vector2>();

    public void Enable() =>
      m_gameInput.Enable();

    public void Disable() =>
      m_gameInput.Disable();

    public void OnPrimaryTap(InputAction.CallbackContext context)
    {
      switch (context.phase)
      {
        case InputActionPhase.Started:
          StartSwipe();
          Touch?.Invoke();
          break;

        case InputActionPhase.Canceled:
          StopSwipe();
          TouchEnd?.Invoke();
          break;
      }
    }

    public void OnTouchPosition(InputAction.CallbackContext context)
    {
    }

    private void StartSwipe() =>
      m_swipeDetector.StartSwipe(TouchPosition());

    private void StopSwipe()
    {
      if (m_swipeDetector.Detect(TouchPosition(), out SwipeDir swipeDir))
        Swipe?.Invoke(swipeDir);
    }
  }
}