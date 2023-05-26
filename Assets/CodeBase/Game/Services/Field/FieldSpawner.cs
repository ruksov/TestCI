using Gobi.Game.Services.Field.Generators;
using Gobi.Infrastructure.Services.StaticData;
using Gobi.Infrastructure.StaticData;
using UnityEngine;

namespace Gobi.Game.Services.Field
{
  public class FieldSpawner
  {
    private const string FieldSpawnPointTag = "FieldSpawnPoint";
    private const string FieldObjectName = "Field";

    private readonly IGridConverter m_gridConverter;
    private readonly ITileMap m_tileMap;
    private readonly IFieldGenerator m_fieldGenerator;
    private readonly FloatingOrigin m_floatingOrigin;

    private readonly FieldStaticData m_fieldData;
    
    private GameObject m_fieldObject;

    public FieldSpawner(IGridConverter gridConverter, ITileMap tileMap, IFieldGenerator fieldGenerator, IStaticDataService staticData, FloatingOrigin floatingOrigin)
    {
      m_gridConverter = gridConverter;
      m_tileMap = tileMap;
      m_fieldGenerator = fieldGenerator;
      m_floatingOrigin = floatingOrigin;

      m_fieldData = staticData.FieldData();
    }

    public void Spawn()
    {
      m_fieldObject = new GameObject(FieldObjectName);
      m_fieldObject.transform.position = GameObject.FindGameObjectWithTag(FieldSpawnPointTag).transform.position;

      m_gridConverter.Init(m_fieldObject.transform.position, m_fieldData);
      m_tileMap.Init(m_fieldData);
      m_fieldGenerator.Init(m_fieldObject.transform, m_fieldData);
      m_floatingOrigin.Init(m_fieldData);
    }
  }
}