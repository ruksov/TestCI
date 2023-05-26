using System;
using Gobi.Infrastructure.StaticData;
using UnityEngine;

namespace Gobi.Game.Services.Field
{
  public interface ITileMap
  {
    void Init(FieldStaticData fieldData);
    void SetTileState(Vector2Int index, TileState state);
    void SetTileStateInRect(Vector2Int index, Vector2Int size, TileState state);
    TileState TileState(Vector2Int index);
    void Free(Vector2Int index);
    void FreeRect(Vector2Int index, Vector2Int size);
    bool IsOccupied(Vector2Int index);
    bool IsFree(Vector2Int index);
    bool IsTileState(Vector2Int index, TileState state);
    bool RectIsFree(Vector2Int index, Vector2Int size);
    bool IndexInBounds(Vector2Int index);
    bool RectInBounds(Vector2Int index, Vector2Int size);
    void ShiftUp(int chunksCount);
  }
}