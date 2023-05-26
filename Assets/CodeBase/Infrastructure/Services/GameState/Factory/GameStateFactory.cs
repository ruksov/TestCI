using System;
using System.Collections.Generic;
using Zenject;

namespace Gobi.Infrastructure.Services.GameState
{
  class GameStateFactory : IGameStateFactory
  {
    private readonly DiContainer m_container;

    private readonly Dictionary<Type, IExitableGameState> m_states = new();

    public GameStateFactory(DiContainer container)
    {
      m_container = container;
    }

    public IExitableGameState State<TState>() where TState : IExitableGameState
    {
      Type type = typeof(TState);

      if (m_states.TryGetValue(type, out IExitableGameState state))
        return state;

      return m_states[type] = m_container.Resolve<TState>();
    }
  }
}