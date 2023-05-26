using Gobi.Infrastructure.Services.SceneLoader;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Gobi.UI.Windows
{
  public class GameOverWindow : MonoBehaviour
  {
    public Button RestartButton;

    private ISceneLoader m_sceneLoader;

    [Inject]
    private void Construct(ISceneLoader sceneLoader) =>
      m_sceneLoader = sceneLoader;

    private void Start() =>
      RestartButton.onClick.AddListener(RestartGame);

    private void RestartGame() =>
      m_sceneLoader.Reload(null);
  }
}