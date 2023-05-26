using Gobi.Infrastructure.StaticData;
using UnityEngine;

namespace Gobi.Game.Services.Field
{
  class GridConverter : IGridConverter
  {
    private Vector3 m_tileSize;
    private Vector3 m_tileHalfSize;

    private Vector3 m_firstTilePosition;
    private Vector3 m_shiftOriginOffset;

    public void Init(Vector3 startPosition, FieldStaticData fieldData)
    {
      m_firstTilePosition = startPosition;

      m_tileSize = fieldData.TileSize;
      m_tileHalfSize = m_tileSize / 2f;

      m_shiftOriginOffset = fieldData.FieldHalfHeight();
    }

    public Vector3 TilePosition(Vector2Int index) =>
      m_firstTilePosition + MoveVector(index) - m_shiftOriginOffset;

    public Vector3 TileCenter(Vector2Int index) =>
      TilePosition(index) + m_tileHalfSize;

    private Vector3 MoveVector(Vector2Int index) =>
      new(m_tileSize.x * index.x, m_tileSize.y * index.y);

    public void AddOriginShift(Vector3 originShiftOffset) =>
      m_shiftOriginOffset += originShiftOffset;
  }
}