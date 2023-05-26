namespace Gobi.Infrastructure.Services.GameState
{
  public interface IGameStateFactory
  {
    IExitableGameState State<TState>() where TState : IExitableGameState;
  }
}