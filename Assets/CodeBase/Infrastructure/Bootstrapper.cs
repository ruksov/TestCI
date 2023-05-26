using Gobi.Infrastructure.Services.GameState;
using UnityEngine;
using Zenject;

namespace Gobi.Infrastructure
{
  public class Bootstrapper : MonoBehaviour
  {
    public string StartSceneName;

    private IGameStateMachine m_stateMachine;

    [Inject]
    public void Construct(IGameStateMachine stateMachine) =>
      m_stateMachine = stateMachine;

    private void Start() =>
      m_stateMachine.Enter<LoadLevelState, string>(StartSceneName);
  }
}