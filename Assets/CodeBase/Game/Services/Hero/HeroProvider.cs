using Gobi.Game.Hero;
using UnityEngine;

namespace Gobi.Game.Services.Hero
{
  class HeroProvider : IHeroProvider
  {
    public GameObject Hero { get; set; }
    public HeroScore HereScore => Hero.GetComponent<HeroScore>();
    public HeroMove HeroMove => Hero.GetComponent<HeroMove>();
  }
}