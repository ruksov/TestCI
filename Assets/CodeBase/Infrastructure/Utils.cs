using System;
using UnityEngine;

namespace Gobi.Infrastructure
{
  public class Utils
  {
    public static void ForEachRect(Vector2Int start, Vector2Int size, Action<Vector2Int> action)
    {
      Vector2Int bounds = start + size;
      
      for(Vector2Int index = start; index.y < bounds.y; ++index.y)
      {
        for(index.x = start.x; index.x < bounds.x; ++index.x)
          action(index);
      }
    }
    
    public static void ForEachRect(Vector2Int start, int width, int height, Action<Vector2Int> action)
    {
      ForEachRect(start, new Vector2Int(width, height), action);
    }
    
    public static bool ForEachRectWhile(Vector2Int start, Vector2Int size, Func<Vector2Int, bool> predicate)
    {
      Vector2Int bounds = start + size;
      
      for(Vector2Int index = start; index.y <  bounds.y; ++index.y)
      {
        for(index.x = start.x; index.x < bounds.x; ++index.x)
        {
          if(!predicate(index))
            return false;
        }
      }

      return true;
    }
    
    public static bool ForEachRectWhile(Vector2Int start, int width, int height, Func<Vector2Int, bool> predicate) => 
      ForEachRectWhile(start, new Vector2Int(width, height), predicate);
  }
}