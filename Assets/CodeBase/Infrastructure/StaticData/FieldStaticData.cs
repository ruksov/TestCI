using UnityEngine;

namespace Gobi.Infrastructure.StaticData
{
  [CreateAssetMenu(fileName = "FieldData", menuName = "StaticData/FieldData")]
  public class FieldStaticData : ScriptableObject
  {
    public Vector2 TileSize;
    public Vector2Int ChunkSize = new(5, 12);
    
    [Min(1)]
    public int VisibleChunksCount = 4;
    
    [Range(0, 11)]
    public int CameraFollowThreshold = 4;
    
    [Range(0, 24)]
    public int StartEmptyAreaLength = 4;

    public Vector3 FieldHeight() =>
      new(0f, ChunkSize.y * VisibleChunksCount * TileSize.y);

    public Vector3 FieldHalfHeight() =>
      FieldHeight() * 0.5f;
  }
}