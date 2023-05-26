using Gobi.Infrastructure.Services.StaticData;
using UnityEngine;
using Zenject;

namespace Gobi.Game.Services.Camera
{
  public class CameraFactory: ICameraFactory
  {
    private readonly DiContainer m_container;
    private readonly IStaticDataService m_staticData;
    private readonly ICameraProvider m_cameraProvider;

    public CameraFactory(DiContainer container, IStaticDataService staticData, ICameraProvider cameraProvider)
    {
      m_staticData = staticData;
      m_container = container;
      m_cameraProvider = cameraProvider;
    }

    public void CreateCamera()
    {
      GameObject camera = Object.Instantiate(m_staticData.CameraPrefab());
      m_container.InjectGameObject(camera);

      m_cameraProvider.Camera = camera;
    }
  }
}