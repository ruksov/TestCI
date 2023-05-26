using UnityEngine;

namespace Gobi.Game.Services.Field.Generators
{
  public interface IObstaclesGenerator
  {
    void Init(Transform fieldTransform, Vector2Int chunkSize, int startEmptyAreaLength);
    void Generate(Vector2Int from, int chunksCount);
    void DestroyFirstChunks(int chunksCount);
    void ShiftOrigin(Vector3 shiftOffset);
  }
}