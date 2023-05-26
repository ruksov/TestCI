using Gobi.Infrastructure.Services.StaticData;
using UnityEngine;

namespace Gobi.Game.Services.Field.Factory
{
  public class ChunkFactory : IChunkFactory
  {
    private readonly IStaticDataService m_staticData;

    public ChunkFactory(IStaticDataService staticData) => 
      m_staticData = staticData;

    public GameObject CreateChunk(Vector3 at) =>
      Object.Instantiate(m_staticData.ChunkPrefab(), at, Quaternion.identity);
  }
}