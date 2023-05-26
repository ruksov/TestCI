using Gobi.Infrastructure.StaticData;

namespace Gobi.Game.Services.Obstacles
{
  public interface IObstacleDataProvider
  {
    ObstacleData GetRandomObstacle();
  }
}