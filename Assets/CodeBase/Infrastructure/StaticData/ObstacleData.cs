using System;
using UnityEngine;

namespace Gobi.Infrastructure.StaticData
{
  [Serializable]
  public class ObstacleData
  {
    public ObstacleType Type;
    public int Weight;
    public GameObject Prefab;
    public Vector2Int Size = Vector2Int.one;
  }
}