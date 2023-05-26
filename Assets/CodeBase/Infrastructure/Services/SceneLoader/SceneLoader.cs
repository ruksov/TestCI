using System;
using System.Collections;
using Gobi.Infrastructure.Services.CoroutineRunner;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gobi.Infrastructure.Services.SceneLoader
{
  class SceneLoader : ISceneLoader
  {
    private readonly ICoroutineRunner m_coroutineRunner;

    public SceneLoader(ICoroutineRunner coroutineRunner) =>
      m_coroutineRunner = coroutineRunner;

    public void Load(string name, Action onLoaded = null)
    {
      if (SceneManager.GetActiveScene().name == name)
      {
        onLoaded?.Invoke();
        return;
      }

      m_coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
    }

    public void Reload(Action onLoaded) =>
      m_coroutineRunner.StartCoroutine(LoadScene(SceneManager.GetActiveScene().name, onLoaded));

    private static IEnumerator LoadScene(string nextScene, Action onLoaded = null)
    {
      AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

      while (!waitNextScene.isDone)
        yield return null;

      onLoaded?.Invoke();
    }
  }
}