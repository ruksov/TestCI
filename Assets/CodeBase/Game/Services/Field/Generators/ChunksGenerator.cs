using System.Collections.Generic;
using Gobi.Game.Services.Field.Factory;
using UnityEngine;

namespace Gobi.Game.Services.Field.Generators
{
  public class ChunksGenerator : IChunksGenerator
  {
    private const string VisualChunksObjectName = "VisualChunks";

    private readonly IChunkFactory m_chunkFactory;
    private readonly IGridConverter m_gridConverter;

    private Vector2Int m_chunkSize;
    private Transform m_chunksRootTransform;

    private readonly List<GameObject> m_chunks = new();

    public ChunksGenerator(IChunkFactory chunkFactory, IGridConverter gridConvertor)
    {
      m_chunkFactory = chunkFactory;
      m_gridConverter = gridConvertor;
    }

    public void Init(Transform fieldTransform, Vector2Int chunkSize)
    {
      m_chunkSize = chunkSize;

      m_chunksRootTransform = new GameObject(VisualChunksObjectName).transform;
      m_chunksRootTransform.transform.parent = fieldTransform;
    }

    public void Generate(Vector2Int from, int chunksCount)
    {
      for (int i = 0; i < chunksCount; ++i)
      {
        GameObject chunk = m_chunkFactory.CreateChunk(m_gridConverter.TilePosition(from));
        chunk.transform.parent = m_chunksRootTransform;
        m_chunks.Add(chunk);

        from.y += m_chunkSize.y;
      }
    }

    public void DestroyFirstChunks(int chunksCount)
    {
      foreach (GameObject chunk in m_chunks.GetRange(0, chunksCount))
        Object.Destroy(chunk);

      m_chunks.RemoveRange(0, chunksCount);
    }

    public void ShiftOrigin(Vector3 shiftOffset)
    {
      foreach (GameObject chunk in m_chunks)
        chunk.transform.position -= shiftOffset;
    }
  }
}