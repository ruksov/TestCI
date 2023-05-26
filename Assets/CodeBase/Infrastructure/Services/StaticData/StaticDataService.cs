using System.Collections.Generic;
using System.Linq;
using Gobi.Infrastructure.StaticData;
using Gobi.UI.Services.Window;
using UnityEngine;

namespace Gobi.Infrastructure.Services.StaticData
{
  class StaticDataService : IStaticDataService
  {
    private const string UIRootPath = "UI/UIRoot";
    private const string HudPath = "UI/Hud";
    private const string WindowsPath = "UI/WindowsData";
    private const string ChunkPath = "Field/Chunk";
    private const string HeroPath = "Hero/Hero";
    private const string ObstaclesPath = "Field/Obstacles/ObstaclesData";
    private const string FieldDataPath = "Field/FieldData";
    private const string CameraPath = "Camera/Camera";

    private ObstacleData[] m_obstacles;
    private readonly Dictionary<WindowId, GameObject> m_windows = new();
    private GameObject m_chunk;
    private FieldStaticData m_fieldData;


    public void Load()
    {
      LoadWindows();
      LoadChunk();
      LoadObstacles();
      LoadFieldData();
    }

    public GameObject WindowPrefab(WindowId id) =>
      m_windows[id];

    public GameObject UIRootPrefab() =>
      Resources.Load<GameObject>(UIRootPath);

    public GameObject ChunkPrefab() =>
      m_chunk;

    public GameObject HeroPrefab() =>
      Resources.Load<GameObject>(HeroPath);

    public GameObject CameraPrefab() => 
      Resources.Load<GameObject>(CameraPath);

    public GameObject HudPrefab() => 
      Resources.Load<GameObject>(HudPath);

    public ObstacleData[] Obstacles() => 
      m_obstacles;

    public FieldStaticData FieldData() => 
      m_fieldData;

    private void LoadWindows()
    {
      WindowsStaticData windowsData = Resources.Load<WindowsStaticData>(WindowsPath);
      foreach (WindowData data in windowsData.Windows)
        m_windows[data.Id] = data.Prefab;
    }

    private void LoadChunk() =>
      m_chunk = Resources.Load<GameObject>(ChunkPath);

    private void LoadObstacles() => 
      m_obstacles = Resources.Load<ObstaclesStaticData>(ObstaclesPath).Datas
        .Where(x => x.Weight > 0)
        .ToArray();

    private void LoadFieldData() => 
      m_fieldData = Resources.Load<FieldStaticData>(FieldDataPath);
  }
}