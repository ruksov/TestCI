using System;
using UnityEngine;

namespace Gobi.Game.Hero
{
  public class HeroScore : MonoBehaviour
  {
    public Action ScoreChanged;

    public HeroMove HeroMove;

    private int m_score;

    public int Score
    {
      get => m_score;
      set
      {
        m_score = value;
        ScoreChanged?.Invoke();
      }
    }

    private void Awake()
    {
      HeroMove.GridIndexChanged += UpdateScore;
    }

    private void UpdateScore()
    {
      if (Score < HeroMove.GridIndex.y)
        Score = HeroMove.GridIndex.y;
    }
  }
}