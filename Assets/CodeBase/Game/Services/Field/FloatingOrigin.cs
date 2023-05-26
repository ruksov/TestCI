using UnityEngine;
using Gobi.Game.Hero;
using Gobi.Game.Services.Hero;
using Gobi.Game.Services.Camera;
using Gobi.Game.Services.Field.Generators;
using Gobi.Infrastructure.StaticData;

namespace Gobi.Game.Services.Field
{
  public class FloatingOrigin
  {
    private readonly IHeroProvider m_heroProvider;
    private readonly IChunksGenerator m_chunksGenerator;
    private readonly IObstaclesGenerator m_obstaclesGenerator;
    private readonly IGridConverter m_gridConverter;
    private readonly ICameraProvider m_cameraProvider;

    private HeroMove m_heroMove;

    private Vector3 m_initialOriginShiftThreshold;
    private Vector3 m_originShiftThreshold;

    public FloatingOrigin(
      IHeroProvider heroProvider,
      IChunksGenerator chunksGenerator,
      IObstaclesGenerator obstaclesGenerator,
      IGridConverter gridConverter,
      ICameraProvider cameraProvider)
    {
      m_heroProvider = heroProvider;
      m_chunksGenerator = chunksGenerator;
      m_obstaclesGenerator = obstaclesGenerator;
      m_gridConverter = gridConverter;
      m_cameraProvider = cameraProvider;
    }

    public void Init(FieldStaticData fieldData)
    {
      m_originShiftThreshold = fieldData.FieldHeight();
      m_initialOriginShiftThreshold = fieldData.FieldHalfHeight();
    }

    public void Start()
    {
      m_heroMove = m_heroProvider.HeroMove;
      m_heroMove.GridIndexChanged += OnHeroMove;

      ShiftCameraOrigin(m_initialOriginShiftThreshold);
    }

    private void OnHeroMove()
    {
      if (ExceededShiftThreshold())
        ShiftOrigin(m_originShiftThreshold);
    }

    private bool ExceededShiftThreshold() =>
      HeroWorldPosition().y >= m_originShiftThreshold.y;

    private Vector3 HeroWorldPosition() =>
      m_gridConverter.TilePosition(m_heroMove.GridIndex);

    private void ShiftOrigin(Vector3 shift)
    {
      m_gridConverter.AddOriginShift(shift);

      m_heroProvider.Hero.transform.position -= shift;

      m_chunksGenerator.ShiftOrigin(shift);
      m_obstaclesGenerator.ShiftOrigin(shift);

      ShiftCameraOrigin(shift);
    }

    private void ShiftCameraOrigin(Vector3 shift) =>
      m_cameraProvider.Camera.transform.position -= shift;
  }
}
