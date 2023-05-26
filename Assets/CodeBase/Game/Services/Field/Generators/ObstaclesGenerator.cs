using System.Collections.Generic;
using System.Linq;
using Gobi.Game.Services.Obstacles;
using Gobi.Infrastructure;
using Gobi.Infrastructure.StaticData;
using UnityEngine;

namespace Gobi.Game.Services.Field.Generators
{
  public class ObstaclesGenerator : IObstaclesGenerator
  {
    private const string ObstaclesObjectName = "Obstacles";
    private const string ChunkName = "Chunk_";

    private readonly ITileMap m_tileMap;
    private readonly IObstacleFactory m_factory;
    private readonly IObstacleDataProvider m_obstacleDataProvider;

    private Vector2Int m_chunkSize;
    private Transform m_chunksParent;
    private readonly List<Transform> m_chunks = new();
    private int m_startEmptyAreaLenght;

    public ObstaclesGenerator(
      ITileMap tileMap,
      IObstacleFactory factory,
      IObstacleDataProvider obstacleDataProvider)
    {
      m_tileMap = tileMap;
      m_factory = factory;
      m_obstacleDataProvider = obstacleDataProvider;
    }

    public void Init(Transform fieldTransform, Vector2Int chunkSize, int startEmptyAreaLength)
    {
      m_chunkSize = chunkSize;

      m_chunksParent = new GameObject(ObstaclesObjectName).transform;
      m_chunksParent.transform.parent = fieldTransform;

      m_startEmptyAreaLenght = startEmptyAreaLength;
    }

    public void Generate(Vector2Int from, int chunksCount)
    {
      Vector2Int end = from + Vector2Int.up * (chunksCount * m_chunkSize.y);

      from = SkipStartEmptyArea(from);

      while (from.y < end.y)
      {
        m_chunks.Add(CreateChunk(from.y));

        GenerateObstaclesForChunk(from, m_chunks.Last());

        from.y += m_chunkSize.y - from.y % m_chunkSize.y;
      }
    }

    public void DestroyFirstChunks(int chunksCount)
    {
      foreach (Transform chunkTransform in m_chunks.GetRange(0, chunksCount))
        Object.Destroy(chunkTransform.gameObject);

      m_chunks.RemoveRange(0, chunksCount);
    }

    public void ShiftOrigin(Vector3 shiftOffset)
    {
      foreach (Transform chunk in m_chunks)
        chunk.transform.position -= shiftOffset;
    }

    private Vector2Int SkipStartEmptyArea(Vector2Int from)
    {
      if (from.y >= m_startEmptyAreaLenght)
        return from;

      int emptyChunksCount = m_startEmptyAreaLenght / m_chunkSize.y;

      for (int i = 0; i < emptyChunksCount; ++i)
        m_chunks.Add(CreateChunk(i * m_chunkSize.y));

      return from + Vector2Int.up * m_startEmptyAreaLenght;
    }

    private Transform CreateChunk(int index)
    {
      Transform chunkTransform = new GameObject(ChunkName + index).transform;
      chunkTransform.parent = m_chunksParent;
      return chunkTransform;
    }

    private void GenerateObstaclesForChunk(Vector2Int from, Transform chunk)
    {
      Utils.ForEachRect(from, m_chunkSize, index => { GenerateObstacle(index, chunk); });
    }

    private void GenerateObstacle(Vector2Int index, Transform chunk)
    {
      if(m_tileMap.IsOccupied(index))
        return;

      ObstacleData obstacleData = m_obstacleDataProvider.GetRandomObstacle();

      if(obstacleData.Type == ObstacleType.Empty)
        return;

      if(CanPlace(index, obstacleData.Size, obstacleData.Type.ToTileState()))
        CreateObstacle(index, obstacleData, chunk);
    }

    private void CreateObstacle(Vector2Int index, ObstacleData data, Transform chunk)
    {
      switch (data.Type)
      {
        case ObstacleType.Static:
          m_factory.CreateStaticObstacle(index, data, chunk);
          break;

        case ObstacleType.Movable:
          m_factory.CreateMovableObstacle(index, data, chunk);
          break;
      }
    }

    private bool CanPlace(Vector2Int index, Vector2Int size, TileState state) =>
      m_tileMap.RectInBounds(index, size) &&
      m_tileMap.RectIsFree(index, size) &&
      StaticObstacleCheck(index, size, state);

    private bool StaticObstacleCheck(Vector2Int index, Vector2Int size, TileState state)
    {
      if (state != TileState.StaticObstacle)
        return true;

      List<Vector2Int> freeTileIndexes = FreeTilesInRowFrom(index, size, state);

      if (freeTileIndexes.Count == 0)
        return false;

      if (freeTileIndexes.Count > 1)
        return true;

      Vector2Int freeTileIndex = freeTileIndexes.First();
      return m_tileMap.IsFree(freeTileIndex + Vector2Int.down);

    }

    private List<Vector2Int> FreeTilesInRowFrom(Vector2Int index, Vector2Int size, TileState state)
    {
      List<Vector2Int> freeTiles = new();
      var rect = new RectInt(index, size);

      for (var rowIndex = new Vector2Int(0, index.y); rowIndex.x < m_chunkSize.x; ++rowIndex.x)
      {
        if (rect.Contains(rowIndex) || m_tileMap.IsTileState(rowIndex, state))
          continue;

        freeTiles.Add(rowIndex);
      }

      return freeTiles;
    }
  }
}