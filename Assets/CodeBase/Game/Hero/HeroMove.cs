using System;
using System.Collections;
using Gobi.Game.Extensions;
using Gobi.Game.Services.Field;
using Gobi.Infrastructure.Services.Input;
using Gobi.Infrastructure.Services.Interaction;
using UnityEngine;
using Zenject;

namespace Gobi.Game.Hero
{
  public class HeroMove : MonoBehaviour
  {
    public Action GridIndexChanged;

    public float Speed = 10;
    public Vector2Int StartGridIndex;
    public Interactable Interactable;
    public bool DisableMoveChecks;
    public bool MoveByOneTile;

    private IInputService m_input;
    private IGridConverter m_gridConverter;
    private ITileMap m_tileMap;

    private bool m_isMoving;
    private Vector2Int m_gridIndex = Vector2Int.zero;

    public Vector2Int GridIndex
    {
      get => m_gridIndex;
      private set
      {
        m_tileMap.Free(m_gridIndex);
        
        m_gridIndex = value;
        m_tileMap.SetTileState(m_gridIndex, TileState.Hero);
        
        GridIndexChanged?.Invoke();
      }
    }

    private Vector3 Position
    {
      get => transform.position;
      set => transform.position = value;
    }

    [Inject]
    private void Construct(IInputService input, IGridConverter gridConvertor, ITileMap tileMap)
    {
      m_input = input;
      m_input.Swipe += Move;
      
      m_gridConverter = gridConvertor;
      m_tileMap = tileMap;

      GridIndex = StartGridIndex;
      Position = m_gridConverter.TileCenter(GridIndex);
    }

    private void Start()
    {
      StartCoroutine(MoveInDirCoroutine(Vector2Int.up));
    }

    private void OnDestroy() => 
      m_input.Swipe -= Move;

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
        GridIndex += dir;
        
        yield return transform.MoveToCoroutine(m_gridConverter.TileCenter(GridIndex), Speed);

        if (MoveByOneTile)
          break;
      }

      m_isMoving = false;
    }

    private bool CanMoveTo(Vector2Int index) => 
      m_tileMap.IndexInBounds(index) && 
      (DisableMoveChecks || m_tileMap.IsFree(index));
  }
}