using Gobi.Game.Hero;
using Gobi.Game.Services.Hero;
using Gobi.Infrastructure.StaticData;
using UnityEngine;

namespace Gobi.Game.Services.Field.Generators
{
  public class FieldGenerator : IFieldGenerator
  {
    private readonly IHeroProvider m_heroProvider;
    private readonly IObstaclesGenerator m_obstaclesGenerator;
    private readonly IChunksGenerator m_chunksGenerator;
    private readonly ITileMap m_tileMap;

    private Vector2Int m_chunkSize;
    private int m_visibleChunksCount;
    private int m_generateChunksStep;

    private HeroMove m_heroMove;
    private Vector2Int m_nextGenerateIndex;
    private Vector2Int m_triggerGenerateIndex;

    public FieldGenerator(IHeroProvider heroProvider,
      IObstaclesGenerator obstaclesGenerator,
      IChunksGenerator chunksGenerator,
      ITileMap tileMap)
    {
      m_heroProvider = heroProvider;
      m_obstaclesGenerator = obstaclesGenerator;
      m_chunksGenerator = chunksGenerator;
      m_tileMap = tileMap;
    }

    public void Init(Transform fieldTransform, FieldStaticData fieldData)
    {
      m_chunkSize = fieldData.ChunkSize;
      m_visibleChunksCount = fieldData.VisibleChunksCount;
      m_generateChunksStep = m_visibleChunksCount / 2;

      m_obstaclesGenerator.Init(fieldTransform, m_chunkSize, fieldData.StartEmptyAreaLength);
      m_chunksGenerator.Init(fieldTransform, m_chunkSize);
    }

    public void Start()
    {
      m_heroMove = m_heroProvider.HeroMove;
      m_heroMove.GridIndexChanged += OnHeroMove;

      m_nextGenerateIndex = Vector2Int.zero;
      GenerateNext(m_visibleChunksCount);
    }

    private void OnHeroMove()
    {
      if (m_heroMove.GridIndex.y != m_triggerGenerateIndex.y)
        return;

      m_obstaclesGenerator.DestroyFirstChunks(m_generateChunksStep);
      m_chunksGenerator.DestroyFirstChunks(m_generateChunksStep);

      m_tileMap.ShiftUp(m_generateChunksStep);

      GenerateNext(m_generateChunksStep);
    }

    private void GenerateNext(int chunksCount)
    {
      m_obstaclesGenerator.Generate(m_nextGenerateIndex, chunksCount);
      m_chunksGenerator.Generate(m_nextGenerateIndex, chunksCount);

      m_nextGenerateIndex.y += m_chunkSize.y * chunksCount;
      m_triggerGenerateIndex = TriggerGenerateIndex();
    }

    private Vector2Int TriggerGenerateIndex() =>
      new(m_nextGenerateIndex.x, m_nextGenerateIndex.y - m_chunkSize.y);
  }
}