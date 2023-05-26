using Gobi.Game.Services.Field;

namespace Gobi.Infrastructure.StaticData
{
  public static class ObstacleTypeExtensions
  {
    public static TileState ToTileState(this ObstacleType type)
    {
      switch (type)
      {
        case ObstacleType.Empty:
          return TileState.Free;

        case ObstacleType.Static:
          return TileState.StaticObstacle;

        case ObstacleType.Movable:
          return TileState.MovableObstacle;
      }
      
      return TileState.Free;
    }
  }
}