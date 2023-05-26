using System.Collections;
using Gobi.Infrastructure.Services.Input;
using UnityEngine;

namespace Gobi.Game.Extensions
{
  public static class Extensions
  {
    public static bool MoveTowards(this Transform transform, Vector3 targetPos, float deltaSpeed) 
    {
      if (Vector3.Distance(transform.position, targetPos) < deltaSpeed) 
      {
        transform.position = targetPos;
        return true;
      }
      
      transform.position = Vector3.MoveTowards(transform.position, targetPos, deltaSpeed);
      return false;
    }

    public static IEnumerator MoveToCoroutine(this Transform transform, Vector3 position, float speed)
    {
      while (!transform.MoveTowards(position, speed * Time.deltaTime))
        yield return null;
    }

    public static Vector2Int ToVector2Int(this SwipeDir dir)
    {
      switch (dir)
      {
        case SwipeDir.Left:
          return Vector2Int.left;

        case SwipeDir.Right:
          return Vector2Int.right;

        case SwipeDir.Up:
          return Vector2Int.up;

        case SwipeDir.Down:
          return Vector2Int.down;
      }

      return Vector2Int.zero;
    }
  }
}