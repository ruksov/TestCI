namespace Gobi.Infrastructure.Services.GameState
{
  public interface IGameStateMachine
  {
    void Enter<TState>() where TState : IExitableGameState;
    void Enter<TState, TPayload>(TPayload payload) where TState : IPayloadGameState<TPayload>;
  }
}