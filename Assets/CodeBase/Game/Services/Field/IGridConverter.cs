using UnityEngine;
using Gobi.Infrastructure.StaticData;

namespace Gobi.Game.Services.Field
{
  public interface IGridConverter
  {
    void Init(Vector3 startPosition, FieldStaticData fieldData);
    Vector3 TilePosition(Vector2Int index);
    Vector3 TileCenter(Vector2Int index);
    void AddOriginShift(Vector3 originShiftOffset);
  }
}