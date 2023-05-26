using Gobi.Game.Services.Hero;
using Gobi.Infrastructure.Services.StaticData;
using Gobi.Infrastructure.StaticData;
using UnityEngine;
using Zenject;

namespace Gobi.Game.Camera
{
  public class CameraFollowHero : MonoBehaviour
  {
    private Transform m_heroTransform;
    private float m_followThreshold;

    [Inject]
    private void Construct(IStaticDataService staticData, IHeroProvider heroProvider)
    {
      FieldStaticData fieldData = staticData.FieldData();
      m_followThreshold = (fieldData.ChunkSize.y - fieldData.CameraFollowThreshold) * fieldData.TileSize.y;
      m_followThreshold -= transform.position.y;
      
      m_heroTransform = heroProvider.Hero.transform;
    }

    private void Update()
    {
      if (!NeedFollow())
        return;

      Vector3 newPosition = transform.position;
      newPosition.y = m_heroTransform.position.y - m_followThreshold;
      transform.position = newPosition;
    }

    private bool NeedFollow() => 
      m_heroTransform.position.y - transform.position.y > m_followThreshold;
  }
}