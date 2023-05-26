using System;
using UnityEngine;

namespace Gobi.Infrastructure.Services.Input
{
  public interface IInputService
  {
    event Action Touch;
    event Action TouchEnd;
    event Action<SwipeDir> Swipe;

    Vector2 TouchPosition();
    void Enable();
    void Disable();
  }
}