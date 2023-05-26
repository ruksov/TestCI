using Gobi.Infrastructure.Services.StaticData;
using Gobi.UI.Services.Window;
using UnityEngine;
using Zenject;

namespace Gobi.UI.Services.Factory
{
  class UIFactory : IUIFactory
  {
    private readonly DiContainer m_container;
    private readonly IStaticDataService m_staticData;

    private Transform m_uiRoot;

    public UIFactory(DiContainer container, IStaticDataService staticData)
    {
      m_container = container;
      m_staticData = staticData;
    }

    public void CreateGameOver() =>
      m_container.InstantiatePrefab(m_staticData.WindowPrefab(WindowId.GameOver), m_uiRoot);

    public void CreateUIRoot() =>
      m_uiRoot = Object.Instantiate(m_staticData.UIRootPrefab()).transform;

    public void CreateHud()
    {
      GameObject hud = Object.Instantiate(m_staticData.HudPrefab());
      m_container.InjectGameObject(hud);
    }
  }
}