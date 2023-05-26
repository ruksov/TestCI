using UnityEngine;

namespace Gobi.Infrastructure.StaticData
{
  [CreateAssetMenu(fileName = "ObstaclesData", menuName = "StaticData/ObstaclesData")]
  public class ObstaclesStaticData : ScriptableObject
  {
    public ObstacleData[] Datas;
  }
}