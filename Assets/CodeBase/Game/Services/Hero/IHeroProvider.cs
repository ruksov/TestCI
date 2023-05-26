using Gobi.Game.Hero;
using UnityEngine;

namespace Gobi.Game.Services.Hero
{
  public interface IHeroProvider
  {
    GameObject Hero { get; set; }
    HeroScore HereScore { get; }
    HeroMove HeroMove{ get; }
  }
}