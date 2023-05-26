using System.Collections;
using Gobi.Game.Extensions;
using Gobi.Game.Services.Field;
using Gobi.Infrastructure;
using Gobi.Infrastructure.Services.Input;
using Gobi.Infrastructure.Services.Interaction;
using UnityEngine;
using Zenject;

namespace Gobi.Game.Field.Obstacle
{
  public class ObstacleMove : MonoBehaviour
  {
    public float Speed;
    public Interactable Interactable;

    private IInputService m_input;
    private ITileMap m_tileMap;
    private IGridConverter m_gridConverter;

    private bool m_isMoving;
    private RectInt m_gridTransform;

    private Vector2Int GridIndex { get => m_gridTransform.position; set => m_gridTransform.position = value; }
    private Vector2Int Size { get => m_gridTransform.size; set => m_gridTransform.size = value; }

    [Inject]
    public void Construct(IInputService input, ITileMap tileMap, IGridConverter gridConvertor)
    {
      m_input = input;
      m_input.Swipe += Move;

      m_tileMap = tileMap;
      m_gridConverter = gridConvertor;
    }

    private void OnDestroy() => 
      m_input.Swipe -= Move;

    public void Init(Vector2Int startIndex, Vector2Int size)
    {
      GridIndex = startIndex;
      Size = size;
    }

    private void Move(SwipeDir swipeDir)
    {
      if (m_isMoving || !Interactable.IsTouched)
        return;

      StartCoroutine(MoveInDirCoroutine(swipeDir.ToVector2Int()));
    }

    private IEnumerator MoveInDirCoroutine(Vector2Int dir)
    {
      m_isMoving = true;

      while (CanMoveTo(GridIndex + dir))
      {
        m_tileMap.FreeRect(GridIndex, Size);

        GridIndex += dir;
        m_tileMap.SetTileStateInRect(GridIndex, Size, TileState.MovableObstacle);

        yield return transform.MoveToCoroutine(m_gridConverter.TileCenter(GridIndex), Speed);
      }

      m_isMoving = false;
    }

    private bool CanMoveTo(Vector2Int nextIndex)
    {
      return m_tileMap.RectInBounds(nextIndex, Size) &&
             Utils.ForEachRectWhile(nextIndex, Size, index =>
               m_gridTransform.Contains(index) || m_tileMap.IsFree(index));
    }
  }
}