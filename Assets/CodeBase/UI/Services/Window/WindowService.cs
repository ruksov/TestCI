using Gobi.UI.Services.Factory;

namespace Gobi.UI.Services.Window
{
  class WindowService : IWindowService
  {
    private readonly IUIFactory m_uiFactory;

    public WindowService(IUIFactory uiFactory) =>
      m_uiFactory = uiFactory;

    public void Open(WindowId id)
    {
      switch (id)
      {
        case WindowId.GameOver:
          m_uiFactory.CreateGameOver();
          break;
      }
    }
  }
}