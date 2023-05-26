namespace Gobi.Infrastructure.Services.GameState
{
  public interface IExitableGameState
  {
    void Exit();
  }

  public interface IEnterableGameState : IExitableGameState
  {
    void Enter();
  }

  public interface IPayloadGameState<in TPayload> : IExitableGameState
  {
    void Enter(TPayload payload);
  }
}