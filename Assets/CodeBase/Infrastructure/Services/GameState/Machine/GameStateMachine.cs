namespace Gobi.Infrastructure.Services.GameState
{
  class GameStateMachine : IGameStateMachine
  {
    private readonly IGameStateFactory m_stateFactory;

    private IExitableGameState m_state;

    public GameStateMachine(IGameStateFactory stateFactory) =>
      m_stateFactory = stateFactory;

    public void Enter<TState>() where TState : IExitableGameState
    {
      ChangeState<TState>();

      if (m_state is IEnterableGameState state)
        state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : IPayloadGameState<TPayload>
    {
      ChangeState<TState>();

      if (m_state is IPayloadGameState<TPayload> payloadGameState)
        payloadGameState.Enter(payload);
    }

    private void ChangeState<TState>() where TState : IExitableGameState
    {
      m_state?.Exit();
      m_state = m_stateFactory.State<TState>();
    }
  }
}