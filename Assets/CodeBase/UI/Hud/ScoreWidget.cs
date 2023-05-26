using System;
using Gobi.Game.Hero;
using Gobi.Game.Services.Hero;
using TMPro;
using UnityEngine;
using Zenject;

namespace Gobi.UI.Hud
{
  public class ScoreWidget : MonoBehaviour
  {
    public TextMeshProUGUI ScoreText;
    
    private HeroScore m_heroScore;

    [Inject]
    private void Construct(IHeroProvider heroProvider)
    {
      m_heroScore = heroProvider.HereScore;
      m_heroScore.ScoreChanged += UpdateScore;
    }

    private void Start() => 
      UpdateScore();

    private void UpdateScore()
    {
      ScoreText.text = $"{m_heroScore.Score}";
    }
  }
}