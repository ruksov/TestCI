using System;

namespace Gobi.Infrastructure.Services.SceneLoader
{
  public interface ISceneLoader
  {
    void Load(string sceneName, Action onLoaded);
    void Reload(Action onLoaded);
  }
}