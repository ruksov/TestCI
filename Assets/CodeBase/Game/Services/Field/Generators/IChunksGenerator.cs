using UnityEngine;

namespace Gobi.Game.Services.Field
{
  public interface IChunksGenerator
  {
    void Init(Transform fieldTransform, Vector2Int chunkSize);
    void Generate(Vector2Int from, int chunksCount);
    void DestroyFirstChunks(int chunksCount);
    void ShiftOrigin(Vector3 shiftOffset);
  }
}