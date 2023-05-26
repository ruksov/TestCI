using Gobi.Infrastructure.StaticData;
using Gobi.UI.Services.Window;
using UnityEngine;

namespace Gobi.Infrastructure.Services.StaticData
{
  public interface IStaticDataService
  {
    void Load();
    GameObject WindowPrefab(WindowId id);
    GameObject UIRootPrefab();
    GameObject ChunkPrefab();
    GameObject HeroPrefab();
    GameObject CameraPrefab();
    GameObject HudPrefab();
    ObstacleData[] Obstacles();
    FieldStaticData FieldData();
  }
}