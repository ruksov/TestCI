using Gobi.Infrastructure;
using Gobi.Infrastructure.StaticData;
using UnityEngine;

namespace Gobi.Game.Services.Field
{
  public class TileMap : ITileMap
  {
    private Vector2Int m_shiftOffset;
    private Vector2Int m_chunkSize;
    private Vector2Int m_size;

    private TileState[,] m_tiles;

    public void Init(FieldStaticData fieldData)
    {
      m_shiftOffset = Vector2Int.zero;

      m_chunkSize = fieldData.ChunkSize;
      m_size = new Vector2Int(m_chunkSize.x, fieldData.VisibleChunksCount * m_chunkSize.y);

      m_tiles = new TileState[m_size.y, m_size.x];
    }

    public void SetTileState(Vector2Int index, TileState state) => 
      SetLocalTileState(WorldIndexToLocal(index), state);

    public void SetTileStateInRect(Vector2Int index, Vector2Int size, TileState state) => 
      Utils.ForEachRect(index, size, rectIndex => SetTileState(rectIndex, state));

    public TileState TileState(Vector2Int index) => 
      GetLocalTileState(WorldIndexToLocal(index));

    public void Free(Vector2Int index) =>
      SetLocalTileState(WorldIndexToLocal(index), Field.TileState.Free);

    public void FreeRect(Vector2Int index, Vector2Int size) =>
      Utils.ForEachRect(index, size, Free);

    public bool IsOccupied(Vector2Int index) =>
      !IsFree(index);

    public bool IsFree(Vector2Int index) =>
      IsTileState(index, Field.TileState.Free);

    public bool IsTileState(Vector2Int index, TileState state) => 
      TileState(index) == state;

    public bool RectIsFree(Vector2Int index, Vector2Int size) =>
      Utils.ForEachRectWhile(index, size, IsFree);

    public bool IndexInBounds(Vector2Int index) =>
      LocalIndexInBounds(WorldIndexToLocal(index));

    public bool RectInBounds(Vector2Int index, Vector2Int size) =>
      Utils.ForEachRectWhile(index, size, IndexInBounds);

    public void ShiftUp(int chunksCount)
    {
      var offset = new Vector2Int(0, chunksCount * m_chunkSize.y);
      m_shiftOffset += offset;

      Utils.ForEachRect(Vector2Int.zero, m_size, index =>
      {
        TileState value = index.y < m_size.y - offset.y
          ? GetLocalTileState(index + offset)
          : Field.TileState.Free;

        SetLocalTileState(index, value);
      });
    }

    private Vector2Int WorldIndexToLocal(Vector2Int worldIndex) =>
      worldIndex - m_shiftOffset;

    private void SetLocalTileState(Vector2Int index, TileState state) =>
      m_tiles[index.y, index.x] = state;

    private TileState GetLocalTileState(Vector2Int index) =>
      m_tiles[index.y, index.x];

    private bool LocalIndexInBounds(Vector2Int index) =>
      index.x >= 0 && index.x < m_chunkSize.x && index.y >= 0 && index.y < m_tiles.GetLength(0);
  }
}