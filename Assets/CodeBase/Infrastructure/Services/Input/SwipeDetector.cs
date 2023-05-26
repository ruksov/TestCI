using UnityEngine;

namespace Gobi.Infrastructure.Services.Input
{
  public class SwipeDetector
  {
    private const float MinSwipeMagnitude = 50;
    private const float SwipeDirThreshold = Mathf.Deg2Rad * 45;

    private Vector2 m_startPosition;

    public void StartSwipe(Vector2 startPosition) =>
      m_startPosition = startPosition;

    public bool Detect(Vector2 stopPosition, out SwipeDir swipeDir)
    {
      Vector2 swipeVector = stopPosition - m_startPosition;
      swipeDir = SwipeDir.COUNT;

      if (swipeVector.magnitude < MinSwipeMagnitude)
        return false;

      float dot = Vector2.Dot(Vector2.up, swipeVector.normalized);
      if (Mathf.Abs(dot) > SwipeDirThreshold)
      {
        swipeDir = dot > 0 ? SwipeDir.Up : SwipeDir.Down;
        return true;
      }

      dot = Vector2.Dot(Vector2.right, swipeVector.normalized);
      if (Mathf.Abs(dot) > SwipeDirThreshold)
      {
        swipeDir = dot > 0 ? SwipeDir.Right : SwipeDir.Left;
        return true;
      }

      return false;
    }
  }
}