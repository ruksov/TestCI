using Gobi.Game.Field.Obstacle;
using Gobi.Game.Services.Field;
using Gobi.Infrastructure.StaticData;
using UnityEngine;
using Zenject;

namespace Gobi.Game.Services.Obstacles
{
  class ObstacleFactory : IObstacleFactory
  {
    private readonly DiContainer m_container;
    private readonly IGridConverter m_gridConvertor;
    private readonly ITileMap m_tileMap;

    public ObstacleFactory(DiContainer container, IGridConverter gridConvertor, ITileMap tileMap)
    {
      m_container = container;
      m_gridConvertor = gridConvertor;
      m_tileMap = tileMap;
    }

    public GameObject CreateStaticObstacle(Vector2Int index, ObstacleData data, Transform parent)
    {
      GameObject obstacle = Object.Instantiate(data.Prefab, m_gridConvertor.TileCenter(index), Quaternion.identity, parent);
      m_tileMap.SetTileStateInRect(index, data.Size, TileState.StaticObstacle);
      return obstacle;
    }

    public GameObject CreateMovableObstacle(Vector2Int index, ObstacleData data, Transform parent)
    {
      GameObject obstacle = Object.Instantiate(data.Prefab, m_gridConvertor.TileCenter(index), Quaternion.identity, parent);
      m_container.InjectGameObject(obstacle);
      obstacle.GetComponent<ObstacleMove>().Init(index, data.Size);
      
      m_tileMap.SetTileStateInRect(index, data.Size, TileState.MovableObstacle);
      return obstacle;
    }
  }
}