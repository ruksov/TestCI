using Gobi.Infrastructure.Services.StaticData;
using UnityEngine;
using Zenject;

namespace Gobi.Game.Services.Hero
{
  public class HeroFactory : IHeroFactory
  {
    private readonly DiContainer m_container;
    private readonly IStaticDataService m_staticData;
    private readonly IHeroProvider m_heroProvider;

    public HeroFactory(DiContainer container, IStaticDataService staticData, IHeroProvider heroProvider)
    {
      m_container = container;
      m_staticData = staticData;
      m_heroProvider = heroProvider;
    }

    public GameObject CreateHero()
    {
      GameObject heroObject = Object.Instantiate(m_staticData.HeroPrefab());
      m_container.InjectGameObject(heroObject);

      m_heroProvider.Hero = heroObject;
      return heroObject;
    }
  }
}