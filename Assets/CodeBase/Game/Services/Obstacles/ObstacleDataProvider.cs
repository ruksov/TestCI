using System;
using System.Linq;
using Gobi.Infrastructure.Services.StaticData;
using Gobi.Infrastructure.StaticData;
using Random = UnityEngine.Random;

namespace Gobi.Game.Services.Obstacles
{
  class ObstacleDataProvider : IObstacleDataProvider
  {
    private readonly ObstacleData[] m_obstacles;
    private int[] m_prefixSums;

    public ObstacleDataProvider(IStaticDataService staticData)
    {
      m_obstacles = staticData.Obstacles();
      InitPrefixSums();
    }

    public ObstacleData GetRandomObstacle() =>
      m_obstacles.Length == 0 ? null : m_obstacles[RandomIndex()];

    private void InitPrefixSums()
    {
      m_prefixSums = new int[m_obstacles.Length];
      if (m_prefixSums.Length == 0)
        return;

      m_prefixSums[0] = m_obstacles[0].Weight;
      
      for (int i = 1; i < m_prefixSums.Length; ++i)
        m_prefixSums[i] = m_prefixSums[i - 1] + m_obstacles[i].Weight;
    }

    private int RandomIndex()
    {
      int totalSum = m_prefixSums.Last();
      int randomNumber = Random.Range(1, totalSum + 1);
      int index = Array.BinarySearch(m_prefixSums, randomNumber);
      return index >= 0 ? index : ~index;
    }
  }
}