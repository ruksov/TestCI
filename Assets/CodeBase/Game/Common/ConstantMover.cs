using UnityEngine;

namespace Gobi.Game.Common
{
  public class ConstantMover : MonoBehaviour
  {
    public Vector3 Velocity;

    private void Update()
    {
      transform.position += Velocity * Time.deltaTime;
    }
  }
}