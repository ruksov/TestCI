using UnityEngine;

namespace Gobi.Game.Services.Field.Factory
{
  public interface IChunkFactory
  {
    GameObject CreateChunk(Vector3 at);
  }
}