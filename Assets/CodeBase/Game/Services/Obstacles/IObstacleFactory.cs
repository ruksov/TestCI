using Gobi.Infrastructure.StaticData;
using UnityEngine;

namespace Gobi.Game.Services.Obstacles
{
  public interface IObstacleFactory
  {
    GameObject CreateStaticObstacle(Vector2Int index, ObstacleData data, Transform parent);
    GameObject CreateMovableObstacle(Vector2Int index, ObstacleData data, Transform parent);
  }
}